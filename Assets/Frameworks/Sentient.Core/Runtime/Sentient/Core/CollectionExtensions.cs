using System;
using System.Collections.Generic;


namespace Sentient
{

    /// <summary>
    /// Defines extension methods for the <see cref="ICollection{T}"/> type.
    /// </summary>
    public static class CollectionExtensions
    {

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"/> and removes it again when the return token is disposed.
        /// </summary>
        /// <typeparam name="T">The type of the item and collection.</typeparam>
        /// <param name="collection">The collection to which the item will be added.</param>
        /// <param name="item">The item to add to the collection.</param>
        /// <returns>A token that should be disposed to remove the item from the collection.</returns>
        public static IDisposable AddAsDisposable< T >( this ICollection< T > collection, T item )
        {
            collection.Add( item );
            return UniRx.Disposable.Create( ( ) => collection.Remove( item ) );
        }

    }

}