using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Zenject;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
#if UNITY_EDITOR || UNITY_STANDALONE
using System.Diagnostics;
using System.IO;
#endif



namespace Sentient.Unity
{

    /// <summary>
    /// A massive library of re-usable Unity helper functions.
    /// Author: Chris Hellsten
    /// </summary>
    public static class UnityUtil
    {

        /// <summary>
        /// Generates a random vector in the range from either [0,0,0,0] to [x,y,z,w] or from -[x,y,z,w] to [x,y,z,w].
        /// </summary>
        /// <param name="range">The range vectors.</param>
        /// <param name="allowNegatives">Allow negatives or do we start from zero mins?</param>
        /// <returns>A randomised Vector.</returns>
        public static Vector4 RandomVectorInRange( Vector4 range, bool allowNegatives = true )
        {
            return new Vector4(
                Random.Range( allowNegatives ? -range.x : 0.0f, range.x ),
                Random.Range( allowNegatives ? -range.y : 0.0f, range.y ),
                Random.Range( allowNegatives ? -range.z : 0.0f, range.z ),
                Random.Range( allowNegatives ? -range.w : 0.0f, range.w ) );
        }

        /// <summary>
        /// Generates a random vector in the range from either [0,0,0,0] to [x,y,z,w] or from -[x,y,z,w] to [x,y,z,w].
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="allowNegatives">Allow negatives or do we start from zero mins?</param>
        /// <returns>A randomised Vector.</returns>
        public static Vector4 RandomInRange( this Vector4 vec, bool allowNegatives = true )
        {
            return RandomVectorInRange( vec, allowNegatives );
        }

        /// <summary>
        /// Generates a random vector in the range from either [0,0,0] to [x,y,z] or from -[x,y,z] to [x,y,z].
        /// </summary>
        /// <param name="range">The range vectors.</param>
        /// <param name="allowNegatives">Allow negatives or do we start from zero mins?</param>
        /// <returns>A randomised Vector.</returns>
        public static Vector3 RandomVectorInRange( Vector3 range, bool allowNegatives = true )
        {
            return new Vector3(
                Random.Range( allowNegatives ? -range.x : 0.0f, range.x ),
                Random.Range( allowNegatives ? -range.y : 0.0f, range.y ),
                Random.Range( allowNegatives ? -range.z : 0.0f, range.z ) );
        }

        /// <summary>
        /// Generates a random vector in the range from either [0,0,0] to [x,y,z] or from -[x,y,z] to [x,y,z].
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="allowNegatives">Allow negatives or do we start from zero mins?</param>
        /// <returns>A randomised Vector.</returns>
        public static Vector3 RandomInRange( this Vector3 vec, bool allowNegatives = true )
        {
            return RandomVectorInRange( vec, allowNegatives );
        }

        /// <summary>
        /// Generates a random vector in the range from either [0,0] to [x,y] or from -[x,y] to [x,y].
        /// </summary>
        /// <param name="range">The range vectors.</param>
        /// <param name="allowNegatives">Allow negatives or do we start from zero mins?</param>
        /// <returns>A randomised Vector.</returns>
        public static Vector2 RandomVectorInRange( Vector2 range, bool allowNegatives = true )
        {
            return new Vector2(
                Random.Range( allowNegatives ? -range.x : 0.0f, range.x ),
                Random.Range( allowNegatives ? -range.y : 0.0f, range.y ) );
        }

        /// <summary>
        /// Generates a random vector in the range from either [0,0] to [x,y] or from -[x,y] to [x,y].
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="allowNegatives">Allow negatives or do we start from zero mins?</param>
        /// <returns>A randomised Vector.</returns>
        public static Vector2 RandomInRange( this Vector2 vec, bool allowNegatives = true )
        {
            return RandomVectorInRange( vec, allowNegatives );
        }

