using UnityEngine;

using Zenject;


namespace Sentient.DI
{

    public static class ZenjectModuleExtensions
    {

        /// <summary>
        /// Registers a module instance.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="module">The module.</param>
        public static void RegisterModule( this DiContainer container, IZenjectModule module )
        {
            container.Bind< IZenjectModule >( ).FromInstance( module );
        }

        /// <summary>
        /// Registers a non <see cref="UnityEngine.Object"/> module.
        /// </summary>
        public static void RegisterModule< T >( this DiContainer container )
            where T : IZenjectModule
        {
            container.Bind< IZenjectModule >( ).To< T >( ).AsSingle( ).NonLazy( );
        }

        /// <summary>
        /// Registers a non-instanced <see cref="Component"/> module.
        /// </summary>
        public static void RegisterModuleBehaviour< T >( this DiContainer container )
            where T : Component, IZenjectModule
        {
            container.Bind< IZenjectModule >( ).To< T >( ).FromNewComponentOnNewGameObject( ).AsSingle( ).NonLazy( );
        }

        /// <summary>
        /// Registers a non-instanced <see cref="AbstractModuleComponent"/> module.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container">The container.</param>
        /// <param name="parent">The parent <see cref="Transform"/> this moudle should be created under.</param>
        public static void RegisterModuleBehaviour< T >( this DiContainer container, Transform parent )
            where T : Component, IZenjectModule
        {
            container.Bind< IZenjectModule >( ).To< T >( ).FromNewComponentOnNewGameObject( new GameObjectCreationParameters { Name = nameof(T), ParentTransform = parent } ).
                AsSingle( ).NonLazy( );
        }

        /// <summary>
        /// Registers all <see cref="IZenjectModule"/> instances on the specified <see cref="GameObject"/> into the <see cref="DiContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="gameObject">The game object.</param>
        public static void RegisterModuleBehaviours( this DiContainer container, GameObject gameObject )
        {
            foreach ( var zenjectModule in gameObject.GetComponents< IZenjectModule >( ) )
                container.Bind< IZenjectModule >( ).FromInstance( zenjectModule );
        }

    }

}
