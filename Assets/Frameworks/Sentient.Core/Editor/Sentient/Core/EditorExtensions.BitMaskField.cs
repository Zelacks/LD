using Sentient.Unity;

using UnityEngine;

using UnityEditor;


namespace Sentient.CustomInspector
{

    public static partial class EditorExtensions
    {

        /// <summary>
        /// Draws the bit mask field.
        /// </summary>
        /// <param name="aPosition">a position.</param>
        /// <param name="aMask">a mask.</param>
        /// <param name="aType">a type.</param>
        /// <param name="aLabel">a label.</param>
        /// <returns></returns>
        public static int DrawBitMaskField( Rect aPosition, int aMask, System.Type aType, GUIContent aLabel )
        {
            var itemNames = System.Enum.GetNames( aType );
            var itemValues = System.Enum.GetValues( aType ) as int[ ];

            int val = aMask;
            int maskVal = 0;
            for ( int i = 0; i < itemValues.Length; i++ )
            {
                if ( itemValues[ i ] != 0 )
                {
                    if ( ( val & itemValues[ i ] ) == itemValues[ i ] )
                        maskVal |= 1 << i;
                }
                else if ( val == 0 )
                    maskVal |= 1 << i;
            }
            int newMaskVal = EditorGUI.MaskField( aPosition, aLabel, maskVal, itemNames );
            int changes = maskVal ^ newMaskVal;

            for ( int i = 0; i < itemValues.Length; i++ )
            {
                if ( ( changes & ( 1 << i ) ) != 0 ) // has this list item changed?
                {
                    if ( ( newMaskVal & ( 1 << i ) ) != 0 ) // has it been set?
                    {
                        if ( itemValues[ i ] == 0 ) // special case: if "0" is set, just set the val to 0
                        {
                            val = 0;
                            break;
                        }
                        else
                            val |= itemValues[ i ];
                    }
                    else // it has been reset
                    {
                        val &= ~itemValues[ i ];
                    }
                }
            }
            return val;
        }

    }

    /// <summary>
    /// Custom property drawer for fields flagged with <see cref="BitMaskAttribute"/>.
    /// </summary>
    /// <seealso cref="UnityEditor.PropertyDrawer" />
    [ CustomPropertyDrawer( typeof ( BitMaskAttribute ) ) ]
    public class EnumBitMaskPropertyDrawer : PropertyDrawer
    {

        /// <summary>
        /// Called when [GUI].
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="prop">The property.</param>
        /// <param name="label">The label.</param>
        public override void OnGUI( Rect position, SerializedProperty prop, GUIContent label )
        {
            var typeAttr = attribute as BitMaskAttribute;
            // Add the actual int value behind the field name
            label.text = label.text + "(" + prop.intValue + ")";
            prop.intValue = EditorExtensions.DrawBitMaskField( position, prop.intValue, typeAttr.FieldType, label );
        }

    }

}
