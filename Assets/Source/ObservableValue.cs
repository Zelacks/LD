using System;


namespace ISSoW_Base_Game.Scripts.Helpers
{

    [ Serializable ]
    public class ObservableValue< T >
    {

        public ObservableValue( )
        {
            val = default( T );
        }

        public ObservableValue( T value )
        {
            val = value;
        }

        public event Action< T > ValueChanged;
        private T val;
        public T Value
        {
            get { return val; }
            set
            {
                val = value;
                ValueChanged?.Invoke( val );
            }
        }

        public static implicit operator T( ObservableValue< T > val )
        {
            return val.Value;
        }

        public static explicit operator ObservableValue< T >( T val )
        {
            return new ObservableValue< T >( val );
        }

    }

}
