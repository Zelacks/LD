using System;
using System.Collections;
using System.Collections.Generic;

using ISSoW_Base_Game.Scripts.Helpers;

using JetBrains.Annotations;

using Source.Grid;
using Source.Helpers;

using UnityEngine;


public class GameStateManager : MonoBehaviour
{

    public List< BuildingBlueprintSingle > Candidates = new List< BuildingBlueprintSingle >( );

    public ObservableValue< int > CurrentMoney = new ObservableValue< int >( 0 );
    public ObservableValue< int > Income = new ObservableValue< int >( 0 );
    public ObservableValue< int > TurnsTaken = new ObservableValue< int >( 0 );

    public event Action< BuildingBlueprintSingle > NewBlueprint;
    public BuildingBlueprintSingle CurrentBlueprint;

    /// <summary>
    /// Called immediately after this <see cref="UnityEngine.Component"/> instance is created.
    /// This is essentially a constructor for a <see cref="UnityEngine.Component"/>, you cannot define your own constructor.
    /// </summary>
    [ UsedImplicitly ]
    private void Awake( )
    {
        Setup( );
    }

    void Setup( )
    {
        ChooseBlueprint( );
    }

    void ChooseBlueprint( )
    {
        List< ProportionValue< BuildingBlueprintSingle > > values = new List< ProportionValue< BuildingBlueprintSingle > >( );
        for ( int i = 0; i < Candidates.Count; i++ )
        {
            values.Add( ProportionValue.Create( Candidates[ i ].ChanceToChoose, Candidates[ i ] ) );
        }

        CurrentBlueprint = values.ChooseByRandom( );
        NewBlueprint?.Invoke( CurrentBlueprint );
    }


    public void ProgressRound( BuildingBlueprintSingle builtBuilding )
    {
        Income.Value = Income.Value + builtBuilding.IncomeGenerated;
        CurrentMoney.Value = CurrentMoney.Value + Income.Value;
        TurnsTaken.Value = TurnsTaken.Value + 1;

        ChooseBlueprint( );
    }


}
