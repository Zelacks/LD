using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using ModestTree;

using Sentient.CustomInspector;

using UnityEditor;

using UnityEngine;
using UnityEngine.Profiling;

using Object = UnityEngine.Object;


namespace Sentient.DI.Editor
{

    public class ModuleConfigEditor : EditorWindow
    {

        // Window Specific

        private static Type windowType = Type.GetType( "UnityEditor.InspectorWindow,UnityEditor.dll" );
        private static ModuleConfigEditor Window;
        private static Vector2 ScrollPosition;


        // Target project context

        private static List< GameObject > ProjectContextAssets = new List< GameObject >( );
        private static int SelectedProjectContextIndex;
        private static ModuleConfigurer TargetProjectContext;


        // Inline module inspectors

        private static List< UnityEditor.Editor > InlineInspectors = new List< UnityEditor.Editor >( );


        // New modules

        private static List< string > FriendlyModuleTypes = new List< string >( );
        private static List< Type > ModuleTypes = new List< Type >( );
        private static int SelectedModuleTypeIndex;
        private static string NewModuleDirectoryPath;


        // Protected directories
        private static List< string > ProtectedDirectories = new List< string >( );
        private static Queue< AbstractModuleAsset > ProtectedModules = new Queue< AbstractModuleAsset >( );
        private static bool IsProtectedContext;


        private static string ProjectPath => projectPath ?? ( projectPath = Application.dataPath.Substring( 0, Application.dataPath.Length - 6 ) );
        private static string projectPath;



        [ MenuItem( "Sentient/Modules/Module Config Editor" ) ]
        public static void Init( )
        {
            Window = GetWindow< ModuleConfigEditor >( "Module Config", windowType );
        }

        private void OnEnable( )
        {
            EditorApplication.projectChanged += PopulateProjectContextAssets;


            // Find all available project contexts.

            PopulateProjectContextAssets( );


            // Find all protected directories

            PopulateProtectedDirectories( );

            // Find all module types that could be added to the configurer.

            ModuleTypes.AddRange(
                from assembly in AppDomain.CurrentDomain.GetAssemblies( )
                where !assembly.FullName.Contains( "System" ) && !assembly.FullName.Contains( "Unity" ) && !assembly.FullName.Contains( "Editor" )
                from type in assembly.GetTypes( )
                where ( type.DerivesFrom( typeof( AbstractModuleAsset ) ) || type.DerivesFrom( typeof( AbstractModuleComponent ) ) ) && !type.IsAbstract
                select type );


            // Extract the friendly names of those types.

            FriendlyModuleTypes.AddRange( ( from type in ModuleTypes
                select type.Name ) );
        }

