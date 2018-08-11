using System.Collections;
using System.Collections.Generic;

using Source.Grid;

using UnityEngine;

using Zenject;


public class GridRenderer : MonoBehaviour
{

    public Material GridMat;

    public Transform xParent;
    public Transform zParent;


    [ Inject ]
    private GridWorld grid;
    private Vector3Int WorldSize => grid.WorldSize;


    private void Start( )
    {
        DrawGrid( );
    }


    LineRenderer CreateGridLineRenderer(Transform parent)
    {
        var go = new GameObject( "GridRenderer" );
        go.transform.SetParent(parent);
        var lr = go.AddComponent< LineRenderer >( );
        lr.startWidth = 0.03f;
        lr.endWidth = 0.03f;
        lr.material = GridMat;
        return lr;
    }

    private void DrawGrid( )
    {
        for ( int i = 0; i <= WorldSize.x; i++ )
        {
            var lr = CreateGridLineRenderer(xParent );
            lr.positionCount = 2;
            lr.SetPosition( 0, new Vector3( i, 0f, 0f ) );
            lr.SetPosition( 1, new Vector3( i, 0f, WorldSize.z ) );
        }

        for ( int i = 0; i <= WorldSize.z; i++ )
        {
            var lr = CreateGridLineRenderer(zParent);
            lr.positionCount = 2;
            lr.SetPosition( 0, new Vector3( 0f, 0f, i ) );
            lr.SetPosition( 1, new Vector3( WorldSize.x, 0f, i ) );
        }

    }

}
