using System.Collections.Generic;

using UnityEngine;

using Zenject;


namespace Sentient.DI
{

    /// <summary>
    /// A type that installs a number of <see cref="AbstractModuleAsset"/> instances into all containers across the application.
    /// This <see cref="Component"/> should always be located on a <see cref="ProjectContext"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="ModulesInstaller"/> will call <see cref="IZenjectModule.Install"/> on every container.
    /// </remarks>
    [ RequireComponent( typeof( ProjectContext ) ) ]
    public class ModuleConfigurer : MonoInstaller< ModuleConfigurer >
    {

        [ Tooltip( "The set of asset modules that should be installed when this Component is active and enabled." ) ]
        public List< AbstractModuleAsset > Modules;

        [ Tooltip( "The set of behaviour modules that should be installed when this Component is active and enabled." ) ]
        public List< AbstractModuleComponent > BehaviourModules;


        /// <inheritdoc />
        public override void InstallBindings( )
        {
            // Honour our enabled state when installing, this allows easy configuration in the inspector for different profiles..
            if ( !enabled )
                return;

            // Bind all component modules..
            foreach ( var module in Modules )
                if ( module != null && module.Enabled )
                    Container.RegisterModule( module );
                else
                    Debug.LogWarning( $"Found null {nameof(AbstractModuleAsset)} in {name}, skipping installation of this module." );

            // Bind all behaviour modules..
            foreach ( var module in BehaviourModules )
                if ( module != null && module.Enabled )
                    Container.RegisterModule( module );
                else
                    Debug.LogWarning( $"Found null {nameof(AbstractModuleComponent)} in {name}, skipping installation of this module." );
        }

    }

}
