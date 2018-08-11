using UnityEngine;

using Zenject;


namespace Sentient.DI
{

    /// <inheritdoc cref="IZenjectModule" />
    /// <summary>
    /// Base class for any <see cref="Component" /> <see cref="IZenjectModule" />.
    /// </summary>
    public abstract class AbstractModuleComponent : MonoBehaviour, IZenjectModule
    {
        /// <summary>
        /// If <c>true</c>, this <see cref="AbstractModuleAsset"/> will be installed.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        /// <inheritdoc />
        public abstract void Install(Context context);

    }

}