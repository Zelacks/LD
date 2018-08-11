using System;
using System.Reflection;

using UnityEditor;

using UnityEditorInternal;

using UnityEngine;

using Zenject;

using EditorStyles = UnityEditor.EditorStyles;
using Object = UnityEngine.Object;


namespace Sentient.CustomInspector
{

    public static partial class EditorExtensions
    {

        public static bool InspectorTitlebar( bool foldout, Object targetObj, bool expandable, bool toggleValue, Action< bool > toggleChanged )
        {
            return InspectorTitlebar( GUILayoutUtility.GetRect( GUIContent.none, InspectorTitlebarStyles.InspectorTitlebar ), foldout, targetObj, expandable, toggleValue, toggleChanged );
        }

        /// <summary>
        ///   <para>Make an inspector-window-like titlebar.</para>
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the titlebar.</param>
        /// <param name="foldout">The foldout state shown with the arrow.</param>
        /// <param name="targetObj">The object (for example a component) that the titlebar is for.</param>
        /// <param name="targetObjs">The objects that the titlebar is for.</param>
        /// <param name="expandable">Whether this editor should display a foldout arrow in order to toggle the display of its properties.</param>
        /// <returns>
        ///   <para>The foldout state selected by the user.</para>
        /// </returns>
        public static bool InspectorTitlebar( Rect position, bool foldout, Object targetObj, bool expandable, bool toggleValue, Action< bool > toggleChanged )
        {
            return InspectorTitlebar( position, ( foldout ? 1 : 0 ) != 0, new Object[ 1 ]
            {
                targetObj
            }, ( expandable ? 1 : 0 ) != 0, toggleValue, toggleChanged );
        }

        /// <summary>
        ///   <para>Make an inspector-window-like titlebar.</para>
        /// </summary>
        /// <param name="position">Rectangle on the screen to use for the titlebar.</param>
        /// <param name="foldout">The foldout state shown with the arrow.</param>
        /// <param name="targetObj">The object (for example a component) that the titlebar is for.</param>
        /// <param name="targetObjs">The objects that the titlebar is for.</param>
        /// <param name="expandable">Whether this editor should display a foldout arrow in order to toggle the display of its properties.</param>
        /// <returns>
        ///   <para>The foldout state selected by the user.</para>
        /// </returns>
        public static bool InspectorTitlebar( Rect position, bool foldout, Object[ ] targetObjs, bool expandable, bool toggleValue, Action< bool > toggleChanged )
        {
            int controlId = GUIUtility.GetControlID( InspectorTitlebarStyles.TitlebarHash, FocusType.Keyboard, position );
            DoInspectorTitlebar( position, controlId, foldout, targetObjs, InspectorTitlebarStyles.InspectorTitlebar, toggleValue, toggleChanged );
            foldout = ReflectedEditorMethods.DoObjectMouseInteraction( foldout, position, targetObjs, controlId );
            if ( expandable )
            {
                Rect foldoutRenderRect = ReflectedEditorMethods.GetInspectorTitleBarObjectFoldoutRenderRect( position );
                ReflectedEditorMethods.DoObjectFoldoutInternal( foldout, foldoutRenderRect, controlId );
            }

            return foldout;
        }

