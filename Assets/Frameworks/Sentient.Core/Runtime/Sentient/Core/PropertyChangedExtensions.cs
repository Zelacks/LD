using System;
using System.ComponentModel;

using UniRx;


namespace Sentient 
{

    /// <summary>
    /// Provides extension methods for the <see cref="INotifyPropertyChanged"/> interface.
    /// </summary>
    public static class PropertyChangedExtensions
    {

        /// <summary>
        /// Wraps the <see cref="INotifyPropertyChanged.PropertyChanged"/> event as an <see cref="IObservable{ EventPattern }"/> for use with Reactive Extensions.
        /// </summary>
        /// <param name="source">The <see cref="INotifyPropertyChanged"/> authoring the event.</param>
        /// <returns>The <see cref="INotifyPropertyChanged.PropertyChanged"/> event as an <see cref="IObservable{EventPattern}"/> for use with Reactive Extensions.</returns>
        public static IObservable< EventPattern< PropertyChangedEventArgs > > PropertyChangedAsObservable( this INotifyPropertyChanged source )
        {
            return Observable.FromEventPattern< PropertyChangedEventHandler, PropertyChangedEventArgs >( h => h.Invoke, h => source.PropertyChanged += h, h => source.PropertyChanged -= h );
        }

    }

}