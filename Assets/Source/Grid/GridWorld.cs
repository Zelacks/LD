using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using UnityEngine;

using Zenject;


namespace Source.Grid
{

    public class GridWorld : MonoBehaviour
    {

        private List< GridObject > BuiltObjects = new List< GridObject >( );

        [ Inject ]
        public BuildingFactory factory { get; set; }



        public Vector3Int WorldSize;

        public GridPos ConvertWorldPosToGrid( Vector3 worldCoordinate )
        {
            const float epsilon = 0.01f;
            var x = Mathf.FloorToInt( worldCoordinate.x + epsilon );
            var y = Mathf.FloorToInt( worldCoordinate.y + epsilon );
            var z = Mathf.FloorToInt( worldCoordinate.z + epsilon );
            return new GridPos( x, y, z );
        }

        public bool ObjectAtLocation( GridPos pos )
        {
            return BuiltObjects.Any( t => t.gridPos == pos );
        }


        public bool BuildObjectAtLocation( GridPos buildPos )
        {
            factory.Buildit( buildPos );
            return true;
        }


    }

}
