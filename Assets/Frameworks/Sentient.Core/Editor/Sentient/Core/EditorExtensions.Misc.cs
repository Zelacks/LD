using System;

using UnityEditor;

using UnityEngine;


namespace Sentient.CustomInspector
{

    public static partial class EditorExtensions
    {

        //constructor
        public static class MiscStyles
        {

            public static GUIStyle HorizontalSeperator = null;
            public static GUIStyle VerticalSeperator = null;

            static MiscStyles( )
            {
                HorizontalSeperator = new GUIStyle( "box" );
                HorizontalSeperator.border.top = HorizontalSeperator.border.bottom = 1;
                HorizontalSeperator.margin.top = HorizontalSeperator.margin.bottom = 1;
                HorizontalSeperator.margin.left = HorizontalSeperator.margin.right = 0;
                HorizontalSeperator.padding.top = HorizontalSeperator.padding.bottom = 1;
                HorizontalSeperator.padding.left = HorizontalSeperator.padding.right = 0;

                VerticalSeperator = new GUIStyle( "box" );
                VerticalSeperator.border.left = VerticalSeperator.border.right = 1;
                VerticalSeperator.margin.left = VerticalSeperator.margin.right = 1;
                VerticalSeperator.margin.top = VerticalSeperator.margin.bottom = 0;
                VerticalSeperator.padding.left = VerticalSeperator.padding.right = 1;
                VerticalSeperator.padding.top = VerticalSeperator.padding.bottom = 0;
            }

        }

        public static void Seperator( Rule rule = Rule.Horizontal )
        {
            switch ( rule )
            {
                case Rule.Horizontal:

                    GUILayout.Box( GUIContent.none, MiscStyles.HorizontalSeperator, GUILayout.ExpandWidth( true ), GUILayout.Height( 1f ) );
                    break;

                case Rule.Vertical:

                    GUILayout.Box( GUIContent.none, MiscStyles.VerticalSeperator, GUILayout.ExpandHeight( true ), GUILayout.Width( 1f ) );
                    break;
            }
        }

        public static Rect GUIToScreenRect( Rect guiRect )
        {
            Vector2 screenPoint = GUIUtility.GUIToScreenPoint( new Vector2( guiRect.x, guiRect.y ) );
            guiRect.x = screenPoint.x;
            guiRect.y = screenPoint.y;
            return guiRect;
        }

        public static void DrawVerticalSplitter( Rect dragRect )
        {
            if ( Event.current.type != EventType.Repaint )
                return;
            Color color = GUI.color;
            GUI.color *= !EditorGUIUtility.isProSkin ? new Color( 0.6f, 0.6f, 0.6f, 1.333f ) : new Color( 0.12f, 0.12f, 0.12f, 1.333f );
            GUI.DrawTexture( new Rect( dragRect.x, dragRect.y + 1f, dragRect.width, 1f ), ( Texture )EditorGUIUtility.whiteTexture );
            GUI.color = color;
        }


        public enum Rule
        {

            Horizontal,
            Vertical

        }



        public static void OnChange( Action drawGui, Action onChange )
        {
            EditorGUI.BeginChangeCheck( );
            drawGui( );
            if ( EditorGUI.EndChangeCheck( ) )
                onChange( );
        }

        public static void Profile( Action drawGui, Action< TimeSpan > handleResults )
        {
            var start = DateTime.Now;
            drawGui( );
            var span = DateTime.Now - start;
            handleResults( span );
        }

        public static void ProfileMilliseconds( string profileName, Action drawGui )
        {
            var start = DateTime.Now;
            drawGui( );
            var span = DateTime.Now - start;
            Debug.Log( $"{profileName} took: {span.TotalMilliseconds} ms" );
        }

    }

}
