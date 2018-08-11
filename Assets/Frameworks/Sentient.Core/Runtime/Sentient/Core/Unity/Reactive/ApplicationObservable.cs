using System;
using UniRx;
using UnityEngine;

namespace Sentient.Unity.Reactive
{
    /// <summary>
    /// Observable extension on <see cref="UnityEngine.Application"/>.
    /// </summary>
    public static class ApplicationObservable
    {
        /// <summary>
        /// An observable that emits and completes when <see cref="Application.quitting"/> is called.
        /// </summary>
        /// <seealso cref="MainThreadDispatcher.OnApplicationQuitAsObservable"/>
        public static IObservable<Unit> OnQuittingAsObservable()
        {
            return Observable.Create<Unit>( observer =>
            {
                Action onQuitting = observer.OnCompleted;

                Application.quitting += onQuitting;

                return Disposable.CreateWithState( onQuitting, handler => { Application.quitting -= handler; } );
            } );
        }
    }
}