using System;

using UnityEditor;

using UnityEngine;


namespace Sentient.CustomInspector
{


    public static partial class EditorExtensions
    {


        /// <summary>
        /// Centers the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        public static void Center( Action content )
        {
            try
            {
                GUILayout.FlexibleSpace( );
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                GUILayout.FlexibleSpace( );
            }
        }




        /// <summary>
        /// Indents inspector content.
        /// </summary>
        /// <param name="content">The indented content.</param>
        /// <param name="levels">The levels.</param>
        public static void Indent( int levels, Action content )
        {
            try
            {
                EditorGUI.indentLevel += levels;
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                EditorGUI.indentLevel -= levels;
            }
        }




        /// <summary>
        /// Disables inspector content.
        /// </summary>
        /// <param name="content">The content to disable.</param>
        public static void Disable( Action content )
        {
            try
            {
                GUI.enabled = false;
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                GUI.enabled = true;
            }
        }

        /// <summary>
        /// Disables inspector content.
        /// </summary>
        /// <param name="disable">if set to <c>true</c> [disable].</param>
        /// <param name="content">The content to disable.</param>
        public static void Disable( bool disable, Action content )
        {
            try
            {
                GUI.enabled = disable;
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                GUI.enabled = true;
            }
        }




        /// <summary>
        /// Renders the captured content at the specified depth.
        /// </summary>
        /// <param name="depth">The depth.</param>
        /// <param name="content">The content.</param>
        public static void Depth( int depth, Action content )
        {
            var cachedDepth = GUI.depth;

            try
            {
                GUI.depth = depth;
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                GUI.depth = cachedDepth;
            }
        }




        /// <summary>
        /// Groups the specified content.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="content">The content.</param>
        public static void Group( Rect position, Action content )
        {
            try
            {
                GUI.BeginGroup( position );
                content?.Invoke( );
            }
            catch ( Exception e )
            {
                Debug.LogError( e );
            }
            finally
            {
                GUI.EndGroup( );
            }
        }

    }


}
