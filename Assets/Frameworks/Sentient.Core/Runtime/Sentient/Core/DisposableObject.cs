using System;

using UniRx;


namespace Sentient
{

    /// <summary>
    /// A simple base class that implements <see cref="IDisposable"/> and exposes a <see cref="CompositeDisposable"/> to deriving types.
    /// </summary>
    public class DisposableObject : IDisposable
    {

        /// <summary>
        /// Gets the composite disposable object for this behaviour.  You can dump your disposables in here to have them cleaned up automatically on Dispose( ) or OnDestroy( ).
        /// </summary>
        protected readonly CompositeDisposable Disposable = new CompositeDisposable( );

        /// <inheritdoc />
        public void Dispose( )
        {
            if ( Disposable.IsDisposed )
                return;

            Disposable?.Dispose( );
            OnDispose( );
        }

        /// <summary>
        /// Allows base classes to handle custom actions when disposed.
        /// </summary>
        protected virtual void OnDispose( ) { }

    }

}