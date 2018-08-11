using System.Collections.Generic;
using Zenject;

namespace Sentient.DI
{
    /// <summary>
    /// Extension class containing any commonly used registering and binding methods for <see cref="DiContainer"/>.
    /// </summary>
    public static class ZenjectRegistrationExtensions
    {
        /// <summary>
        /// Binds an <see cref="IEnumerable{T}"/> to a <see cref="DiContainer.ResolveAll{TContract}"/>
        /// </summary>
        public static void BindMany<T>( this DiContainer container )
        {
            container.Bind<IEnumerable<T>>().FromMethod( c => c.Container.ResolveAll<T>() );
        }

        /// <summary>
        /// Extension method to allow backward compatibility after Zenject Version 6.1.0 (June 17, 2018) which
        /// removed the overload for AsSingle which contained the concreteId.
        /// </summary>
        /// <param name="scoped">The scoped.</param>
        /// <param name="id">The identifier.</param>
        public static ArgConditionCopyNonLazyBinder AsCached( this ScopeConcreteIdArgConditionCopyNonLazyBinder scoped, object id )
        {
            return scoped.AsCached().WithConcreteId( id );
        }
    }
}