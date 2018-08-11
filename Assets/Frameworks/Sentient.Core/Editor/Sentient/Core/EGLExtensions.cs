#region Copyright (C) 2017 Sentient Computing. All Rights Reserved.

// =============================================================================
//                              Sentient Computing
//                    Copyright (C) 2017, All Rights Reserved.
// 
//     This material is confidential and may not be disclosed in whole or in part
//     to any third party nor used in any manner whatsoever other than for the
//     purposes expressly consented to by Sentient Computing in writing.
//  
//     This material is also copyright and may not be reproduced, stored in a
//     retrieval system or transmitted in any form or by any means in whole or
//     in part without the express written consent of Sentient Computing.
// 
//     This copyright notice may not be removed from this file.
// 
// =============================================================================

#endregion


#if UNITY_EDITOR

using System;

using UnityEditor;

using UnityEngine;


namespace Sentient.CustomInspector
{

    [ Obsolete( "Use EditorExtensions instead.", false ) ]
    public static class EGLExtensions
    {

        /// <summary>
        /// Defines a horizontal layout.
        /// </summary>
        /// <param name="layoutBody">The contents of this layout.</param>
        /// <param name="layoutOptions">The layout options.</param>
        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Horizontal( Action layoutBody, params GUILayoutOption[ ] layoutOptions )
        {
            try
            {
                EditorGUILayout.BeginHorizontal( layoutOptions );
                layoutBody?.Invoke( );
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
            finally
            {
                EditorGUILayout.EndHorizontal( );
            }
        }

        /// <summary>
        /// Defines a horizontal layout.
        /// </summary>
        /// <param name="layoutBody">The contents of this layout.</param>
        /// <param name="style">The style.</param>
        /// <param name="layoutOptions">The layout options.</param>
        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Horizontal( Action layoutBody, GUIStyle style, params GUILayoutOption[ ] layoutOptions )
        {
            try
            {
                EditorGUILayout.BeginHorizontal( style, layoutOptions );
                layoutBody?.Invoke( );
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
            finally
            {
                EditorGUILayout.EndHorizontal( );
            }
        }

        /// <summary>
        /// Defines a horizontal layout of the specified <see cref="Color" />.
        /// </summary>
        /// <param name="layoutBody">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        /// <param name="layoutOptions">The layout options.</param>
        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Horizontal( Action layoutBody, Color layoutColor, params GUILayoutOption[ ] layoutOptions )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.backgroundColor = layoutColor;
                EditorGUILayout.BeginHorizontal( layoutOptions );
                layoutBody?.Invoke( );
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
            finally
            {
                EditorGUILayout.EndHorizontal( );
                GUI.backgroundColor = cachedColor;
            }
        }

        /// <summary>
        /// Defines a horizontal layout of the specified <see cref="Color" />.
        /// </summary>
        /// <param name="layoutBody">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        /// <param name="style">The style.</param>
        /// <param name="layoutOptions">The layout options.</param>
        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Horizontal( Action layoutBody, Color layoutColor, GUIStyle style, params GUILayoutOption[ ] layoutOptions )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.backgroundColor = layoutColor;
                EditorGUILayout.BeginHorizontal( style, layoutOptions );
                layoutBody?.Invoke( );
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
            finally
            {
                EditorGUILayout.EndHorizontal( );
                GUI.backgroundColor = cachedColor;
            }
        }

        /// <summary>
        /// Defines a vertical layout.
        /// </summary>
        /// <param name="layoutBody">The layout body.</param>
        /// <param name="layoutOptions">The layout options.</param>
        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Vertical( Action layoutBody, params GUILayoutOption[ ] layoutOptions )
        {
            try
            {
                EditorGUILayout.BeginVertical( layoutOptions );
                layoutBody?.Invoke( );
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
            finally
            {
                EditorGUILayout.EndVertical( );
            }
        }

        /// <summary>
        /// Defines a vertical layout.
        /// </summary>
        /// <param name="layoutBody">The layout body.</param>
        /// <param name="style">The style.</param>
        /// <param name="layoutOptions">The layout options.</param>
        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Vertical( Action layoutBody, GUIStyle style, params GUILayoutOption[ ] layoutOptions )
        {
            try
            {
                EditorGUILayout.BeginVertical( style, layoutOptions );
                layoutBody?.Invoke( );
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
            finally
            {
                EditorGUILayout.EndVertical( );
            }
        }

        /// <summary>
        /// Defines a vertical layout of the specified <see cref="Color" />.
        /// </summary>
        /// <param name="layoutBody">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        /// <param name="layoutOptions">The layout options.</param>
        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Vertical( Action layoutBody, Color layoutColor, params GUILayoutOption[ ] layoutOptions )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.color = layoutColor;
                EditorGUILayout.BeginVertical( layoutOptions );
                layoutBody?.Invoke( );
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
            finally
            {
                EditorGUILayout.EndVertical( );
                GUI.color = cachedColor;
            }
        }

        /// <summary>
        /// Defines a vertical layout of the specified <see cref="Color" />.
        /// </summary>
        /// <param name="layoutBody">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        /// <param name="style">The style.</param>
        /// <param name="layoutOptions">The layout options.</param>
        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Vertical( Action layoutBody, Color layoutColor, GUIStyle style, params GUILayoutOption[ ] layoutOptions )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.color = layoutColor;
                EditorGUILayout.BeginVertical( style, layoutOptions );
                layoutBody?.Invoke( );
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
            finally
            {
                EditorGUILayout.EndVertical( );
                GUI.color = cachedColor;
            }
        }

        /// <summary>
        /// Defines a scroll view.
        /// </summary>
        /// <param name="scrollBody">The scroll body.</param>
        /// <param name="scrollPosition">The scroll position.</param>
        /// <param name="showHorizontal">if set to <c>true</c> [always show horizontal].</param>
        /// <param name="showVertical">if set to <c>true</c> [always show vertical].</param>
        /// <param name="layoutOptions">The layout options.</param>
        public static void ScrollView( Action scrollBody, ref Vector2 scrollPosition, bool showHorizontal, bool showVertical,
            params GUILayoutOption[ ] layoutOptions )
        {
            var verticalScrollBar = GUI.skin.verticalScrollbar;
            var horizontalScrollBar = GUI.skin.horizontalScrollbar;

            try
            {
                if ( !showHorizontal )
                    GUI.skin.horizontalScrollbar = GUIStyle.none;

                if ( !showVertical )
                    GUI.skin.verticalScrollbar = GUIStyle.none;

                scrollPosition = EditorGUILayout.BeginScrollView( scrollPosition, showHorizontal, showVertical, layoutOptions );
                scrollBody?.Invoke( );
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

        public static void Foldout( string editorPref, string content, bool toggleOnLableClick, Action foldoutBody )
        {
            var showFoldout = EditorPrefs.GetBool( editorPref, false );

            var newShowFoldout = EditorGUILayout.Foldout( showFoldout, content, toggleOnLableClick );

            if ( showFoldout != newShowFoldout )
            {
                showFoldout = newShowFoldout;
                EditorPrefs.SetBool( editorPref, showFoldout );
            }

            if ( showFoldout )
            {
                foldoutBody?.Invoke( );
            }

        }

    }

}

#endif
