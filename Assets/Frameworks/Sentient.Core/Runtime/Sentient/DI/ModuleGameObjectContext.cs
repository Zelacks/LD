namespace Sentient.DI 
{

    /// <summary>
    /// A derived form of <see cref="DisposableGameObjectContext"/> that automatically installs modules without requiring a <see cref="ModulesInstaller"/> attached.
    /// </summary>
    public class ModuleGameObjectContext : DisposableGameObjectContext
    {

        /// <inheritdoc />
        protected override void RunInternal( )
        {
            AddNormalInstaller( new ModulesInstallerObject(  ) );

            base.RunInternal( );
        }

    }

}