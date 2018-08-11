using System;


namespace Sentient 
{

    /// <summary>
    /// Simple implementation of <see cref="ISwitch"/>.
    /// </summary>
    public class Switch : ISwitch
    {

        /// <inheritdoc />
        public bool IsOn
        {
            get { return isOn; }
            set
            {
                if ( isOn == value )
                    return;
                isOn = value;
                Changed?.Invoke( new SwitchEvent( this, value ) );
            }
        }
        private bool isOn;

        /// <inheritdoc />
        public event Action< SwitchEvent > Changed;

    }

}