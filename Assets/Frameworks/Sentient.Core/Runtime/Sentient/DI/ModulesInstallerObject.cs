using System.Collections.Generic;

using UnityEngine;

using Zenject;


namespace Sentient.DI 
{

    /// <summary>
    /// Installs all modules configured via a <see cref="ModuleConfigurer"/> into a <see cref="DiContainer"/>.
    /// </summary>
    public class ModulesInstallerObject : InstallerBase
    {

        /// <summary>
        /// Gets the <see cref="Zenject.Context"/> that this installer will install into.
        /// </summary>
        [ Inject ]
        public Context Context { get; }


        /// <inheritdoc />
        public override void InstallBindings( )
        {
            InstallModules( Container, Context );
        }

        /// <summary>
        /// Gets all modules.
        /// </summary>
        private static IEnumerable< IZenjectModule > GetModules( DiContainer container )
        {
            return container.ResolveAll< IZenjectModule >( );
        }

        /// <summary>
        /// Installs all modules.
        /// </summary>
        private static void InstallModules( DiContainer container, Context context )
        {
            var modules = GetModules( container );

            foreach ( var zenjectModule in modules )
            {
                if ( container.IsValidating )
                {
                    // During validation some instances may return null from Resolve.
                    if ( zenjectModule == null )
                    {
                        Debug.Log( $"{nameof(ModulesInstaller)} skipped installation of null module instance during validation." +
                            $" If validation is failing please use the attribute {nameof(ZenjectAllowDuringValidationAttribute)}" +
                            " on your module class." );

                        break;
                    }

                    Debug.Log( $"Installing module: {zenjectModule?.GetType( ).FullName ?? "NULL"} into " +
                        $"context: {context?.gameObject?.name ?? "Unknown"}" );
                }

                zenjectModule.Install( context );
            }
        }

    }

}