        private void OnGUI( )
        {
            if ( SelectedProjectContextIndex > ProjectContextAssets.Count )
            {
                SelectedProjectContextIndex = 0;
                EditorPrefs.SetInt( Prefs.SelectedAssetIndex, 0 );
            }


            TargetProjectContext = ProjectContextAssets[ SelectedProjectContextIndex ].GetComponent< ModuleConfigurer >( );


            // Protected context check.

            IsProtectedContext = IsContextProtected( );


            // Protected modules check.

            ProtectedModules = new Queue< AbstractModuleAsset >( GetProtectedModules( TargetProjectContext ) );


            EditorExtensions.ScrollView( ref ScrollPosition, EditorExtensions.ScrollBar.Horizontal, EditorExtensions.ScrollBar.Vertical, ( ) =>
            {

                EditorGUILayout.Space( );
                EditorGUILayout.Space( );
                EditorExtensions.Vertical( ( ) =>
                {
                    // ProjectContext Select

                    SelectedProjectContextIndex = EditorGUILayout.Popup( SelectedProjectContextIndex, ProjectContextAssets.Select( asset =>
                        $"[{ProjectContextAssets.IndexOf( asset )}] {asset.name}" ).ToArray( ) );
                    EditorPrefs.SetInt( Prefs.SelectedAssetIndex, SelectedProjectContextIndex );

                    // Project context identification info.
                    EditorExtensions.Horizontal( ( ) =>
                    {
                        EditorGUILayout.LabelField( "Asset Path:", GUILayout.Width( 75 ) );
                        EditorGUILayout.LabelField( AssetDatabase.GetAssetPath( ProjectContextAssets[ SelectedProjectContextIndex ] ).Substring( 7 )
                            .TrimEnd( ".prefab".ToCharArray( ) ) );
                    } );


                    EditorGUILayout.Space( );


                    var showAddModuleFields = EditorPrefs.GetBool( Prefs.ShowAddModuleFields, true );
                    var headerClicked = DrawHeader( "Add Module", showAddModuleFields );

                    if ( headerClicked )
                    {
                        showAddModuleFields = !showAddModuleFields;
                        EditorPrefs.SetBool( Prefs.ShowAddModuleFields, showAddModuleFields );
                    }

                    if ( showAddModuleFields )
                    {
                        if ( IsProtectedContext )
                        {
                            EditorExtensions.Horizontal( ( ) =>
                            {
                                EditorGUILayout.HelpBox( "This context is protected and cannot be edited.", MessageType.Warning );
                                EditorExtensions.Button( "Clone", new[ ] { GUILayout.Height( 37.5f ) }, ( ) =>
                                {
                                    // Get the path of the directory the context should be coppied too.

                                    var newProjectContextDirectoryPath = EditorUtility.OpenFolderPanel( "New Module Directory", Application.dataPath, "" );
                                    newProjectContextDirectoryPath = newProjectContextDirectoryPath.StartsWith( ProjectPath ) ? 
                                        NewModuleDirectoryPath.Substring( ProjectPath.Length ) : 
                                        "Assets";


                                    // Copy the asset.

                                    AssetDatabase.CopyAsset( AssetDatabase.GetAssetPath( ProjectContextAssets[ SelectedProjectContextIndex ] ),
                                        $"{newProjectContextDirectoryPath}/ProjectContext.prefab" );

                                    AssetDatabase.SaveAssets( );


                                    // Refresh the project context assets list.

                                    PopulateProjectContextAssets( );

                                } );
                            } );
                        }
                        else
                        {
                            // If any protected modules are referenced ...

                            if ( ProtectedModules.Any( ) )
                            {
                                EditorExtensions.Horizontal( ( ) =>
                                {
                                    // Display an error ...

                                    EditorGUILayout.HelpBox( "One or more protected modules have been found. These are reference only modules and " +
                                        "must be replaced.", MessageType.Error );


                                    // And provide a convenient means to resolution.

                                    EditorExtensions.Button( "Fix Now", new[ ] { GUILayout.Height( 37.5f ) }, ( ) =>
                                    {
                                        NewModuleDirectoryPath = EditorUtility.OpenFolderPanel( "New Module Directory", Application.dataPath, "" );
                                        NewModuleDirectoryPath = NewModuleDirectoryPath.StartsWith( ProjectPath ) ? 
                                            NewModuleDirectoryPath.Substring( ProjectPath.Length ) : 
                                            "Assets";

                                        // Process all queued modules ...

                                        while ( ProtectedModules.Any( ) )
                                        {
                                            var module = ProtectedModules.Dequeue( );
                                            var newModulePath = $"{NewModuleDirectoryPath}/{module.name}.asset";


                                            // Copy that asset to the new path.

                                            AssetDatabase.CopyAsset( AssetDatabase.GetAssetPath( module ), newModulePath );
                                            AssetDatabase.SaveAssets( );

                                            // Ensure the project context references the new module.

                                            var newModule = AssetDatabase.LoadAssetAtPath< AbstractModuleAsset >( newModulePath );
                                            TargetProjectContext.Modules[ TargetProjectContext.Modules.IndexOf( module ) ] = newModule;
                                        }

                                    } );

                                } );

                            }
                            else
                            {

                                // Add Existing Module

                                EditorGUILayout.HelpBox( "Drag a module into the field below to add it to the project context.", MessageType.Info );
                                Object module = null;
                                module = EditorGUILayout.ObjectField( "", module, typeof( IZenjectModule ), false );

                                if ( module != null )
                                    AddModule( module );

                                EditorGUILayout.Space( );

                                // Add New Module

                                EditorGUILayout.HelpBox( "Add a new module by first selecting the module you require from the dropdown and then clicking " +
                                    "the \"Add New Module\" button.", MessageType.Info );

                                EditorExtensions.Horizontal( ( ) =>
                                {
                                    NewModuleDirectoryPath = EditorPrefs.GetString( Prefs.NewModuleDirectoryPath, Application.dataPath );
                                    NewModuleDirectoryPath = EditorGUILayout.TextField( NewModuleDirectoryPath );

                                    EditorExtensions.Button( "...", new[ ] { GUILayout.MaxWidth( 50 ) }, ( ) =>
                                    {
                                        NewModuleDirectoryPath = EditorUtility.OpenFolderPanel( "New Module Directory", NewModuleDirectoryPath, "" );
                                        NewModuleDirectoryPath = NewModuleDirectoryPath.StartsWith( ProjectPath ) ? 
                                            NewModuleDirectoryPath.Substring( ProjectPath.Length ) : 
                                            "Assets";
                                    } );

                                    EditorPrefs.SetString( Prefs.NewModuleDirectoryPath, NewModuleDirectoryPath );

                                } );

                                EditorExtensions.Horizontal( ( ) =>
                                {
                                    SelectedModuleTypeIndex = EditorGUILayout.Popup( SelectedModuleTypeIndex, FriendlyModuleTypes.ToArray( ), Styles.Popup );
                                    EditorExtensions.Button( "Add New Module", new[ ] { GUILayout.MaxWidth( 150 ) }, ( ) =>
                                    {
                                        Object newModule;

                                        if ( ModuleTypes[ SelectedModuleTypeIndex ].DerivesFrom( typeof( AbstractModuleAsset ) ) )
                                        {
                                            newModule = CreateInstance( ModuleTypes[ SelectedModuleTypeIndex ] );

                                            AssetDatabase.CreateAsset( newModule,
                                                $"{NewModuleDirectoryPath}/{ObjectNames.NicifyVariableName( ModuleTypes[ SelectedModuleTypeIndex ].Name )}.asset" );

                                            AssetDatabase.SaveAssets( );
                                        }
                                        else
                                            newModule = TargetProjectContext.gameObject.AddComponent( ModuleTypes[ SelectedModuleTypeIndex ] );

                                        AddModule( newModule );
                                    } );
                                } );

                            }

                        }


                        EditorGUILayout.Space( );
                    }


                    // Inline inspectors

                    DrawInlineInspectors( Prefs.ShowModuleEditors, "Modules", TargetProjectContext.Modules );
                    DrawInlineInspectors( Prefs.ShowComponentModuleEditors, "Component Modules", TargetProjectContext.BehaviourModules );

                } );
            } );
        }

