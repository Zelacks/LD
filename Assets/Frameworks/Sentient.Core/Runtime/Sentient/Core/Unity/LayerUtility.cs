using System;
using UnityEngine;

namespace Sentient.Core.Unity
{
    /// <summary>
    /// Utility class for <see cref="LayerMask"/> operations.
    /// </summary>
    public static class LayerUtility
    {
        /// <summary>
        /// Returns the single layer as an integer from the specified <see cref="LayerMask"/>. If there is not exactly
        /// one layer in the <see cref="HasLayer"/> then an <see cref="InvalidOperationException"/> is thrown.
        /// </summary>
        public static int Single( this LayerMask layerMask )
        {
            int layerCount = SelectedLayersCount( layerMask );
            if ( layerCount != 1 )
                throw new InvalidOperationException( "Invalid LayerMask: LayerMask contains " + layerCount + " layers but must have exactly 1." );

            return FirstOrDefault( layerMask );
        }

        /// <summary>
        /// Returns whether or not the specified layer is flagged within the <see cref="LayerMask"/>.
        /// </summary>
        public static bool HasLayer( this LayerMask layerMask, int layer )
        {
            if ( layerMask == ( layerMask | ( 1 << layer ) ) )
                return true;

            return false;
        }

        /// <summary>
        /// Returns the number of layers flagged in the specified <see cref="LayerMask"/>.
        /// </summary>
        public static int SelectedLayersCount( this LayerMask layerMask )
        {
            var selected = 0;

            for( int index = 0; index < 32; index++ )
                if ( HasLayer( layerMask, index ) )
                    selected++;

            return selected;
        }

        /// <summary>
        /// Returns the first <see cref="int"/> layer flagged within the specified <see cref="LayerMask"/>.
        /// If the <see cref="LayerMask"/> has no layers, returns 0.
        /// </summary>
        public static int FirstOrDefault( this LayerMask layerMask )
        {
            for( int index = 0; index < 32; index++ )
            {
                if ( HasLayer( layerMask, index ) )
                    return index;
            }

            return 0;
        }
    }
}