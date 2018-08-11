using System;
using System.Collections.Generic;

using UnityEngine;


namespace Source.Grid
{

    [ Serializable, CreateAssetMenu( fileName = "Building Blueprint", menuName = "Slumlord/Building Blueprint Single" ) ]
    public class BuildingBlueprintSingle : ScriptableObject
    {

        public List< GridPos > Positions;
        public GameObject BuildingPrefab;

    }

}
