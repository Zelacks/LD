using Zenject;


namespace Sentient 
{

    /// <summary>
    /// An interface for a type that has access to a Zenject <see cref="DiContainer"/>.
    /// </summary>
    public interface IContainer
    {

        /// <summary>
        /// Gets the DI sub-container of this object.
        /// </summary>
        DiContainer Container { get; }

    }


    /// <summary>
    /// Defines extension methods for the <see cref="IContainer"/> type.
    /// </summary>
    public static class ContainerExtensions
    {

        /// <summary>
        /// Gets the specified component from a <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type of the component to resolve.</typeparam>
        /// <param name="container">The player instance.</param>
        /// <returns>A component from the <see cref="DiContainer"/>.</returns>
        public static T Get< T >( this IContainer container )
            where T : class
        {
            return container.Container.TryResolve< T >( );
        }

    }

}