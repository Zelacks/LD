using Source.Grid;

using UnityEngine;

using Zenject;


namespace Source
{



    public class BuildingFactory : MonoBehaviour
    {

        public GameObject colliderBlock;
        public BuildingBlueprintSingle blueprint;


        [ Inject ]
        private GridWorld world;
        [ Inject ]
        private DiContainer container;


        public void Buildit( GridPos buildLoc )
        {
            CreateBuildingAtLocationSingle( buildLoc, blueprint );
        }

        public void CreateBuildingAtLocationSingle( GridPos buildLoc, BuildingBlueprintSingle buildingBlueprint )
        {
            var prefab = container.InstantiatePrefab( buildingBlueprint.BuildingPrefab );
            prefab.transform.SetParent( null );
            prefab.transform.position = buildLoc.pos;
            prefab.transform.rotation = Quaternion.identity;

            foreach ( var blueprintLocs in buildingBlueprint.Positions )
            {
                var col = container.InstantiatePrefab( colliderBlock );
                var gridobj = col.GetComponentInChildren< GridObject >( );
                gridobj.gridPos = buildLoc + blueprintLocs;
                col.transform.SetParent( prefab.transform );
                col.transform.position = gridobj.gridPos.pos;
                world.AddObjectToBuilt( gridobj );
            }
        }




    }

}