        /// <summary>
        /// Draws the header.
        /// </summary>
        private bool DrawHeader( string header, bool contentShown )
        {
            var headerContent = new GUIContent( header );

            var headerRect = GUILayoutUtility.GetRect( headerContent, Styles.Header, GUILayout.ExpandWidth( false ),
                GUILayout.ExpandWidth( true ), GUILayout.MaxHeight( 25 ) );

            var iconRect = new Rect( headerRect );
            iconRect.position = new Vector2( iconRect.min.x + 5, iconRect.position.y );

            if ( Event.current.type == EventType.Repaint )
            {
                Styles.Header.Draw( headerRect, headerContent, false, false, false, false );
                EditorGUI.LabelField( iconRect, new GUIContent( contentShown ? Styles.EyeHide : Styles.EyeShow ) );
            }

            if ( Event.current.type == EventType.MouseDown && headerRect.Contains( Event.current.mousePosition ) )
            {
                Event.current.Use( );
                return true;
            }

            return false;
        }

        /// <summary>
        /// Draws the inline editors for the specified collection of modules.
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="showModulesEditorPref"></param>
        /// <param name="header"></param>
        /// <param name="modules"></param>
        private void DrawInlineInspectors< TModule >( string showModulesEditorPref, string header, IEnumerable< TModule > modules )
            where TModule : Object, IZenjectModule
        {
            var showModules = EditorPrefs.GetBool( showModulesEditorPref, true );
            var headerClicked = DrawHeader( header, showModules );

            if ( headerClicked )
            {
                showModules = !showModules;
                EditorPrefs.SetBool( showModulesEditorPref, showModules );
            }

            if ( showModules )
            {
                EditorExtensions.Vertical( new[ ] { GUILayout.ExpandWidth( true ) }, ( ) =>
                {
                    if ( modules.IsEmpty( ) )
                    {
                        EditorGUILayout.LabelField( "No modules to show.", Styles.NoModulesText );
                    }
                    else
                    {
                        foreach ( var module in modules.Where( m => m != null ) )
                        {
                            EditorExtensions.Vertical( ( ) =>
                            {
                                EditorExtensions.Disable( !IsProtectedContext, ( ) =>
                                {

                                    var showModule = EditorPrefs.GetBool( $"{Prefs.InlineModuleEditor}_{module.name}", true );
                                    var newShowModule = EditorExtensions.InspectorTitlebar( showModule, module, true, module.Enabled, enabled =>
                                    {
                                        module.Enabled = enabled;
                                    } );

                                    if ( showModule != newShowModule )
                                        EditorPrefs.SetBool( $"{Prefs.InlineModuleEditor}_{module.name}", newShowModule );


                                    bool referenceError = false;

                                    if ( module.GetType( ).DerivesFrom( typeof( AbstractModuleAsset ) ) )
                                    {
                                        referenceError = ProtectedModules.Any( asset => asset == module );
                                    }

                                    if ( showModule || IsProtectedContext )
                                    {
                                        // Disable the inline editor if this module is protected.

                                        EditorExtensions.Disable( !referenceError, ( ) =>
                                        {
                                            var editor = InlineInspectors.FirstOrDefault( inspector => inspector.target == module );
                                            if ( editor == null )
                                                InlineInspectors.Add( editor = UnityEditor.Editor.CreateEditor( module ) );

                                            editor.OnInspectorGUI( );
                                        } );
                                    }
                                } );

                            } );

                        }

                        EditorGUILayout.Space( );
                    }

                } );

            }

        }




