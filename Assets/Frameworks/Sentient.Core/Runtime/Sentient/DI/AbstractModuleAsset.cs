using UnityEngine;

using Zenject;


namespace Sentient.DI
{
    
    /// <inheritdoc cref="IZenjectModule" />
    /// <summary>
    /// Base class for any <see cref="ScriptableObject" /> <see cref="IZenjectModule" />.
    /// </summary>
    public abstract class AbstractModuleAsset : ScriptableObject, IZenjectModule
    {
        [ SerializeField, HideInInspector ]
        private bool enabled = true;

        /// <summary>
        /// If <c>true</c>, this <see cref="AbstractModuleAsset"/> will be installed.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <summary>
        /// Installs this <see cref="IZenjectModule" /> into the specified <see cref="Context" />.
        /// </summary>
        /// <param name="context">The <see cref="Context" /> to install this module into.</param>
        public abstract void Install( Context context );

    }

}
