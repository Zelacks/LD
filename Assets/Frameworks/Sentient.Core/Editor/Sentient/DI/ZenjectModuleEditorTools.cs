using System.Linq;

using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;

using Zenject;

using Object = UnityEngine.Object;


namespace Sentient.DI.Editor
{

    /// <summary>
    /// Editor tools for correcting problems with scene configuration relating to Zenject Modules.
    /// </summary>
    public class ZenjectModuleEditorTools
    {

        /// <summary>
        /// Invoked from the Unity main menu, fixes all context objects in the scene by removing null installers and ensuring all contexts have a module installer added.
        /// </summary>
        [ MenuItem( "Sentient/Modules/Fix Zenject Contexts" ) ]
        public static void UpdateContexts( )
        {
            const string message = "This operation cannot be undone. Please save the scene before using.\n\n" +
                "This will remove any null installers and ensure the modules installer exists on every context " +
                "found within the scene.";

            if ( !EditorUtility.DisplayDialog( "Warning", message, "Continue", "Cancel" ) )
                return;

            int modifyCount = 0;
            foreach ( var context in Object.FindObjectsOfType< Context >( ) )
            {
                bool changed = false;

                changed |= EnsureModuleInstallerAdded( context );
                changed |= RemoveNullInstallers( context );

                if ( changed )
                    modifyCount++;
            }

            EditorUtility.DisplayDialog( "Update Contexts", $"{modifyCount} contexts were changed.", "OK" );
        }

        /// <summary>
        /// Ensures all selected contexts have a <see cref="ModulesInstaller"/> component attached.
        /// </summary>
        public static void EnsureModuleInstallerAddedMenu( )
        {
            const string message = "This operation cannot be undone. Please save the scene before using.";

            if ( !EditorUtility.DisplayDialog( "Warning", message, "Continue", "Cancel" ) )
                return;

            foreach ( var context in UnityEditor.Selection.gameObjects.Select( g => g.gameObject.GetComponent< Context >( ) ) )
                EnsureModuleInstallerAdded( context );
        }

        /// <summary>
        /// Ensures the <see cref="ModulesInstaller"/> exists on the component as is added to the install list.
        /// </summary>
        /// <param name="context">The context to add a <see cref="ModulesInstaller"/> to, if necessary.</param>
        /// <returns>True if any installers were added.</returns>
        public static bool EnsureModuleInstallerAdded( Context context )
        {
            EditorGUI.BeginChangeCheck( );

            var installer = context.GetComponent< ModulesInstaller >( );

            bool modified = false;

            // Note: Editor Undo does not work correctly. Dont bother doing it at all, just show a warning.
            // The List doesnt undo as expected and doesn't always revert the previous entry.

            if ( installer == null )
            {
                installer = context.gameObject.AddComponent< ModulesInstaller >( );
                modified = true;
            }

            if ( !context.Installers.Contains( installer ) )
            {
                var newInstallers = context.Installers.Concat( new[ ] { installer } ).ToList( );
                context.Installers = newInstallers;

                modified = true;
            }

            if ( !modified )
                return false;

            EditorUtility.SetDirty( context );
            EditorSceneManager.MarkSceneDirty( context.gameObject.scene );

            Debug.Log( $"Added {nameof(ModulesInstaller)} to: {context.GetType( ).Name} {context.name}.", context );
            return true;
        }

        /// <summary>
        /// Removes all null installer entries from a <see cref="Context"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> from which to remove all null installer entries.</param>
        /// <returns>True if the context contained any null installer entries.</returns>
        public static bool RemoveNullInstallers( Context context )
        {
            if ( !context.Installers.Contains( null ) )
                return false;

            context.Installers = context.Installers.Where( i => i != null ).ToList( );

            EditorUtility.SetDirty( context );
            EditorSceneManager.MarkSceneDirty( context.gameObject.scene );

            // Note: Editor Undo does not work correctly. Dont bother doing it at all, just show a warning.
            // The List doesnt undo as expected and doesn't always revert the previous entry.

            Debug.Log( $"Removed null instance from {context.GetType( ).Name} {context.name}.", context );
            return true;
        }

    }

}
