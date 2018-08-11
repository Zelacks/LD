using Source;
using Source.Grid;

using Zenject;


public class MainGameInstaller : MonoInstaller< MainGameInstaller >
{

    /// <inheritdoc />
    public override void InstallBindings( )
    {
        Container.Bind< GridWorld >( ).ToSelf( ).FromMethod( _ => FindObjectOfType< GridWorld >( ) ).AsSingle( );
        Container.Bind< BuildingFactory >( ).ToSelf( ).FromMethod( _ => FindObjectOfType< BuildingFactory >( ) ).AsSingle( );
        Container.Bind< GameStateManager >( ).ToSelf( ).FromMethod( _ => FindObjectOfType< GameStateManager >( ) ).AsSingle( );
    }

}
