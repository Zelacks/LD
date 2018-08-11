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
        /// Defines a horizontal layout.
        /// </summary>
        /// <param name="content">The contents of this layout.</param>
        public static void Horizontal( Action content )
        {
            try
            {
                EditorGUILayout.BeginHorizontal( );
                content?.Invoke( );
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
        /// <param name="style">The style.</param>
        /// <param name="content">The contents of this layout.</param>
        public static void Horizontal( GUIStyle style, Action content )
        {
            try
            {
                EditorGUILayout.BeginHorizontal( style );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="options">The layout options.</param>
        public static void Horizontal( IEnumerable< GUILayoutOption > options, Action content )
        {
            try
            {
                EditorGUILayout.BeginHorizontal( options.ToArray( ) );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="style">The style.</param>
        /// <param name="options">The layout options.</param>
        public static void Horizontal( GUIStyle style, IEnumerable< GUILayoutOption > options, Action content )
        {
            try
            {
                EditorGUILayout.BeginHorizontal( style, options.ToArray( ) );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        public static void Horizontal( Color layoutColor, Action content )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.backgroundColor = layoutColor;
                EditorGUILayout.BeginHorizontal( );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        /// <param name="options">The layout options.</param>
        public static void Horizontal( Color layoutColor, IEnumerable< GUILayoutOption > options, Action content )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.backgroundColor = layoutColor;
                EditorGUILayout.BeginHorizontal( options.ToArray( ) );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        /// <param name="style">The style.</param>
        /// <param name="options">The layout options.</param>
        public static void Horizontal( Color layoutColor, GUIStyle style, IEnumerable< GUILayoutOption > options, Action content )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.backgroundColor = layoutColor;
                EditorGUILayout.BeginHorizontal( style, options.ToArray( ) );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        public static void Vertical( Action content )
        {
            try
            {
                EditorGUILayout.BeginVertical( );
                content?.Invoke( );
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
        /// <param name="style">The style.</param>
        /// <param name="content">The contents of this layout.</param>
        public static void Vertical( GUIStyle style, Action content )
        {
            try
            {
                EditorGUILayout.BeginVertical( style );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="options">The layout options.</param>
        public static void Vertical( IEnumerable< GUILayoutOption > options, Action content )
        {
            try
            {
                EditorGUILayout.BeginVertical( options.ToArray( ) );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="style">The style.</param>
        /// <param name="options">The layout options.</param>
        public static void Vertical( GUIStyle style, IEnumerable< GUILayoutOption > options, Action content )
        {
            try
            {
                EditorGUILayout.BeginVertical( style, options.ToArray( ) );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        public static void Vertical( Color layoutColor, Action content )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.color = layoutColor;
                EditorGUILayout.BeginVertical( );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        /// <param name="options">The layout options.</param>
        public static void Vertical( Color layoutColor, IEnumerable< GUILayoutOption > options, Action content )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.color = layoutColor;
                EditorGUILayout.BeginVertical( options.ToArray( ) );
                content?.Invoke( );
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
        /// <param name="content">The contents of this layout.</param>
        /// <param name="layoutColor">Color of the layout.</param>
        /// <param name="style">The style.</param>
        /// <param name="options">The layout options.</param>
        public static void Vertical( Color layoutColor, GUIStyle style, IEnumerable< GUILayoutOption > options, Action content )
        {
            var cachedColor = GUI.color;

            try
            {
                GUI.color = layoutColor;
                EditorGUILayout.BeginVertical( style, options.ToArray( ) );
                content?.Invoke( );
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
        /// Defines an area.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="content">The content.</param>
        public static void Area(Rect rect, Action content)
        {
            try
            {
                GUILayout.BeginArea(rect);
                content?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                GUILayout.EndArea();
            }
        }

        /// <summary>
        /// Defines an area.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="style">The style.</param>
        /// <param name="content">The content.</param>
        public static void Area(Rect rect, GUIStyle style, Action content)
        {
            try
            {
                GUILayout.BeginArea(rect, style);
                content?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                GUILayout.EndArea();
            }
        }

        /// <summary>
        /// Defines an area.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="guiContent">Content of the GUI.</param>
        /// <param name="style">The style.</param>
        /// <param name="content">The content.</param>
        public static void Area(Rect rect, GUIContent guiContent, GUIStyle style, Action content)
        {
            try
            {
                GUILayout.BeginArea(rect, guiContent, style);
                content?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            finally
            {
                GUILayout.EndArea();
            }
        }
    }

}