using System;

using UniRx;

using UnityEngine;


namespace Sentient
{

    /// <summary>
    /// A simple base class that implements <see cref="IDisposable"/> and exposes a <see cref="CompositeDisposable"/> to deriving types.
    /// This behaviour will call <see cref="Dispose"/> when the object is destroyed (and vice-versa).
    /// </summary>
    public class DisposableBehaviour : MonoBehaviour, IDisposable
    {

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        public bool IsDisposed => Disposable?.IsDisposed ?? true;

        /// <summary>
        /// Gets the composite disposable object for this behaviour.  You can dump your disposables in here to have them cleaned up automatically on Dispose( ) or OnDestroy( ).
        /// </summary>
        protected readonly CompositeDisposable Disposable = new CompositeDisposable( );

        /// <summary>
        /// Allows deriving types to override the behaviour when this disposable is disposed.
        /// Should we destroy this component, this gameobject or do nothing?
        /// </summary>
        protected virtual DestroyMode DisposeMode => DestroyMode.DestroyGameObject;

        /// <summary>
        /// Defines possible steps to take when this behaviour is disposed.
        /// </summary>
        public enum DestroyMode
        {

            /// <summary>
            /// Destroy the component when disposed.
            /// </summary>
            DestroyComponent,

            /// <summary>
            /// Destroy the gameobject when disposed.
            /// </summary>
            DestroyGameObject,

            /// <summary>
            /// Do nothing when disposed.
            /// </summary>
            DoNothing

        }

        /// <inheritdoc />
        public void Dispose( )
        {
            if ( Disposable.IsDisposed )
                return;

            Disposable?.Dispose( );

            OnDispose( );

            switch ( DisposeMode )
            {
                case DestroyMode.DestroyComponent:
                    if ( this )
                        Destroy( this );
                    break;

                case DestroyMode.DestroyGameObject:
                    if ( gameObject )
                        Destroy( gameObject );
                    break;
            }
        }

        /// <summary>
        /// Called when this <see cref="Component"/> or <see cref="GameObject"/> is destroyed.
        /// </summary>
        protected virtual void OnDestroy( )
        {
            Dispose( );
        }

        /// <summary>
        /// Called when the object is disposed for the first time.
        /// </summary>
        protected virtual void OnDispose( )
        {

        }

    }

}