        /// <summary>
        /// Generates a random float in the range from either [0, x] or from [-x, x].
        /// </summary>
        /// <param name="range">The range vectors.</param>
        /// <param name="allowNegatives">Allow negatives or do we start from zero mins?</param>
        /// <returns>A randomised Vector.</returns>
        public static float RandomFloatPlusMinus( float range, bool allowNegatives = true )
        {
            return Random.Range( allowNegatives ? -range : 0.0f, range );
        }

        /// <summary>
        /// Generates a random float in the range from either [0, x] or from [-x, x].
        /// </summary>
        /// <param name="range">The range vectors.</param>
        /// <param name="allowNegatives">Allow negatives or do we start from zero mins?</param>
        /// <returns>A randomised Vector.</returns>
        public static float RandomInRange( this float range, bool allowNegatives = true )
        {
            return RandomFloatPlusMinus( range, allowNegatives );
        }



        /// <summary>
        /// 
        /// </summary>
        private const int DefaultNormalIterations = 16;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="stdDev"></param>
        /// <returns></returns>
        public static float RandomNormal( float mean, float stdDev )
        {
            return RandomNormal( mean, stdDev, DefaultNormalIterations );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mean"></param>
        /// <param name="stdDev"></param>
        /// <param name="iters"></param>
        /// <returns></returns>
        public static float RandomNormal( float mean, float stdDev, int iters )
        {
            float result = 0;

            for ( var i = 0; i < iters; i++ )
                result += Random.Range( -1f, 1f );

            result *= stdDev * 2;
            result += mean;

            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static T RandomItem< T >( this List< T > list )
        {
            return list.Count == 0 ? default( T ) : list[ Random.Range( 0, list.Count ) ];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T RandomItem< T >( this T[ ] array )
        {
            return array.Length == 0 ? default( T ) : array[ Random.Range( 0, array.Length ) ];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public static T RandomItem< T >( this List< T > list, int startIndex, int endIndex )
        {
            return list[ Random.Range( startIndex, endIndex ) ];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public static T RandomItem< T >( this T[ ] array, int startIndex, int endIndex )
        {
            return array[ Random.Range( startIndex, endIndex ) ];
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="chance"></param>
        /// <returns></returns>
        public static bool Roll( float chance )
        {
            return Random.Range( 0f, 1f ) < chance;
        }

        


        public static Vector2 MultiplyComponentWise( this Vector2 a, Vector2 b )
        {
            return new Vector2( a.x * b.x, a.y * b.y );
        }

        public static Vector2 InvertComponentWise( this Vector2 a )
        {
            return new Vector2( 1 / a.x, 1 / a.y );
        }

        public static Vector2 DivideComponentWise( this Vector2 a, Vector2 b )
        {
            return a.MultiplyComponentWise( b.InvertComponentWise( ) );
        }

        public static Vector2 ClampComponentWise( this Vector2 a, float min, float max )
        {
            return new Vector2( Mathf.Clamp( a.x, min, max ), Mathf.Clamp( a.y, min, max ) );
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        public static Vector4[ ] GenMeshTangents( Mesh mesh )
        {
            //speed up math by copying the mesh arrays
            var triangles = mesh.triangles;
            var vertices = mesh.vertices;
            var uv = mesh.uv;
            var normals = mesh.normals;

            //variable definitions
            var triangleCount = triangles.Length;
            var vertexCount = vertices.Length;

            var tan1 = new Vector3[ vertexCount ];
            var tan2 = new Vector3[ vertexCount ];

            var tangents = new Vector4[ vertexCount ];

            for ( long a = 0; a < triangleCount; a += 3 )
            {
                long i1 = triangles[ a + 0 ];
                long i2 = triangles[ a + 1 ];
                long i3 = triangles[ a + 2 ];

                var v1 = vertices[ i1 ];
                var v2 = vertices[ i2 ];
                var v3 = vertices[ i3 ];

                var w1 = uv[ i1 ];
                var w2 = uv[ i2 ];
                var w3 = uv[ i3 ];

                var x1 = v2.x - v1.x;
                var x2 = v3.x - v1.x;
                var y1 = v2.y - v1.y;
                var y2 = v3.y - v1.y;
                var z1 = v2.z - v1.z;
                var z2 = v3.z - v1.z;

                var s1 = w2.x - w1.x;
                var s2 = w3.x - w1.x;
                var t1 = w2.y - w1.y;
                var t2 = w3.y - w1.y;

                var r = 1.0f / ( s1 * t2 - s2 * t1 );

                var sdir = new Vector3( ( t2 * x1 - t1 * x2 ) * r, ( t2 * y1 - t1 * y2 ) * r, ( t2 * z1 - t1 * z2 ) * r );
                var tdir = new Vector3( ( s1 * x2 - s2 * x1 ) * r, ( s1 * y2 - s2 * y1 ) * r, ( s1 * z2 - s2 * z1 ) * r );

                tan1[ i1 ] += sdir;
                tan1[ i2 ] += sdir;
                tan1[ i3 ] += sdir;

                tan2[ i1 ] += tdir;
                tan2[ i2 ] += tdir;
                tan2[ i3 ] += tdir;
            }


            for ( long a = 0; a < vertexCount; ++a )
            {
                var n = normals[ a ];
                var t = tan1[ a ];
                Vector3.OrthoNormalize( ref n, ref t );

                tangents[ a ].x = t.x;
                tangents[ a ].y = t.y;
                tangents[ a ].z = t.z;

                tangents[ a ].w = Vector3.Dot( Vector3.Cross( n, t ), tan2[ a ] ) < 0.0f ? -1.0f : 1.0f;
            }

            return tangents;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static float TemporalExp( float exp )
        {
            return 1.0f - Mathf.Pow( 1.0f - exp, Time.deltaTime );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dampingPerSecond"></param>
        /// <returns></returns>
        public static float DampingCoefficient( float dampingPerSecond )
        {
            return Mathf.Pow( 1.0f - dampingPerSecond, Time.deltaTime );
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="inp"></param>
        /// <returns></returns>
        public static float RadianDistance( float inp )
        {
            while ( inp < 0 )
                inp += Mathf.PI * 2.0f;
            while ( inp >= Mathf.PI * 2.0f )
                inp -= Mathf.PI * 2.0f;
            if ( inp > Mathf.PI )
                inp = Mathf.PI * 2 - inp;

            return inp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inp"></param>
        /// <returns></returns>
        public static float DegreeDistance( float inp )
        {
            while ( inp < 0 )
                inp += 360.0f;
            while ( inp >= 360.0f )
                inp -= 360.0f;
            if ( inp > 180.0f )
                inp = 360.0f - inp;

            return inp;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="c"></param>
        /// <param name="includeSelf"></param>
        /// <returns></returns>
        public static T GetComponentInParent< T >( this Component c, bool includeSelf = true )
            where T : class
        {
            T result = null;
            var xf = c.transform;

            if ( !includeSelf )
                xf = xf.parent;

            while ( result == null && xf != null )
            {
                result = xf.GetComponent( typeof( T ) ) as T;
                xf = xf.parent;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xf"></param>
        /// <param name="T"></param>
        /// <param name="includeSelf"></param>
        /// <returns></returns>
        public static Component GetComponentInParent( this Transform xf, Type T, bool includeSelf = true )
        {
            Component result = null;

            if ( !includeSelf )
                xf = xf.parent;

            while ( result == null && xf != null )
            {
                result = xf.GetComponent( T );
                xf = xf.parent;
            }

            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        public static T Create< T >( T original )
            where T : Component
        {
            var result = Object.Instantiate( original.gameObject ).GetComponent< T >( );
            TrimClone( result.gameObject );
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static GameObject Create( GameObject original )
        {
            var result = Object.Instantiate( original );
            TrimClone( result );
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static GameObject Create( string resource )
        {
            var result = ( GameObject )Object.Instantiate( Resources.Load( resource ) );
            TrimClone( result );
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public static void TrimClone( GameObject obj )
        {
            obj.name = obj.name.Substring( 0, obj.name.Length - 7 );
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsParentOf( this Transform a, Transform b )
        {
            do
            {
                b = b.parent;
            } while ( a != b && b != null );

            return a == b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="stepUp"></param>
        /// <param name="stepDown"></param>
        /// <returns></returns>
        public static bool GetAncestorLink( this Transform a, Transform b, out int stepUp, out int stepDown )
        {
            var commonParent = a;
            stepUp = 0;

            while ( commonParent != null )
            {
                stepDown = GetChildDistance( commonParent, b );
                if ( stepDown >= 0 )
                    return true;

                commonParent = commonParent.parent;
                stepUp++;
            }

            stepUp = -1;
            stepDown = -1;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GetChildDistance( this Transform a, Transform b )
        {
            var t = b;
            var result = 0;

            while ( t != null && a != t )
            {
                t = t.parent;
                result++;
            }

            return a == t ? result : -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GetParentalDistance( this Transform a, Transform b )
        {
            var t = a;
            var result = 0;

            while ( t != null && b != t )
            {
                t = t.parent;
                result++;
            }

            return b == t ? result : -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="stepDown"></param>
        /// <param name="name"></param>
        /// <param name="stepUp"></param>
        /// <returns></returns>
        public static Transform SearchAncestorTree( this Transform a, int stepUp, int stepDown, string name )
        {
            var t = a;
            while ( stepUp > 0 )
            {
                stepUp--;
                t = t.parent;
            }

            return FindChild( t, stepDown, name );
        }

        /// <summary>
        /// Gets the full name of a transform including the name of its parents up to the root object.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="delimiter"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string GetFullNodeName( this Transform a, string delimiter, string suffix = "" )
        {
            while ( true )
            {
                string result = $"{a.name}{( string.IsNullOrEmpty( suffix ) ? string.Empty : delimiter )}{suffix}";
                if ( a.parent == null ) return result;
                a = a.parent;
                suffix = result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="depth"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static Transform FindChild( Transform a, int depth, string name )
        {
            if ( depth == 0 )
                return a.name == name ? a : null;

            return ( from Transform t in a select FindChild( t, depth - 1, name ) ).FirstOrDefault( r => r != null );

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsChildOf( this Transform a, Transform b )
        {
            return b.IsParentOf( a );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsParentOf( this GameObject a, GameObject b )
        {
            return a.transform.IsParentOf( b.transform );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsChildOf( this GameObject a, GameObject b )
        {
            return b.transform.IsParentOf( a.transform );
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static List< GameObject > GetChildren( this GameObject src, bool recursive )
        {
            var result = new List< GameObject >( );

            for ( var i = 0; i < src.transform.childCount; i++ )
            {
                result.Add( src.transform.GetChild( i ).gameObject );
                if ( recursive )
                    result.AddAll( src.transform.GetChild( i ).GetChildren( true ) );
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public static List< Transform > GetChildren( this Transform src, bool recursive )
        {
            var result = new List< Transform >( );

            for ( var i = 0; i < src.childCount; i++ )
            {
                result.Add( src.GetChild( i ) );
                if ( recursive )
                    result.AddAll( src.GetChild( i ).GetChildren( true ) );
            }

            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="alpha"></param>
        public static void SetAlpha( this Material t, float alpha )
        {
            t.color = new Color( t.color.r, t.color.g, t.color.b, alpha );
        }



        /// <summary>
        /// Copies a component to another GameObject.
        /// Note:: only the public fields of the object will copy.
        /// </summary>
        /// <typeparam name="T">The type to copy.</typeparam>
        /// <param name="comp">The component to copy (extension method).</param>
        /// <param name="destination">The gameobject to copy to.</param>
        /// <returns>The instance of the new copy that has been attached to the GameObject, or null if the copy failed.</returns>
        public static T CopyTo< T >( this T comp, GameObject destination )
            where T : Component
        {
            var type = comp.GetType( );

            var copy = destination.GetComponent< T >( ) ?? destination.AddComponent< T >( );
            if ( copy == null )
                return null;

            var fields = type.GetFields( );
            foreach ( var field in fields )
                field.SetValue( copy, field.GetValue( comp ) );

            return copy;
        }



        /// <summary>
        /// A value that represents "Close Enough" when comparing floating point values.
        /// </summary>
        private const float Epsilon = 0.00001f;

        /// <summary>
        /// Extension method for check if two floats are approximately equal.
        /// </summary>
        /// <param name="a">The extend float.</param>
        /// <param name="b">Compare to this float.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the values are approximately equal.</returns>
        public static bool Approx( this float a, float b, float tolerance = Epsilon )
        {
            return Mathf.Abs( a - b ) < tolerance;
        }

        /// <summary>
        /// Extension method for check if two floats are approximately equal.
        /// </summary>
        /// <param name="a">The extended float.</param>
        /// <param name="min">The approximate minimum value.</param>
        /// <param name="max">The approximate maximum value.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the value is approximately between min and max.</returns>
        public static bool ApproxInRange( this float a, float min, float max, float tolerance = Epsilon )
        {
            return a + tolerance >= min && a - tolerance <= max;
        }



        /// <summary>
        /// Extension method for check if two Vector2's are approximately equal.
        /// </summary>
        /// <param name="a">The extended Vector.</param>
        /// <param name="b">Compare to this Vector.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the values are approximately equal.</returns>
        public static bool Approx( this Vector2 a, Vector2 b, float tolerance = Epsilon )
        {
            return Approx( a.x, b.x, tolerance ) && Approx( a.y, b.y, tolerance );
        }

        /// <summary>
        /// Extension method for check if two Vector3's are approximately equal.
        /// </summary>
        /// <param name="a">The extended Vector.</param>
        /// <param name="b">Compare to this Vector.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the values are approximately equal.</returns>
        public static bool Approx( this Vector3 a, Vector3 b, float tolerance = Epsilon )
        {
            return Approx( a.x, b.x, tolerance ) && Approx( a.y, b.y, tolerance ) && Approx( a.z, b.z, tolerance );
        }

        /// <summary>
        /// Extension method for check if two Vector4's are approximately equal.
        /// </summary>
        /// <param name="a">The extended Vector.</param>
        /// <param name="b">Compare to this Vector.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the values are approximately equal.</returns>
        public static bool Approx( this Vector4 a, Vector4 b, float tolerance = Epsilon )
        {
            return Approx( a.x, b.x, tolerance ) && Approx( a.y, b.y, tolerance ) && Approx( a.z, b.z, tolerance ) && Approx( a.w, b.w, tolerance );
        }



        /// <summary>
        /// Extension method for check if two Quaternions are approximately equal.
        /// </summary>
        /// <param name="a">The extended Quaternion.</param>
        /// <param name="b">Compare to this Quaterion.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the values are approximately equal.</returns>
        public static bool Approx( this Quaternion a, Quaternion b, float tolerance = Epsilon )
        {
            return Quaternion.Angle( a, b ).Approx( 0.0f, tolerance );
        }



#if UNITY_EDITOR || UNITY_STANDALONE

        /// <summary>
        /// Takes a screenshot and stores it in a new folder called Screenshots.
        /// The folder will be located at the Application.dataPath parent.
        /// </summary>
        public static void TakeScreenshot( )
        {
            var root = new DirectoryInfo( Application.dataPath );
            root = root.Parent;

            var dir = root?.FullName + "/Screenshots";

            if ( !Directory.Exists( dir ) )
                Directory.CreateDirectory( dir );

            TakeScreenshot( new DirectoryInfo( dir ) );
        }

        /// <summary>
        /// Takes a screenshot and stores in the specified directory.
        /// </summary>
        /// <param name="dir">The screenshot's directory.</param>
        public static void TakeScreenshot( DirectoryInfo dir )
        {
            TakeScreenshot( dir, "Screenshot_", true, false );
        }

        /// <summary>
        /// Takes a screenshot.
        /// </summary>
        /// <param name="dir">The directory in which to store the screenshot.</param>
        /// <param name="namePrefix">The name of the screenshot.</param>
        /// <param name="appendDate">Should the date-time be appended to the name of the screenshot?</param>
        /// <param name="overWrite">Do we overwrite existing screenshots or add a unique integer to avoid overwriting?</param>
        public static void TakeScreenshot( DirectoryInfo dir, string namePrefix, bool appendDate, bool overWrite )
        {
            if ( dir == null )
                return;

            var datetime = DateTime.Now.ToString( "yyyy-MM-dd_HH-mm" );
            var prefix = dir.FullName + "/" + namePrefix + ( appendDate ? datetime : "" );
            var suffix = 0;

            if ( !overWrite )
                while ( File.Exists( prefix + suffix.ToString( " ##00;;" ) + ".png" ) )
                    suffix++;

            TakeScreenshot( prefix + suffix.ToString( " ##00;;" ) + ".png" );
        }

#endif

        /// <summary>
        /// Takes a screenshot and writes it exactly to the path specified.
        /// </summary>
        /// <param name="fullPath">The full path of the screenshot file.</param>
        public static void TakeScreenshot( string fullPath )
        {
            ScreenCapture.CaptureScreenshot( fullPath );
        }



        /// <summary>
        /// Event system for the application.
        /// </summary>
        public static EventSystem EventSys => _EventSys ?? ( _EventSys = Object.FindObjectOfType< EventSystem >( ) );
        private static EventSystem _EventSys;



        /// <summary>
        /// Checks if an input Canvas GUI object has focus.
        /// This is useful to ignore input from keys if the user is typing into a text box for example.
        /// </summary>
        /// <param name="ignoreSliders">Should sliders be ignored as an input type?</param>
        /// <returns>True if the user has focus on a text box.</returns>
        public static bool IsInputGuiFocused( bool ignoreSliders = false )
        {
            if ( EventSys && EventSys.currentSelectedGameObject )
            {
                var infi = EventSys.currentSelectedGameObject.GetComponent< InputField >( );
                if ( infi && infi.isFocused )
                    return true;

                if ( !ignoreSliders && EventSys.currentSelectedGameObject.GetComponent< Slider >( ) )
                    return true;
            }

            return false;
        }



        /// <summary>
        /// Checks if a button has been pressed, but if a relevant GUI input object has focus, always returns false.
        /// </summary>
        /// <param name="axis">The axis to check.</param>
        /// <param name="ignoreSliders">Do we ignore sliders as an input type?</param>
        /// <returns>Has the axis been pushed down?</returns>
        public static bool GetButtonDownCheckGui( string axis, bool ignoreSliders = false )
        {
            if ( string.IsNullOrEmpty( axis ) )
                return false;

            return !IsInputGuiFocused( ignoreSliders ) && Input.GetButtonDown( axis );

        }

        /// <summary>
        /// Checks if a button has been released, but if a relevant GUI input object has focus, always returns false.
        /// </summary>
        /// <param name="axis">The axis to check.</param>
        /// <param name="ignoreSliders">Do we ignore sliders as an input type?</param>
        /// <returns>Has the axis been pushed down?</returns>
        public static bool GetButtonUpCheckGui( string axis, bool ignoreSliders = false )
        {
            if ( string.IsNullOrEmpty( axis ) )
                return false;

            return !IsInputGuiFocused( ignoreSliders ) && Input.GetButtonUp( axis );

        }

        /// <summary>
        /// Checks if a button is currently down, but if a relevant GUI input object has focus, always returns false.
        /// </summary>
        /// <param name="axis">The axis to check.</param>
        /// <param name="ignoreSliders">Do we ignore sliders as an input type?</param>
        /// <returns>Has the axis been pushed down?</returns>
        public static bool GetButtonCheckGui( string axis, bool ignoreSliders = false )
        {
            if ( string.IsNullOrEmpty( axis ) )
                return false;

            return !IsInputGuiFocused( ignoreSliders ) && Input.GetButton( axis );

        }

        /// <summary>
        /// Reads an axis state, but if a relevant GUI input object has focus, always returns 0.0f.
        /// </summary>
        /// <param name="axis">The axis to check.</param>
        /// <param name="ignoreSliders">Do we ignore sliders as an input type?</param>
        /// <returns>Has the axis been pushed down?</returns>
        public static float GetAxisCheckGui( string axis, bool ignoreSliders = false )
        {
            if ( string.IsNullOrEmpty( axis ) )
                return 0.0f;

            return IsInputGuiFocused( ignoreSliders ) ? 0.0f : Input.GetAxis( axis );

        }



        /// <summary>
        /// Converts a hue to a colour.
        /// </summary>
        /// <param name="hue">The hue value between [0, 1]</param>
        /// <returns>A fully saturated colour at the specified hue.</returns>
        public static Color ColourFromHue( float hue )
        {
            var vec = new Vector3
            {
                x = Mathf.Cos( ( hue - 0.0f / 3.0f ) * Mathf.PI * 2.0f ),
                y = Mathf.Cos( ( hue - 1.0f / 3.0f ) * Mathf.PI * 2.0f ),
                z = Mathf.Cos( ( hue - 2.0f / 3.0f ) * Mathf.PI * 2.0f )
            };


            var max = Mathf.Max( vec.x, vec.y, vec.z );
            var min = Mathf.Min( vec.x, vec.y, vec.z );

            vec.x = ( vec.x - min ) / ( max - min );
            vec.y = ( vec.y - min ) / ( max - min );
            vec.z = ( vec.z - min ) / ( max - min );

            return new Color( vec.x, vec.y, vec.z );
        }



        /// <summary>
        /// Converts a world position into Canvas coordinates.
        /// </summary>
        /// <param name="canvas">The extended canvas.</param>
        /// <param name="worldPosition">The world position to convert.</param>
        /// <param name="camera">Optional camera to use (Camera.main will be used if none is specified).</param>
        /// <returns>2D canvas coordinates.</returns>
        public static Vector3 WorldToCanvas( this Canvas canvas, Vector3 worldPosition, Camera camera = null )
        {
            if ( camera == null )
                camera = Camera.main;


            var canvasRect = canvas.GetComponent< RectTransform >( );
            var viewportPosition = camera.WorldToViewportPoint( worldPosition );

            var vec2 = Vector2.Scale( viewportPosition, canvasRect.sizeDelta ) - canvasRect.sizeDelta * 0.5f;

            return new Vector3( vec2.x, vec2.y, viewportPosition.z );
        }



        /// <summary>
        /// Scales a float value (that should normally by a value between [0..1]) according to the following common formula:
        /// y = A + B * (x ^ C)
        /// Where A = scaleFactors.x, B = scaleFactors.y and C = scaleFactors.z.
        /// </summary>
        /// <param name="val">The raw value.</param>
        /// <param name="scaleFactors">The scaling factors.</param>
        /// <returns>A scaled factor.</returns>
        public static float ScaleFloat( float val, Vector3 scaleFactors )
        {
            return scaleFactors.x + Mathf.Pow( val, scaleFactors.z ) * scaleFactors.y;
        }

        /// <summary>
        /// Scales a float value (that should normally by a value between [0..1]) according to the following common formula:
        /// y = A + B * (x ^ C)
        /// Where A = scaleFactors.x, B = scaleFactors.y and C = scaleFactors.z.
        /// </summary>
        /// <param name="val">The raw value.</param>
        /// <param name="scaleFactors">The scaling factors.</param>
        /// <returns>A scaled factor.</returns>
        public static float Scale( this float val, Vector3 scaleFactors )
        {
            return scaleFactors.x + Mathf.Pow( val, scaleFactors.z ) * scaleFactors.y;
        }



        /// <summary>
        /// Returns the normalized version of the quaterion.
        /// </summary>
        /// <param name="q">The quaternion.</param>
        /// <returns>A normalized quaternion.</returns>
        public static Quaternion Normalize( this Quaternion q )
        {
            var magnitude = Mathf.Sqrt( q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w );
            return new Quaternion( q.x / magnitude, q.y / magnitude, q.z / magnitude, q.w / magnitude );
        }

        /// <summary>
        /// Creates a look-rotation in a flat direction (no y component to the forward vector).
        /// The up-vector is global up.
        /// </summary>
        /// <param name="fwd">The forward vector that will become flattened.</param>
        /// <returns></returns>
        public static Quaternion LookFlat( Vector3 fwd )
        {
            Vector3 flatFwd;

            fwd.Normalize( );

            if ( Mathf.Abs( Vector3.Dot( fwd, Vector3.up ) ) > 0.999f )
            {
                flatFwd = Vector3.forward;
            }
            else
            {
                flatFwd = fwd;
                flatFwd.y = 0.0f;
            }

            return Quaternion.LookRotation( flatFwd );
        }



        /// <summary>
        /// Attempts to open windows explorer to the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void OpenExplorer( string path )
        {

#if UNITY_EDITOR && !UNITY_WEBPLAYER
            var rPath = path.Replace( '/', '\\' );
            Process.Start( "explorer.exe", rPath );
#endif

        }

        /// <summary>
        /// Opens windows explorer and selects the specified file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public static void OpenExplorerAndSelect( string filePath )
        {

#if UNITY_EDITOR && !UNITY_WEBPLAYER
            var args = $"/e,/select,\"{filePath.Replace( '/', '\\' )}\"";
            Process.Start( "explorer.exe", args );
#endif

        }

        /// <summary>
        /// Determines if the directory exists.
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        /// <returns>True iff the directory exists.</returns>
        public static bool DirExists( string path )
        {
#if UNITY_EDITOR && !UNITY_WEBPLAYER
            if ( string.IsNullOrEmpty( path ) )
                return false;

            var rPath = path.Replace( '/', '\\' );
            return Directory.Exists( rPath );
#else
        return false;
#endif
        }



        /// <summary>
        /// A function that recursively trolls through a <see cref="Transform"/> tree to generate a containing bounds.
        /// </summary>
        /// <param name="transform">The current <see cref="Transform"/> node.</param>
        /// <param name="bounds">The bounds of the <see cref="GameObject"/> so far.</param>
        /// <returns>The bounds of the object.</returns>
        public static Bounds? CalculateBounds( Transform transform, Bounds? bounds )
        {
            var renderer = transform.GetComponent<Renderer>();
            var collider = transform.GetComponent< Collider >( );
            var thisBounds = renderer == null ? ( collider == null ? ( Bounds? )null : collider.bounds ) : renderer.bounds;

            if (thisBounds.HasValue )
                if ( bounds.HasValue )
                {
                    var temp = new Bounds( bounds.Value.center, bounds.Value.size );
                    temp.Encapsulate( thisBounds.Value );
                    bounds = temp;
                }
                else
                {
                    bounds = thisBounds.Value;
                }

            foreach ( Transform child in transform )
            {
                var childBounds = CalculateBounds( child, bounds );
                if ( bounds.HasValue && childBounds.HasValue )
                {
                    var temp = new Bounds( bounds.Value.center, bounds.Value.size );
                    temp.Encapsulate( childBounds.Value );
                    bounds = temp;
                }
                else if ( childBounds.HasValue )
                {
                    bounds = childBounds.Value;
                }
            }

            return bounds;
        }


        /// <summary>
        /// Finds the lowest level <see cref="Context"/> that contains the specified <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">The transform who's context we are searching for.</param>
        /// <param name="checkSelf">Should we search ourselves for a context, or strictly search from our parent upwards.</param>
        /// <returns>The zenject container responsible for the transform.</returns>
        public static Context GetContext( this Transform transform, bool checkSelf )
        {
            var context = checkSelf ? transform.GetComponentInParent< Context >( ) : transform.parent?.GetComponentInParent< Context >( );
            if ( context )
                return context;

            var scene = Object.FindObjectsOfType<SceneContext>().FirstOrDefault(sc => sc.gameObject.scene == transform.gameObject.scene);
            if ( scene )
                return scene;

            return Object.FindObjectOfType< ProjectContext >( );
        }

    }

}
