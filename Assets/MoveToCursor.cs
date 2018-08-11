using System.Collections;
using System.Collections.Generic;

using Source.Grid;

using UnityEngine;

using Zenject;


public class MoveToCursor : MonoBehaviour
{

    public Camera cam;
    [ Inject ]
    private GridWorld world;

    private GridObject gridObj;


    // Use this for initialization
    void Start( )
    {
        gridObj = GetComponentInChildren< GridObject >( );
    }

    // Update is called once per frame
    void Update( )
    {
        var clampedx = Mathf.Clamp( Input.mousePosition.x, 0f, ( float )( cam.pixelWidth - 1 ) );
        var clampedy = Mathf.Clamp( Input.mousePosition.y, 0f, ( float )( cam.pixelHeight - 1 ) );

        RaycastHit hit;
        var ray = cam.ScreenPointToRay( new Vector3( clampedx, clampedy, 500f ) );
        var result = Physics.Raycast( ray, out hit );

        if ( result )
        {
            gridObj.gridPos = world.ConvertWorldPosToGrid( hit.point );
        }

    }

}
