using System;
using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;

using TMPro;

using UnityEngine;

using Zenject;


public class DisplayStateValue : MonoBehaviour
{

    public ToShow DisplayData;
    [ Inject ]
    private GameStateManager state;
    private TextMeshProUGUI textMesh;
    public enum ToShow
    {

        Income,
        Money,
        Turns,

    }

    /// <summary>
    /// Called just before the <see cref="UnityEngine.Component"/> begins its update cycle.
    /// This function is called after all scene objects receive their Awake( ) calls, and can be used to setup cross-dependencies with other <see cref="UnityEngine.Component"/>s.
    /// </summary>
    [ UsedImplicitly ]
    private void Start( )
    {
        textMesh = GetComponentInChildren< TextMeshProUGUI >( );
        switch ( DisplayData )
        {
            case ToShow.Income:
                state.Income.ValueChanged += UpdateValue;
                UpdateValue( state.Income );
                break;
            case ToShow.Money:
                state.CurrentMoney.ValueChanged += UpdateValue;
                UpdateValue( state.CurrentMoney );
                break;
            case ToShow.Turns:
                state.TurnsTaken.ValueChanged += UpdateValue;
                UpdateValue( state.TurnsTaken );
                break;
            default:
                throw new ArgumentOutOfRangeException( );
        }
    }

    private void UpdateValue( int obj )
    {
        textMesh.text = obj.ToString( );
    }

}
