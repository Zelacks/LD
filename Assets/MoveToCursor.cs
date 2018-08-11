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
    [ Inject ]
    private GameStateManager state;

    // Use this for initialization
    void Start( ) { }

    // Update is called once per frame
    void Update( )
    {
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            var clampedx = Mathf.Clamp( Input.mousePosition.x, 0f, ( float )( cam.pixelWidth - 1 ) );
            var clampedy = Mathf.Clamp( Input.mousePosition.y, 0f, ( float )( cam.pixelHeight - 1 ) );

            RaycastHit hit;
            var ray = cam.ScreenPointToRay( new Vector3( clampedx, clampedy, 500f ) );
            var result = Physics.Raycast( ray, out hit );

            if ( result )
            {
                var DidBuild = world.BuildObjectAtLocation( world.ConvertWorldPosToGrid( hit.point ), state.CurrentBlueprint );

                if ( DidBuild )
                {
                    state.ProgressRound( state.CurrentBlueprint );
                }

            }
        }
    }

}
