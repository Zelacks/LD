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
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


namespace Sentient.CustomInspector
{

    public static class ListRenderer
    {

        private static readonly Queue< object > itemsToRemove = new Queue< object >( );

        /// <summary>
        /// Draws the list. Use this draw list method when the reference to a list item needs
        /// to change.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="renderItem">The render item.</param>
        public static void DrawList< TObject >( List< TObject > list, Func< TObject, TObject > renderItem )
        {
            for ( int index = 0; index < list.Count; index++ )
            {
                EditorExtensions.Horizontal( ( ) =>
                {
                    var item = list[ index ];
                    list[ index ] = renderItem( item );

                    if ( GUILayout.Button( "Delete", GUILayout.Width( 50f ) ) )
                        itemsToRemove.Enqueue( item );
                } );
            }

            if ( GUILayout.Button( "+" ) )
                list.Add( default( TObject ) );

            // Remove item marked for deletion
            while ( itemsToRemove.Any( ) )
            {
                list.Remove( ( TObject )itemsToRemove.Dequeue( ) );
            }

        }

        /// <summary>
        /// Draws the list. Use this draw list method when the reference to this list item never changes
        /// but the properties of that list item does.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="addItem">The add item.</param>
        /// <param name="renderItem">The render item.</param>
        public static void DrawList< TObject >( List< TObject > list, Func< TObject > addItem, Action< TObject > renderItem )
        {
            foreach ( var item in list )
            {
                EditorExtensions.Horizontal( ( ) =>
                {
                    renderItem( item );

                    if ( GUILayout.Button( "Delete", GUILayout.Width( 50f ) ) )
                        itemsToRemove.Enqueue( item );
                } );
            }

            var newItem = addItem( );

            if( newItem != null )
                list.Add( newItem );

            // Remove item marked for deletion
            while ( itemsToRemove.Any( ) )
            {
                list.Remove( ( TObject )itemsToRemove.Dequeue( ) );
            }

        }

    }

}

#endif