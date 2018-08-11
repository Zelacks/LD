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
        /// Creates an inspector button.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="onClick">The action to invoke on button clicked.</param>
        public static void Button( string text, Action onClick )
        {
            try
            {
                if ( GUILayout.Button( text ) )
                {
                    onClick?.Invoke( );
                }
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
        }

        /// <summary>
        /// Creates an inspector button.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="options">The options.</param>
        /// <param name="onClick">The action to invoke on button clicked.</param>
        public static void Button( string text, IEnumerable< GUILayoutOption > options, Action onClick )
        {
            try
            {
                if ( GUILayout.Button( text, options.ToArray( ) ) )
                {
                    onClick?.Invoke( );
                }
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
        }

        /// <summary>
        /// Creates an inspector button.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="guiStyle">The GUI style.</param>
        /// <param name="options">The options.</param>
        /// <param name="onClick">The action to invoke on button clicked.</param>
        public static void Button( string text, GUIStyle guiStyle, IEnumerable< GUILayoutOption > options, Action onClick )
        {
            try
            {
                if ( GUILayout.Button( text, guiStyle, options.ToArray( ) ) )
                {
                    onClick?.Invoke( );
                }
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
        }

        /// <summary>
        /// Creates an inspector button.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="onClick">The action to invoke on button clicked.</param>
        public static void Button( GUIContent content, Action onClick )
        {
            try
            {
                if ( GUILayout.Button( content ) )
                {
                    onClick?.Invoke( );
                }
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
        }

        /// <summary>
        /// Creates an inspector button.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="options">The options.</param>
        /// <param name="onClick">The action to invoke on button clicked.</param>
        public static void Button( GUIContent content, IEnumerable< GUILayoutOption > options, Action onClick )
        {
            try
            {
                if ( GUILayout.Button( content, options.ToArray( ) ) )
                {
                    onClick?.Invoke( );
                }
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
        }

        /// <summary>
        /// Creates an inspector dropwdown button.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="content">The content.</param>
        /// <param name="style">The GUI style.</param>
        /// <param name="onClick">The action to invoke on button clicked.</param>
        public static void DropdownButton( Rect rect, GUIContent content, GUIStyle style, Action onClick )
        {
            try
            {
                if ( EditorGUI.DropdownButton( rect, content, FocusType.Passive, style ) )
                {
                    onClick?.Invoke( );
                }
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
        }

    }

}
