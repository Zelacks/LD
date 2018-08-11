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
    public class GUILayoutExtensions
    {

        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Button( string text, Action onClick, params GUILayoutOption[ ] layoutOptions )
        {
            try
            {
                if ( GUILayout.Button( text, layoutOptions ) )
                {
                    onClick?.Invoke( );
                }
            }
            catch ( Exception exception )
            {
                Debug.LogError( exception );
            }
        }

        [ Obsolete( "Use EditorExtensions instead.", false ) ]
        public static void Button( string text, GUIStyle guiStyle, Action onClick, params GUILayoutOption[ ] layoutOptions )
        {
            try
            {
                if ( GUILayout.Button( text, guiStyle, layoutOptions ) )
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

#endif
