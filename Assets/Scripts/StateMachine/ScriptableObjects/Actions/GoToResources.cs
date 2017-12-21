using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/GotoRes")]
public class GoToResources : Action
{
    Unidad unit;

    // Use this for initialization
    public override void Act(StateController controller)
    {
        Debug.Log("Estoy dentro");
        unit = controller.player.GetComponent<Unidad>();
        unit.pathfinding.SetSelected(controller.player);
        controller.map.GeneratePathTo(unit.mapa.transform.GetChild(6).gameObject.GetComponent<Hex>().tileX, unit.mapa.transform.GetChild(6).gameObject.GetComponent<Hex>().tileY);
        unit.pathfinding.NextTurn();
    }
}

