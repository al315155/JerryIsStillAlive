using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIA : MonoBehaviour
{

    public UnitState currentState;
    private Unidad unit;
    // Use this for initialization
    void Start()
    {
        currentState = UnitState.DEFAULT;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case UnitState.DEFAULT:
                break;
            case UnitState.DEFENSIVA:
                break;
            case UnitState.OFENSIVA:
                break;
            case UnitState.SUPERVIVENCIA:
                break;
        }
    }
}

public enum UnitState
{
    DEFAULT,
    SUPERVIVENCIA,  // Cuando es atacado.
    DEFENSIVA,      // Cuando un enemigo entra en el área de influencia
    OFENSIVA        // Cuando se lleva la delantera en influencia y queremos atacar    
}
