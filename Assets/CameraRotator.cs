using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CameraRotator : MonoBehaviour
{

    private float current = 0f;
    private float sens = 5f;

    // Update is called once per frame
    void Update( )
    {
        if ( Input.GetMouseButton( 1 ) )
        {
            current += sens * Input.GetAxis( "Mouse X" );
            transform.rotation = Quaternion.AngleAxis( current, Vector3.up );
        }


    }

}
