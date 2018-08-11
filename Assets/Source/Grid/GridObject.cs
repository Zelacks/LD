using System;

using JetBrains.Annotations;

using Sentient;

using UnityEngine;


namespace Source.Grid
{
    [Serializable]
    public struct GridPos
    {

        public Vector3Int pos;

        public GridPos( int inx, int iny, int inz )
        {
            pos = new Vector3Int( inx, iny, inz );
        }

        public bool IsAtPosition( int inx, int iny, int inz )
        {
            return inx == pos.x && iny == pos.y && inz == pos.z;
        }

        public bool IsAtPosition( GridPos other )
        {
            return IsAtPosition( other.pos.x, other.pos.x, other.pos.z );
        }

        /// <inheritdoc />
        public override bool Equals( object other )
        {
            return other is GridPos && IsAtPosition( ( GridPos )other );
        }

        public override int GetHashCode( )
        {
            return pos.GetHashCode( );
        }

        public static bool operator ==( GridPos o1, GridPos o2 )
        {
            return o1.Equals( o2 );
        }

        public static GridPos operator +( GridPos o1, GridPos o2 )
        {
            var pos1 = o1.pos;
            var pos2 = o2.pos;

            return new GridPos( pos1.x + pos2.x, pos1.y + pos2.y, pos1.z + pos2.z );
        }


        public static bool operator !=( GridPos o1, GridPos o2 )
        {
            return !( o1 == o2 );
        }

    }


    public class GridObject : DisposableBehaviour
    {

        public event Action< GridPos > MovedToNewPosition;

        private GridPos pos;
        public GridPos gridPos
        {
            get { return pos; }
            set
            {
                if ( value != pos )
                {
                    pos = value;
                    MovedToNewPosition?.Invoke( pos );
                }
            }
        }

        /// <summary>
        /// Called immediately after this <see cref="UnityEngine.Component"/> instance is created.
        /// This is essentially a constructor for a <see cref="UnityEngine.Component"/>, you cannot define your own constructor.
        /// </summary>
        [ UsedImplicitly ]
        private void Awake( )
        {
            //MovedToNewPosition += SetPositionToGrid;
        }

        public void SetPositionToGrid( GridPos gridPos1 )
        {
            transform.position = gridPos.pos;
        }

    }

}
