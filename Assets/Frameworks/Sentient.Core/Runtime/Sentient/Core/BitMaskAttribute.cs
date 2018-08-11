using System;

using UnityEngine;


namespace Sentient.Unity
{

    /// <summary>
    /// Defines an attribute in a Unity inspector that should function as a bit field mask (indepedent bit mask).
    /// When combined with the relevant editor scripts this allows us to draw bit mask editor fields for these enum types.
    /// </summary>
    /// <seealso cref="PropertyAttribute" />
    public class BitMaskAttribute : PropertyAttribute
    {

        /// <summary>
        /// Gets the type of the bit-mask.
        /// </summary>
        public Type FieldType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitMaskAttribute"/> class.
        /// </summary>
        /// <param name="fieldType">The type of the bit-mask.</param>
        public BitMaskAttribute( Type fieldType )
        {
            FieldType = fieldType;
        }

    }

}