        internal static void DoInspectorTitlebar( Rect position, int id, bool foldout, Object[ ] targetObjs, GUIStyle baseStyle, bool toggleValue, Action< bool > toggleChanged )
        {
            var inspectorTitlebarText = InspectorTitlebarStyles.InspectorTitlebarText;
            var iconButton = InspectorTitlebarStyles.IconButton;
            var vector2 = iconButton.CalcSize( EditorGUIContents.TitleSettingsIcon );
            var position1 = new Rect( position.x + ( float )baseStyle.padding.left, position.y + ( float )baseStyle.padding.top, 16f, 16f );
            var position2 = new Rect( ( float )( ( double )position.xMax - ( double )baseStyle.padding.right - 2.0 - 16.0 ), position1.y, vector2.x, vector2.y );
            var position3 = new Rect( ( float )( ( double )position1.xMax + 2.0 + 2.0 + 16.0 ), position1.y, 100f, position1.height )
            {
                xMax = position2.xMin - 2f
            };

            var current = Event.current;
            int num = -1;

            foreach ( var targetObj in targetObjs )
            {
                int objectEnabled = EditorUtility.GetObjectEnabled( targetObj );
                if ( num == -1 )
                    num = objectEnabled;
                else if ( num != objectEnabled )
                    num = -2;
            }

            EditorGUI.showMixedValue = num == -2;
            var position4 = position1;
            position4.x = position1.xMax + 2f;
            EditorGUI.BeginChangeCheck( );
            var backgroundColor = GUI.backgroundColor;

            bool flag2 = AnimationMode.IsPropertyAnimated( targetObjs[ 0 ], "m_Enabled" );
            if ( flag2 )
            {
                var color = AnimationMode.animatedPropertyColor;

                if ( ReflectedEditorMethods.InAnimationRecording( ) )
                    color = AnimationMode.recordedPropertyColor;
                else if ( ReflectedEditorMethods.IsPropertyCandidate( targetObjs[ 0 ], "m_Enabled" ) )
                    color = AnimationMode.candidatePropertyColor;

                color.a *= GUI.color.a;
                GUI.backgroundColor = color;
            }

            int controlId = GUIUtility.GetControlID( InspectorTitlebarStyles.TitlebarHash, FocusType.Keyboard, position );
            bool enabled = ReflectedEditorMethods.DoToggleForward( position4, controlId, toggleValue, GUIContent.none, EditorStyles.toggle );

            if ( flag2 )
                GUI.backgroundColor = backgroundColor;

            if ( EditorGUI.EndChangeCheck( ) )
            {
                Undo.RecordObjects( targetObjs, ( !enabled ? "Disable" : "Enable" ) + " Component" + ( targetObjs.Length <= 1 ? "" : "s" ) );
                toggleChanged?.Invoke( enabled );
            }

            EditorGUI.showMixedValue = false;
            if ( position4.Contains( Event.current.mousePosition ) && ( current.type == EventType.MouseDown && current.button == 1 || current.type == EventType.ContextClick ) )
            {
                ReflectedEditorMethods.DoPropertyContextMenu( new SerializedObject( targetObjs[ 0 ] ).FindProperty( "m_Enabled" ) );
                current.Use( );
            }

            var rectangle = position2;
            rectangle.x -= 18f;
            rectangle = ReflectedEditorMethods.DrawEditorHeaderItems( rectangle, targetObjs );
            position3.xMax = rectangle.xMin - 2f;
            if ( current.type == EventType.Repaint )
            {
                Texture2D miniThumbnail = AssetPreview.GetMiniThumbnail( targetObjs[ 0 ] );
                GUIStyle.none.Draw( position1, ReflectedEditorMethods.TempContent( ( Texture )miniThumbnail ), false, false, false, false );
            }

            bool enabled1 = GUI.enabled;
            GUI.enabled = true;
            switch ( current.type )
            {
                case EventType.MouseDown:
                    if ( position2.Contains( current.mousePosition ) )
                    {
                        ReflectedEditorMethods.DisplayObjectContextMenu( position2, targetObjs, 0 );
                        current.Use( );
                        break;
                    }

                    break;
                case EventType.Repaint:
                    baseStyle.Draw( position, GUIContent.none, id, foldout );
                    position = baseStyle.padding.Remove( position );
                    inspectorTitlebarText.Draw( position3, ReflectedEditorMethods.TempContent( ObjectNames.GetInspectorTitle( targetObjs[ 0 ] ) ), id, foldout );
                    iconButton.Draw( position2, EditorGUIContents.TitleSettingsIcon, id, foldout );
                    break;
            }

            GUI.enabled = enabled1;
        }

    }




    internal static class InspectorTitlebarStyles
    {

        public static int TitlebarHash => ( int )typeof( EditorGUI ).GetField( "s_TitlebarHash", BindingFlags.Static | BindingFlags.NonPublic )?.GetValue( null );

        public static GUIStyle InspectorTitlebar;
        public static GUIStyle InspectorTitlebarText;
        public static GUIStyle IconButton;

        static InspectorTitlebarStyles( )
        {
            InspectorTitlebar = ( GUIStyle )typeof( EditorStyles ).GetProperty( "inspectorTitlebar", BindingFlags.NonPublic | BindingFlags.Static )?.GetValue( null );
            InspectorTitlebarText = ( GUIStyle )typeof( EditorStyles ).GetProperty( "inspectorTitlebarText", BindingFlags.NonPublic | BindingFlags.Static )?.GetValue( null );
            IconButton = ( GUIStyle )typeof( EditorStyles ).GetProperty( "iconButton", BindingFlags.NonPublic | BindingFlags.Static )?.GetValue( null );

        }

    }




    internal static class EditorGUIContents
    {

        private static BindingFlags BindingFlags = BindingFlags.NonPublic | BindingFlags.Static;
        public static GUIContent TitleSettingsIcon;

        static EditorGUIContents( )
        {
            TitleSettingsIcon = ( GUIContent )typeof( EditorGUI ).GetNestedType( "GUIContents", BindingFlags.NonPublic ).GetProperty( "titleSettingsIcon", BindingFlags )?
                .GetValue( null );
        }

    }

}
