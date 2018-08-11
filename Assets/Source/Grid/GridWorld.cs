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

        public void AddObjectToBuilt( GridObject obj )
        {
            BuiltObjects.Add( obj );
        }


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

        public bool ObjectInsideWorld( GridPos gpos )
        {
            var pos = gpos.pos;
            return ( pos.x >= 0 && pos.x <= WorldSize.x-1 &&
                pos.y >= 0 && pos.y <= WorldSize.y-1 &&
                pos.z >= 0 && pos.z <= WorldSize.z-1 );

        }


        public bool BuildObjectAtLocation( GridPos buildPos, BuildingBlueprintSingle building )
        {
            foreach ( var pos in building.Positions )
            {
                var worldGrid = buildPos + pos;
                if ( !ObjectInsideWorld( worldGrid ) || ObjectAtLocation( worldGrid ) )
                {
                    return false;
                }
            }

            factory.CreateBuildingAtLocationSingle( buildPos, building );
            return true;
        }


    }

}
