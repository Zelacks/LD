using System;

using UnityEditor;

using UnityEngine;


namespace Sentient.CustomInspector
{

    public static partial class EditorExtensions
    {

        /// <summary>
        /// Creates a foldout in the inspector.
        /// </summary>
        /// <param name="editorPref">The editor preference.</param>
        /// <param name="guiContent">Content of the GUI.</param>
        /// <param name="toggleOnLableClick">if set to <c>true</c> [toggle on lable click].</param>
        /// <param name="content">The content.</param>
        public static void Foldout( string editorPref, GUIContent guiContent, bool toggleOnLableClick, Action content )
        {
            var showFoldout = EditorPrefs.GetBool( editorPref, true );

            var newShowFoldout = EditorGUILayout.Foldout( showFoldout, guiContent, toggleOnLableClick );

            if ( showFoldout != newShowFoldout )
            {
                showFoldout = newShowFoldout;
                EditorPrefs.SetBool( editorPref, showFoldout );
            }

            if ( showFoldout )
            {
                content?.Invoke( );
            }

        }

        /// <summary>
        /// Creates a foldout in the inspector.
        /// </summary>
        /// <param name="editorPref">The editor preference.</param>
        /// <param name="guiContent">Content of the GUI.</param>
        /// <param name="toggleOnLableClick">if set to <c>true</c> [toggle on lable click].</param>
        /// <param name="style">The style.</param>
        /// <param name="content">The content.</param>
        public static void Foldout( string editorPref, GUIContent guiContent, bool toggleOnLableClick, GUIStyle style, Action content )
        {
            var showFoldout = EditorPrefs.GetBool( editorPref, true );

            var newShowFoldout = EditorGUILayout.Foldout( showFoldout, guiContent, toggleOnLableClick, style );

            if ( showFoldout != newShowFoldout )
            {
                showFoldout = newShowFoldout;
                EditorPrefs.SetBool( editorPref, showFoldout );
            }

            if ( showFoldout )
            {
                content?.Invoke( );
            }

        }

        /// <summary>
        /// Creates a foldout in the inspector.
        /// </summary>
        /// <param name="editorPref">The editor preference.</param>
        /// <param name="label">The label.</param>
        /// <param name="toggleOnLableClick">if set to <c>true</c> [toggle on lable click].</param>
        /// <param name="content">The content.</param>
        public static void Foldout( string editorPref, string label, bool toggleOnLableClick, Action content )
        {
            var showFoldout = EditorPrefs.GetBool( editorPref, true );

            var newShowFoldout = EditorGUILayout.Foldout( showFoldout, label, toggleOnLableClick );

            if ( showFoldout != newShowFoldout )
            {
                showFoldout = newShowFoldout;
                EditorPrefs.SetBool( editorPref, showFoldout );
            }

            if ( showFoldout )
            {
                content?.Invoke( );
            }

        }

        /// <summary>
        /// Creates a foldout in the inspector.
        /// </summary>
        /// <param name="editorPref">The editor preference.</param>
        /// <param name="label">The label.</param>
        /// <param name="toggleOnLableClick">if set to <c>true</c> [toggle on lable click].</param>
        /// <param name="style">The style.</param>
        /// <param name="content">The content.</param>
        public static void Foldout( string editorPref, string label, bool toggleOnLableClick, GUIStyle style, Action content )
        {
            var showFoldout = EditorPrefs.GetBool( editorPref, true );

            var newShowFoldout = EditorGUILayout.Foldout( showFoldout, label, toggleOnLableClick, style );

            if ( showFoldout != newShowFoldout )
            {
                showFoldout = newShowFoldout;
                EditorPrefs.SetBool( editorPref, showFoldout );
            }

            if ( showFoldout )
            {
                content?.Invoke( );
            }

        }

    }

}
