using System;

using UniRx;


namespace Sentient 
{

    /// <summary>
    /// An interface for a type that can be either on / off.
    /// </summary>
    public interface ISwitch
    {

        /// <summary>
        /// Gets the current state of the <see cref="ISwitch"/>.
        /// </summary>
        bool IsOn { get; }

        /// <summary>
        /// An event that is fired every time the state of the <see cref="ISwitch"/> changes.
        /// </summary>
        event Action< SwitchEvent > Changed;

    }

    /// <summary>
    /// Defines extension methods for the <see cref="ISwitch"/> type.
    /// </summary>
    public static class ISwitchExtensions
    {

        /// <summary>
        /// Wraps the <see cref="ISwitch.Changed"/> event as an <see cref="IObservable{SwitchEvent}"/> for use with Reactive Extensions.
        /// </summary>
        /// <param name="source">The <see cref="ISwitch"/> authoring the event.</param>
        /// <returns>The <see cref="ISwitch.Changed"/> event as an <see cref="IObservable{SwitchEvent}"/> for use with Reactive Extensions.</returns>
        public static IObservable< SwitchEvent > ChangedAsObservable( this ISwitch source )
        {
            return Observable.FromEvent< SwitchEvent >( h => source.Changed += h, h => source.Changed -= h );
        }

        /// <summary>
        /// Wraps the <see cref="ISwitch.Changed"/> (to on) event as an <see cref="IObservable{SwitchEvent}"/> for use with Reactive Extensions.
        /// </summary>
        /// <param name="source">The <see cref="ISwitch"/> authoring the event.</param>
        /// <returns>The <see cref="ISwitch.Changed"/> (to on) event as an <see cref="IObservable{SwitchEvent}"/> for use with Reactive Extensions.</returns>
        public static IObservable< SwitchEvent > OnAsObservable( this ISwitch source )
        {
            return source.ChangedAsObservable( ).Where( ev => ev.State );
        }

        /// <summary>
        /// Wraps the <see cref="ISwitch.Changed"/> (to off) event as an <see cref="IObservable{SwitchEvent}"/> for use with Reactive Extensions.
        /// </summary>
        /// <param name="source">The <see cref="ISwitch"/> authoring the event.</param>
        /// <returns>The <see cref="ISwitch.Changed"/> (to off) event as an <see cref="IObservable{SwitchEvent}"/> for use with Reactive Extensions.</returns>
        public static IObservable< SwitchEvent > OffAsObservable( this ISwitch source )
        {
            return source.ChangedAsObservable( ).Where( ev => !ev.State );
        }

        /// <summary>
        /// Performs an action now and then whenever the state of the switch changes, until the return token is disposed.
        /// </summary>
        /// <param name="source">The switch status to monitor.</param>
        /// <param name="action">The action to perform now and whenever there's a change.</param>
        /// <returns>A token to dispose to stop listening to change events.</returns>
        public static IDisposable NowAndOnChange( this ISwitch source, Action action )
        {
            action( );
            return source.ChangedAsObservable( ).Subscribe( _ => action( ) );
        }

        /// <summary>
        /// Performs an action now and then whenever the state of the switch changes, until the return token is disposed.
        /// </summary>
        /// <param name="source">The switch status to monitor.</param>
        /// <param name="action">The action to perform now and whenever there's a change.</param>
        /// <returns>A token to dispose to stop listening to change events.</returns>
        public static IDisposable NowAndOnChange( this ISwitch source, Action< ISwitch > action )
        {
            action( source );
            return source.ChangedAsObservable( ).Subscribe( ev => action( ev.Switch ) );
        }

    }

}