        /// <summary>
        /// Removes the module from the project context.
        /// </summary>
        /// <param name="command"></param>
        [ MenuItem( "CONTEXT/AbstractModuleAsset/Remove Module" ) ]
        [ MenuItem( "CONTEXT/AbstractModuleComponent/Remove Module" ) ]
        private static void RemoveModule( MenuCommand command )
        {
            if ( IsProtectedContext )
            {
                EditorUtility.DisplayDialog( "Context Protected!", "This module could not removed. This context is protected.", "Cancel" );
                return;
            }

            var abstractModuleAsset = command.context as AbstractModuleAsset;
            var abstractModuleComponent = command.context as AbstractModuleComponent;

            if ( abstractModuleAsset != null )
                TargetProjectContext.Modules.Remove( abstractModuleAsset );

            if ( abstractModuleComponent != null )
                TargetProjectContext.BehaviourModules.Remove( abstractModuleComponent );

            InlineInspectors.Remove( InlineInspectors.First( editor => editor.target == command.context ) );
        }

        /// <summary>
        /// Adds a module to the project context.
        /// </summary>
        /// <param name="module"></param>
        private static void AddModule( Object module )
        {
            var abstractModuleAsset = module as AbstractModuleAsset;
            var abstractModuleComponent = module as AbstractModuleComponent;

            if ( abstractModuleAsset != null )
            {
                TargetProjectContext.Modules.Add( abstractModuleAsset );
                EditorUtility.SetDirty( TargetProjectContext );
            }

            if ( abstractModuleComponent != null )
            {
                TargetProjectContext.BehaviourModules.Add( abstractModuleComponent );
                EditorUtility.SetDirty( TargetProjectContext );
            }


        }




        /// <summary>
        /// Finds the project contexts.
        /// </summary>
        public void PopulateProjectContextAssets( )
        {
            ProjectContextAssets.Clear( );

            ProjectContextAssets.AddRange(
                from asset in AssetDatabase.FindAssets( "ProjectContext" )
                let assetPath = AssetDatabase.GUIDToAssetPath( asset )
                where assetPath.Contains( ".prefab" )
                select AssetDatabase.LoadAssetAtPath< GameObject >( assetPath ) );
        }

