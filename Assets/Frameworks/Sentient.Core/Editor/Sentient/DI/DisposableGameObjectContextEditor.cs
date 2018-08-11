#if !ODIN_INSPECTOR
using UnityEditor;

using UnityEngine;

using Zenject;


namespace Sentient.DI.Editor
{

    /// <summary>
    /// Default inspector for all types deriving from <see cref="DisposableGameObjectContext"/>.
    /// </summary>
    /// <seealso cref="Zenject.GameObjectContextEditor" />
    [ CustomEditor( typeof( DisposableGameObjectContext ), true ) ]
    public class DisposableGameObjectContextEditor : GameObjectContextEditor
    {

        /// <inheritdoc />
        protected override void OnGui( )
        {
            GUILayout.Space( 15.0f );
            GUILayout.Label( $"{target.GetType( ).Name} Properties", EditorStyles.boldLabel );

            DrawDefaultInspector( );

            GUILayout.Space( 15.0f );
            GUILayout.Label( "Dependency Installation", EditorStyles.boldLabel );

            base.OnGui( );
        }

    }

}
#endif
