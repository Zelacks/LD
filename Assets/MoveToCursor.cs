using System.Collections;
using System.Collections.Generic;

using Sentient;

using Source.Grid;

using UnityEngine;

using Zenject;


public class MoveToCursor : DisposableBehaviour
{

    public Camera cam;
    public Material Ghostmat;
    public Color ErrorColor;
    [ Inject ]
    private GridWorld world;
    [ Inject ]
    private GameStateManager state;

    private GameObject gameobjectInstance;


    private GridPos curPos;
    private GridPos GridPos
    {
        get { return curPos; }
        set
        {
            if ( value != curPos )
            {
                curPos = value;
                UpdateGhostPreview( );
            }
        }
    }


    private void UpdateGhostPreview( )
    {
        if ( gameobjectInstance == null ) return;

        gameobjectInstance.SetActive( world.ObjectInsideWorld( GridPos ) );

        var canbuild = true;
        var curBlueprint = state.CurrentBlueprint;

        //First check if its inside the world, porbably cheaper
        foreach ( var posToCheck in curBlueprint.Positions )
        {
            if ( !world.ObjectInsideWorld( curPos + posToCheck ) )
            {
                canbuild = false;
                break;
            }
        }

        foreach ( var posToCheck in curBlueprint.Positions )
        {
            if ( world.ObjectAtLocation( curPos + posToCheck ) )
            {
                canbuild = false;
                break;
            }
        }

        var renderers = gameobjectInstance.GetComponentsInChildren< MeshRenderer >( );

        foreach ( var meshRenderer in renderers )
        {
            foreach ( var mat in meshRenderer.materials )
            {
                mat.color = canbuild ? Ghostmat.color : ErrorColor;
            }
        }

        gameobjectInstance.transform.position = curPos.pos;
    }

    void InstanceNewModel( BuildingBlueprintSingle newBlueprint )
    {
        if ( gameobjectInstance != null )
        {
            Destroy( gameobjectInstance );
        }

        if ( newBlueprint.BuildingPrefab != null )
        {
            gameobjectInstance = Instantiate( newBlueprint.BuildingPrefab );
            var renderers = gameobjectInstance.GetComponentsInChildren< MeshRenderer >( );
            foreach ( var meshRenderer in renderers )
            {
                var mats = meshRenderer.materials;
                for ( int i = 0; i < mats.Length; i++ )
                {
                    meshRenderer.materials[ i ] = Ghostmat;
                }
            }
        }

        UpdateGhostPreview( );
    }

    // Use this for initialization
    void Start( )
    {
        state.NewBlueprint += InstanceNewModel;
        InstanceNewModel( state.CurrentBlueprint );
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
            GridPos = world.ConvertWorldPosToGrid( hit.point );
        }

        if ( Input.GetMouseButtonDown( 0 ) )
        {
            var DidBuild = world.BuildObjectAtLocation( GridPos, state.CurrentBlueprint );

            if ( DidBuild )
            {
                state.ProgressRound( state.CurrentBlueprint );
            }
        }
    }

}