        /// <summary>
        /// Populates the ProtectedDirectories list.
        /// </summary>
        /// <returns></returns>
        public void PopulateProtectedDirectories( )
        {
            var protectedDirectoryAssets = AssetDatabase.FindAssets( "__PROTECTED_DIR_" );
            ProtectedDirectories.AddRange( from asset in protectedDirectoryAssets
                                           let path = new FileInfo( AssetDatabase.GUIDToAssetPath( asset ) ).DirectoryName
                                           select path );
        }

        /// <summary>
        /// Determines whether [is context protected].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is context protected]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsContextProtected( )
        {
            var assetPath = AssetDatabase.GetAssetPath( ProjectContextAssets[ SelectedProjectContextIndex ] );
            var directory = new DirectoryInfo( $"{Application.dataPath.Replace( "Assets", "" )}" +
                $"{assetPath.Remove( assetPath.LastIndexOf( "/", StringComparison.Ordinal ) )}" );

            return directory.GetFiles( ).Any( file => file.Name == "__PROTECTED_DIR_CONTEXT__.asset" );
        }

        /// <summary>
        /// Gets all protected modules the specified <see cref="ModuleConfigurer"/> currently references.
        /// </summary>
        /// <param name="moduleConfigurer">The module configurer.</param>
        /// <returns></returns>
        public IEnumerable< AbstractModuleAsset > GetProtectedModules( ModuleConfigurer moduleConfigurer )
        {
            return from module in moduleConfigurer.Modules
                let path = AssetDatabase.GetAssetPath( module )
                where File.Exists( path )
                let file = new FileInfo( path )
                where ProtectedDirectories.Any( directory => directory == file.DirectoryName )
                select module;
        }



        /// <summary>
        /// Editor preferences.
        /// </summary>
        private static class Prefs
        {

            public static readonly string SelectedAssetIndex = "module_config_editor_selected_asset_index";
            public static readonly string InlineModuleEditor = "module_config_editor_inline_module_editor";
            public static readonly string ShowModuleEditors = "module_config_editor_show_module_editors";
            public static readonly string ShowComponentModuleEditors = "module_config_editor_show_component_module_editors";
            public static readonly string ShowAddModuleFields = "module_config_editor_show_add_module_fields";
            public static readonly string NewModuleDirectoryPath = "module_config_editor_new_module_directory_path";

        }

        /// <summary>
        /// Styles.
        /// </summary>
        public static class Styles
        {

            public static GUIStyle Module = new GUIStyle( EditorStyles.foldout );

            public static GUIStyle Header = new GUIStyle( ( GUIStyle )typeof( EditorStyles )
                .GetProperty( "inspectorBig", BindingFlags.Static | BindingFlags.NonPublic ).GetValue( null ) );

            public static GUIStyle NoModulesText = new GUIStyle( EditorStyles.label );

            public static GUIStyle Popup = new GUIStyle( EditorStyles.popup );

            public static Texture EyeShow;
            public static Texture EyeHide;


            static Styles( )
            {
                Module.fontStyle = FontStyle.Bold;
                Module.contentOffset = new Vector2( 2, 0 );
                Module.padding.top = Module.padding.bottom = 0;

                Header.font = EditorStyles.boldLabel.font;
                Header.margin = new RectOffset( 1, 1, 0, 0 );

                NoModulesText.alignment = TextAnchor.MiddleCenter;
                NoModulesText.fontStyle = FontStyle.Italic;

                Popup.fontSize = EditorStyles.label.fontSize;

                EyeShow = AssetDatabase.LoadAssetAtPath< Texture >( AssetDatabase.GUIDToAssetPath( AssetDatabase.FindAssets( "eye-show" ).First( ) ) );
                EyeHide = AssetDatabase.LoadAssetAtPath< Texture >( AssetDatabase.GUIDToAssetPath( AssetDatabase.FindAssets( "eye-hide" ).First( ) ) );
            }

        }

    }

}
