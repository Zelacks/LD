namespace Sentient 
{

    /// <summary>
    /// Holds information about a switch changing state.
    /// </summary>
    public class SwitchEvent
    {

        /// <summary>
        /// Creates a new <see cref="SwitchEvent"/> instance.
        /// </summary>
        /// <param name="theSwitch">The <see cref="ISwitch"/> that changed state.</param>
        /// <param name="state">The <see cref="bool"/> state that the <see cref="ISwitch"/> changed to at the moment of the event.</param>
        public SwitchEvent( ISwitch theSwitch, bool state )
        {
            Switch = theSwitch;
            State = state;
        }

        /// <summary>
        /// Gets the <see cref="ISwitch"/> that changed state.
        /// </summary>
        public ISwitch Switch { get; }

        /// <summary>
        /// Gets the <see cref="bool"/> state that the <see cref="ISwitch"/> changed to at the moment of the event. 
        /// </summary>
        public bool State { get; }

    }

}