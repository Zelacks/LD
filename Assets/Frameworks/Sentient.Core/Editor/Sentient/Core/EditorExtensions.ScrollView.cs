using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;


namespace Sentient.CustomInspector
{

    public static partial class EditorExtensions
    {

        /// <summary>
        /// Defines a scroll view.
        /// </summary>
        /// <param name="scrollPosition">The scroll position.</param>
        /// <param name="showHorizontal">if set to <c>true</c> [always show horizontal].</param>
        /// <param name="showVertical">if set to <c>true</c> [always show vertical].</param>
        /// <param name="content">The content.</param>
        public static void ScrollView( ref Vector2 scrollPosition, bool showHorizontal, bool showVertical, Action content )
        {
            var verticalScrollBar = GUI.skin.verticalScrollbar;
            var horizontalScrollBar = GUI.skin.horizontalScrollbar;

            try
            {
                if ( !showHorizontal )
                    GUI.skin.horizontalScrollbar = GUIStyle.none;

                if ( !showVertical )
                    GUI.skin.verticalScrollbar = GUIStyle.none;

                scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition, showHorizontal, showVertical );
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                EditorGUILayout.EndScrollView( );
                GUI.skin.horizontalScrollbar = horizontalScrollBar;
                GUI.skin.verticalScrollbar = verticalScrollBar;
            }
        }

        /// <summary>
        /// Defines a scroll view.
        /// </summary>
        /// <param name="scrollPosition">The scroll position.</param>
        /// <param name="showHorizontal">if set to <c>true</c> [always show horizontal].</param>
        /// <param name="showVertical">if set to <c>true</c> [always show vertical].</param>
        /// <param name="options">The layout options.</param>
        /// <param name="content">The content.</param>
        public static void ScrollView( ref Vector2 scrollPosition, bool showHorizontal, bool showVertical,
            IEnumerable< GUILayoutOption > options, Action content )
        {
            var verticalScrollBar = GUI.skin.verticalScrollbar;
            var horizontalScrollBar = GUI.skin.horizontalScrollbar;

            try
            {
                if ( !showHorizontal )
                    GUI.skin.horizontalScrollbar = GUIStyle.none;

                if ( !showVertical )
                    GUI.skin.verticalScrollbar = GUIStyle.none;

                scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition, showHorizontal, showVertical, options.ToArray( ) );
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                EditorGUILayout.EndScrollView( );
                GUI.skin.horizontalScrollbar = horizontalScrollBar;
                GUI.skin.verticalScrollbar = verticalScrollBar;
            }
        }

        /// <summary>
        /// Defines a scroll view.
        /// </summary>
        /// <param name="scrollPosition">The scroll position.</param>
        /// <param name="disable">Disable the specified scroll bars.</param>
        /// <param name="autoHide">Auto hides the specified scroll bars.</param>
        /// <param name="content">The content.</param>
        public static void ScrollView( ref Vector2 scrollPosition, ScrollBar disable, ScrollBar autoHide, Action content )
        {
            var verticalScrollBar = GUI.skin.verticalScrollbar;
            var horizontalScrollBar = GUI.skin.horizontalScrollbar;

            try
            {
                if ( disable != ScrollBar.Vertical )
                    GUI.skin.horizontalScrollbar = GUIStyle.none;

                if ( disable != ScrollBar.Horizontal )
                    GUI.skin.verticalScrollbar = GUIStyle.none;

                scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition, autoHide == ScrollBar.Vertical, autoHide == ScrollBar.Horizontal );
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                EditorGUILayout.EndScrollView( );
                GUI.skin.horizontalScrollbar = horizontalScrollBar;
                GUI.skin.verticalScrollbar = verticalScrollBar;
            }
        }

        /// <summary>
        /// Defines a scroll view.
        /// </summary>
        /// <param name="scrollPosition">The scroll position.</param>
        /// <param name="disable">Disable the specified scroll bars.</param>
        /// <param name="autoHide">Auto hides the specified scroll bars.</param>
        /// <param name="options">The layout options.</param>
        /// <param name="content">The content.</param>
        public static void ScrollView( ref Vector2 scrollPosition, ScrollBar disable, ScrollBar autoHide,
            IEnumerable< GUILayoutOption > options, Action content )
        {
            var verticalScrollBar = GUI.skin.verticalScrollbar;
            var horizontalScrollBar = GUI.skin.horizontalScrollbar;

            try
            {
                if ( disable != ScrollBar.Vertical )
                    GUI.skin.horizontalScrollbar = GUIStyle.none;

                if ( disable != ScrollBar.Horizontal )
                    GUI.skin.verticalScrollbar = GUIStyle.none;

                scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition, autoHide == ScrollBar.Vertical, autoHide == ScrollBar.Horizontal, options.ToArray( ) );
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                EditorGUILayout.EndScrollView( );
                GUI.skin.horizontalScrollbar = horizontalScrollBar;
                GUI.skin.verticalScrollbar = verticalScrollBar;
            }
        }

        public enum ScrollBar
        {

            Both,
            Horizontal,
            Vertical

        }

    }

}