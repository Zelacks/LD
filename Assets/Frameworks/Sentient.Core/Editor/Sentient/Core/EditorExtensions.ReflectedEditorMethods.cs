using System;
using System.Reflection;

using UnityEditor;

using UnityEngine;

using Object = UnityEngine.Object;


namespace Sentient.CustomInspector
{

    public static partial class EditorExtensions
    {

        internal static class ReflectedEditorMethods
        {

            private static BindingFlags BindingFlags = BindingFlags.NonPublic | BindingFlags.Static;

            private static Type editorGUIInteralType = Type.GetType( "UnityEditor.EditorGUIInternal,UnityEditor.dll" );
            private static MethodInfo doObjectMouseInteractionMethod;
            private static MethodInfo doToggleForwardMethod;
            private static MethodInfo tempContentTextureMethod;
            private static MethodInfo tempContentStringMethod;
            private static MethodInfo displayObjectContextMenuMethod;
            private static MethodInfo doPropertyContextMenuMethod;
            private static MethodInfo inAnimationRecordingMethod;
            private static MethodInfo isPropertyCandidateMethod;
            private static MethodInfo drawEditorHeaderItemsMethod;


            static ReflectedEditorMethods( )
            {
                doObjectMouseInteractionMethod = typeof( EditorGUI ).GetMethod( "DoObjectMouseInteraction", BindingFlags );

                doToggleForwardMethod = editorGUIInteralType.GetMethod( "DoToggleForward", BindingFlags );

                tempContentTextureMethod = typeof( EditorGUIUtility ).GetMethod( "TempContent", BindingFlags, null, new[ ] { typeof( Texture ) }, null );

                tempContentStringMethod = typeof( EditorGUIUtility ).GetMethod( "TempContent", BindingFlags, null, new[ ] { typeof( string ) }, null );

                displayObjectContextMenuMethod = typeof( EditorUtility ).GetMethod( "DisplayObjectContextMenu", BindingFlags, null, new[ ]
                {
                    typeof( Rect ),
                    typeof( Object[ ] ), typeof( int )
                }, null );

                doPropertyContextMenuMethod = typeof( EditorGUI ).GetMethod( "DoPropertyContextMenu", BindingFlags );

                inAnimationRecordingMethod = typeof( AnimationMode ).GetMethod( "InAnimationRecording", BindingFlags );

                isPropertyCandidateMethod = typeof( AnimationMode ).GetMethod( "IsPropertyCandidate", BindingFlags );

                drawEditorHeaderItemsMethod = typeof( EditorGUIUtility ).GetMethod( "DrawEditorHeaderItems", BindingFlags );
            }


            /// <summary>
            /// Does the object mouse interaction.
            /// </summary>
            /// <param name="foldout">if set to <c>true</c> [foldout].</param>
            /// <param name="interactionRect">The interaction rect.</param>
            /// <param name="targetObjs">The target objs.</param>
            /// <param name="id">The identifier.</param>
            /// <returns></returns>
            public static bool DoObjectMouseInteraction( bool foldout, Rect interactionRect, Object[ ] targetObjs, int id ) =>
                ( bool )doObjectMouseInteractionMethod?.Invoke( null, new object[ ] { foldout, interactionRect, targetObjs, id } );

            /// <summary>
            /// Does the property context menu.
            /// </summary>
            /// <param name="property">The property.</param>
            public static void DoPropertyContextMenu( SerializedProperty property ) => doPropertyContextMenuMethod?.Invoke( null, new object[ ] { property } );

            /// <summary>
            /// Gets the inspector title bar object foldout render rect.
            /// </summary>
            /// <param name="rect">The rect.</param>
            /// <returns></returns>
            public static Rect GetInspectorTitleBarObjectFoldoutRenderRect( Rect rect )
            {
                return new Rect( rect.x + 3f, rect.y + 3f, 16f, 16f );
            }

            /// <summary>
            /// Does the object foldout internal.
            /// </summary>
            /// <param name="foldout">if set to <c>true</c> [foldout].</param>
            /// <param name="renderRect">The render rect.</param>
            /// <param name="id">The identifier.</param>
            public static void DoObjectFoldoutInternal( bool foldout, Rect renderRect, int id )
            {
                bool enabled = GUI.enabled;
                GUI.enabled = true;
                if ( Event.current.GetTypeForControl( id ) == EventType.Repaint )
                {
                    bool flag = GUIUtility.hotControl == id;
                    EditorStyles.foldout.Draw( renderRect, flag, flag, foldout, false );
                }

                GUI.enabled = enabled;
            }

            /// <summary>
            /// Does the toggle forward.
            /// </summary>
            /// <param name="position">The position.</param>
            /// <param name="id">The identifier.</param>
            /// <param name="value">if set to <c>true</c> [value].</param>
            /// <param name="content">The content.</param>
            /// <param name="style">The style.</param>
            /// <returns></returns>
            public static bool DoToggleForward( Rect position, int id, bool value, GUIContent content, GUIStyle style ) =>
                ( bool )doToggleForwardMethod.Invoke( null, new object[ ] { position, id, value, content, style } );

            /// <summary>
            /// Draws the editor header items.
            /// </summary>
            /// <param name="rectangle">The rectangle.</param>
            /// <param name="targetObjs">The target objs.</param>
            /// <returns></returns>
            public static Rect DrawEditorHeaderItems( Rect rectangle, UnityEngine.Object[ ] targetObjs ) =>
                ( Rect )drawEditorHeaderItemsMethod.Invoke( null, new object[ ] { rectangle, targetObjs } );

            /// <summary>
            /// Temporaries the content.
            /// </summary>
            /// <param name="i">The i.</param>
            /// <returns></returns>
            public static GUIContent TempContent( Texture i ) => ( GUIContent )tempContentTextureMethod.Invoke( null, new object[ ] { i } );

            /// <summary>
            /// Temporaries the content.
            /// </summary>
            /// <param name="t">The t.</param>
            /// <returns></returns>
            public static GUIContent TempContent( string t ) => ( GUIContent )tempContentStringMethod.Invoke( null, new object[ ] { t } );


            /// <summary>
            /// Displays the object context menu.
            /// </summary>
            /// <param name="position">The position.</param>
            /// <param name="context">The context.</param>
            /// <param name="contextUserData">The context user data.</param>
            public static void DisplayObjectContextMenu( Rect position, UnityEngine.Object[ ] context, int contextUserData ) =>
                displayObjectContextMenuMethod?.Invoke( null, new object[ ] { position, context, contextUserData } );


            /// <summary>
            /// Ins the animation recording.
            /// </summary>
            /// <returns></returns>
            public static bool InAnimationRecording( ) => ( bool )inAnimationRecordingMethod.Invoke( null, new object[ ] { } );


            public static bool IsPropertyCandidate( Object target, string propertyPath ) =>
                ( bool )isPropertyCandidateMethod.Invoke( null, new object[ ] { target, propertyPath } );


        }

    }

}