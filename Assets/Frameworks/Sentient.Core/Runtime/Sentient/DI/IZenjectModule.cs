using Zenject;


namespace Sentient.DI
{

    /// <summary>
    /// A module that can install requirements across a number of injection containers.
    /// </summary>
    public interface IZenjectModule
    {
        /// <summary>
        /// If true, this <see cref="IZenjectModule"/> will be installed.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Installs this <see cref="IZenjectModule" /> into the specified <see cref="Context" />.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> to install this module into.</param>
        void Install( Context context );

    }

}
