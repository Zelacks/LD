using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Sentient
{



    /// <summary>
    /// A simple library of helper extension functions.
    /// Author: Chris Hellsten
    /// </summary>
    public static class Util
    {

        /// <summary>
        /// Joins a set of paths together to form a composite system path.
        /// </summary>
        /// <param name="strings">The relative directories.</param>
        /// <returns>A directory path.</returns>
        public static string ConcatPath(params string[] strings)
        {
            string result = "";

            if (strings.Length == 0 || strings[0] == null)
                return result;

            bool haveBasis = false;

            foreach (string str in strings)
            {
                if (String.IsNullOrEmpty(str))
                    continue;

                string trimmed = haveBasis ? str.Trim('\\', '/') : str.TrimEnd('\\', '/');
                if (String.IsNullOrEmpty(trimmed))
                    continue;

                result += (haveBasis ? "\\" : "") + trimmed;

                haveBasis = true;
            }

            return result.Replace('/', '\\');
        }

        /// <summary>
        /// Concatenates the string parameters onto the current string.
        /// Each string object is treated as a separate folder in a string path and slashes will be appended appropriately automatically.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="strings">The strings to concatenate.</param>
        /// <returns>A combined path.</returns>
        public static string ConcatPath(this string src, params string[] strings)
        {
            src = src.TrimEnd('\\', '/');
            bool haveBasis = !String.IsNullOrEmpty(src);

            foreach (string str in strings)
            {
                if (String.IsNullOrEmpty(str))
                    continue;

                string trimmed = haveBasis ? str.Trim('\\', '/') : str.TrimEnd('\\', '/');
                if (String.IsNullOrEmpty(trimmed))
                    continue;

                src += (haveBasis ? "\\" : "") + trimmed;

                haveBasis = true;
            }

            return src.Replace('/', '\\');
        }




        /// <summary>
        /// Joins a set of URL segments together to form a composite URL.
        /// Does not automatically prepend: http://
        /// </summary>
        /// <param name="strings">The relative directories.</param>
        /// <returns>A web URL.</returns>
        public static string ConcatUrl(params string[] strings)
        {
            string result = "";

            if (strings.Length == 0 || strings[0] == null)
                return result;

            bool haveBasis = false;

            foreach (string str in strings)
            {
                if (String.IsNullOrEmpty(str))
                    continue;

                string trimmed = haveBasis ? str.Trim('\\', '/') : str.TrimEnd('\\', '/');
                if (String.IsNullOrEmpty(trimmed))
                    continue;

                result += (haveBasis ? "/" : "") + trimmed;

                haveBasis = true;
            }

            return result.Replace('\\', '/');
        }

        /// <summary>
        /// Concatenates the string parameters onto the current string.
        /// Each string object is treated as a separate folder in a string URL and slashes will be appended appropriately automatically.
        /// Does not automatically prepend: http://
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="strings">The strings to concatenate.</param>
        /// <returns>A combined URL.</returns>
        public static string ConcatUrl(this string src, params string[] strings)
        {
            src = src.TrimEnd('\\', '/');
            bool haveBasis = !String.IsNullOrEmpty(src);

            foreach (string str in strings)
            {
                if (String.IsNullOrEmpty(str))
                    continue;

                string trimmed = haveBasis ? str.Trim('\\', '/') : str.TrimEnd('\\', '/');
                if (String.IsNullOrEmpty(trimmed))
                    continue;

                src += (haveBasis ? "/" : "") + trimmed;

                haveBasis = true;
            }

            return src.Replace('\\', '/');
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        public static void UnorderedRemove< T >( this List< T > list, int index )
        {
            list[ index ] = list[ list.Count - 1 ];
            list.RemoveAt( list.Count - 1 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="val"></param>
        public static void UnorderedRemove< T >( this List< T > list, T val )
        {
            for ( int i = 0; i < list.Count; i++ )
            {
                if ( ReferenceEquals( list[ i ], val ) )
                {
                    UnorderedRemove( list, i );
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public static void Swap< T >( this List< T > list, int index1, int index2 )
        {
            T temp = list[ index1 ];
            list[ index1 ] = list[ index2 ];
            list[ index2 ] = temp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index"></param>
        public static void SwapToFront< T >( this List< T > list, int index )
        {
            Swap( list, index, 0 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="p"></param>
        public static void AddAll< T >( this List< T > list, params IEnumerable[ ] p )
        {
            foreach ( var c in p )
                list.AddRange( c.OfType< T >( ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static List< T > Merge< T >( params IEnumerable[ ] p )
        {
            var result = new List< T >( );
            foreach ( var c in p )
                result.AddRange( c.OfType< T >( ) );
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all"></param>
        /// <param name="f"></param>
        public static void ForEach< T >( this IEnumerable< T > all, Action< T > f )
        {
            if ( all == null )
                return;

            foreach ( var i in all )
                f( i );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all"></param>
        /// <param name="f"></param>
        public static void ForEachReverse< T >( this IEnumerable< T > all, Action< T > f )
        {
            if ( all == null )
                return;

            foreach ( var i in all.Reverse( ) )
                f( i );
        }




        /// <summary>
        /// Returns a factor that is equal to one if value is zero.
        /// The return value approaches zero as value approaches infinity.
        /// At a value of x, the result is y.
        /// </summary>
        /// <param name="y"></param>
        /// <param name="value"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float DiminishingReturns( float x, float y, float value )
        {
            float xy = x * y;
            return xy / ( xy + value * ( 1.0f - y ) );
        }

        /// <summary>
        /// Returns a factor that is equal to one if value is zero.
        /// The return value approaches zero as value approaches infinity.
        /// At a value of 100, the result is 0.5.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float DiminishingReturns( float value )
        {
            return 100.0f / ( 100.0f + value );
        }



        /// <summary>
        /// A value that represents "Close Enough" when comparing floating point values.
        /// </summary>
        private const float EpsilonFloat = 0.00001f;

        /// <summary>
        /// A value that represents "Close Enough" when comparing floating point values.
        /// </summary>
        private const decimal EpsilonDecimal = 0.0000000001m;

        /// <summary>
        /// Extension method for check if two floats are approximately equal.
        /// </summary>
        /// <param name="a">The extend float.</param>
        /// <param name="b">Compare to this float.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the values are approximately equal.</returns>
        public static bool Approx( this float a, float b, float tolerance = EpsilonFloat )
        {
            return Math.Abs( a - b ) < tolerance;
        }

        /// <summary>
        /// Extension method to check if two decimals are approximately equal.
        /// </summary>
        /// <param name="a">The extend float.</param>
        /// <param name="b">Compare to this float.</param>
        /// <param name="tolerance">Optional user-specified tolerance. (Default is 10^-15)</param>
        /// <returns>True if the values are approximately equal.</returns>
        public static bool Approx( this decimal a, decimal b, decimal tolerance = EpsilonDecimal )
        {
            return Math.Abs( a - b ) < tolerance;
        }

        /// <summary>
        /// Extension method for check if two floats are approximately equal.
        /// </summary>
        /// <param name="a">The extended float.</param>
        /// <param name="min">The approximate minimum value.</param>
        /// <param name="max">The approximate maximum value.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the value is approximately between min and max.</returns>
        public static bool ApproxInRange( this float a, float min, float max, float tolerance = EpsilonFloat )
        {
            return a + tolerance >= min && a - tolerance <= max;
        }

        /// <summary>
        /// Extension method for check if two floats are approximately equal.
        /// </summary>
        /// <param name="a">The extended float.</param>
        /// <param name="min">The approximate minimum value.</param>
        /// <param name="max">The approximate maximum value.</param>
        /// <param name="tolerance">Optional user-specified tolerance.</param>
        /// <returns>True if the value is approximately between min and max.</returns>
        public static bool ApproxInRange( this decimal a, decimal min, decimal max, decimal tolerance = EpsilonDecimal )
        {
            return a + tolerance >= min && a - tolerance <= max;
        }



        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection.
        /// </summary>
        /// <remarks>
        /// If more than one element has the maximal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current maximal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource Best< TSource, TKey >( this IEnumerable< TSource > source,
            Func< TSource, TKey > selector )
            where TSource : class
        {
            return source.Best( selector, Comparer< TKey >.Default );
        }

        /// <summary>
        /// Returns the maximal element of the given sequence, based on
        /// the given projection and the specified comparer for projected values. 
        /// </summary>
        /// <remarks>
        /// If more than one element has the maximal projected value, the first
        /// one encountered will be returned. This overload uses the default comparer
        /// for the projected type. This operator uses immediate execution, but
        /// only buffers a single result (the current maximal element).
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="selector">Selector to use to pick the results to compare</param>
        /// <param name="comparer">Comparer to use to compare projected values</param>
        /// <returns>The maximal element, according to the projection.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
        /// or <paramref name="comparer"/> is null</exception>
        /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>
        public static TSource Best< TSource, TKey >( this IEnumerable< TSource > source,
            Func< TSource, TKey > selector, IComparer< TKey > comparer )
            where TSource : class
        {
            if ( source == null )
                throw new ArgumentNullException( nameof(source) );
            if ( selector == null )
                throw new ArgumentNullException( nameof(selector) );
            if ( comparer == null )
                throw new ArgumentNullException( nameof(comparer) );
            using ( var sourceIterator = source.GetEnumerator( ) )
            {
                if ( !sourceIterator.MoveNext( ) )
                {
                    return null;
                    //throw new InvalidOperationException( "Sequence contains no elements" );
                }
                var max = sourceIterator.Current;
                var maxKey = selector( max );
                while ( sourceIterator.MoveNext( ) )
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector( candidate );
                    if ( comparer.Compare( candidateProjected, maxKey ) > 0 )
                    {
                        max = candidate;
                        maxKey = candidateProjected;
                    }
                }
                return max;
            }
        }

        /// <summary>
        /// Linq extension to find the index of the first item matching a condition.
        /// </summary>
        /// <typeparam name="TSource">Type of the IEnumerable.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">The selector function.</param>
        /// <returns>The index or -1 if none were matching.</returns>
        public static int FirstIndex< TSource >( this IEnumerable< TSource > source, Func< TSource, bool > selector )
        {
            if ( source == null )
                return -1;
            if ( selector == null )
                throw new ArgumentNullException( nameof(selector) );

            int i = 0;

            using ( var sourceIterator = source.GetEnumerator( ) )
            {
                while ( sourceIterator.MoveNext( ) )
                {
                    var candidate = sourceIterator.Current;

                    if ( selector( candidate ) )
                        return i;

                    i++;
                }
            }

            return -1;
        }

        /// <summary>
        /// Linq extension to find all indices of items matching a condition.
        /// </summary>
        /// <typeparam name="TSource">Type of the IEnumerable.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">The selector function.</param>
        /// <returns>An enumerable of indices.</returns>
        public static IEnumerable< int > AllIndices< TSource >( this IEnumerable< TSource > source, Func< TSource, bool > selector )
        {
            if ( source == null )
                return new int[ 0 ];
            if ( selector == null )
                throw new ArgumentNullException( nameof(selector) );

            var list = new List< int >( );

            int i = 0;

            using ( var sourceIterator = source.GetEnumerator( ) )
            {
                while ( sourceIterator.MoveNext( ) )
                {
                    var candidate = sourceIterator.Current;

                    if ( selector( candidate ) )
                        list.Add( i );

                    i++;
                }
            }

            return list;
        }

    }

}
