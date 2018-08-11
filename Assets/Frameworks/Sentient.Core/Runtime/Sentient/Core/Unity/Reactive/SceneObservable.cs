using System;
using UniRx;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Sentient.Unity.Reactive
{
    /// <summary>
    /// Reactive extensions on <see cref="SceneManager"/>.
    /// </summary>
    public static class SceneObservable
    {
        /// <summary>
        /// Returns a new <see cref="IObservable{T}"/> that fires when a <see cref="Scene"/> was loaded by the <see cref="SceneManager"/>.
        /// </summary>
        public static IObservable<Tuple<Scene, LoadSceneMode>> OnSceneLoaded()
        {
            return Observable.Create<Tuple<Scene, LoadSceneMode>>( observer =>
            {
                UnityAction<Scene, LoadSceneMode> sceneLoadedAction = ( scene, mode ) =>
                {
                    var value = Tuple.Create( scene, mode );
                    observer.OnNext( value );
                };

                SceneManager.sceneLoaded += sceneLoadedAction;

                return Disposable.Create( () => SceneManager.sceneLoaded -= sceneLoadedAction );
            } );
        }

        /// <summary>
        /// Returns an <see cref="IObservable{T}"/> that completes when the <see cref="Scene"/> is unloaded.
        /// </summary>
        public static IObservable<Unit> OnUnloadedAsObservable( this Scene scene )
        {
            return OnSceneUnloaded().Where( s => s == scene ).Take( 1 ).AsUnitObservable();
        }

        /// <summary>
        /// Returns a new <see cref="IObservable{T}"/> that fires when a <see cref="Scene"/> was unloaded by the <see cref="SceneManager"/>.
        /// </summary>
        public static IObservable<Scene> OnSceneUnloaded()
        {
            return Observable.Create<Scene>( observer =>
            {
                UnityAction<Scene> sceneLoadedAction = scene => observer.OnNext( scene );

                SceneManager.sceneUnloaded += sceneLoadedAction;

                return Disposable.Create( () => SceneManager.sceneUnloaded -= sceneLoadedAction );
            } );
        }

        /// <summary>
        /// Returns a new <see cref="IObservable{T}"/> that fires when a <see cref="Scene"/> was addtively loaded by the <see cref="SceneManager"/>.
        /// </summary>
        public static IObservable<Scene> OnAdditiveSceneLoaded()
        {
            return Observable.Create<Scene>( observer =>
            {
                UnityAction<Scene, LoadSceneMode> sceneLoadedAction = ( scene, mode ) =>
                {
                    if ( mode == LoadSceneMode.Additive )
                        observer.OnNext( scene );
                };

                SceneManager.sceneLoaded += sceneLoadedAction;

                return Disposable.Create( () => SceneManager.sceneLoaded -= sceneLoadedAction );
            } );
        }
    }
}