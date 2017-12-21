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
        
		Hex destination = controller.resources.GetNearerResourcePoint (unit);

		// posiciones Al rededor
		List<Hex> totalHexes = destination.neighbors;
		List<Hex> freeHexes = new List<Hex> ();

		bool occupied;
		for (int i = 0; i < totalHexes.Count; i++) {
			occupied = false;
			for (int j = 0; j < controller.human.Squad.Count; j++) {
				if (totalHexes [i].tileX == controller.human.Squad [i].GetComponent<Pathfinding> ().tileX &&
					totalHexes [i].tileY == controller.human.Squad [i].GetComponent<Pathfinding> ().tileY) {
					occupied = true;
					break;
				}
			}
			if (!occupied) {
				freeHexes.Add (totalHexes [i]);
			}
		}

		totalHexes = freeHexes;
		freeHexes = new List<Hex> ();

		for (int i = 0; i < totalHexes.Count; i++) {
			occupied = false;
			for (int j = 0; j < controller.cpu.Squad.Count; j++) {
				if (totalHexes [i].tileX == controller.cpu.Squad [i].GetComponent<Pathfinding> ().tileX &&
					totalHexes [i].tileY == controller.cpu.Squad [i].GetComponent<Pathfinding> ().tileY) {
					occupied = true;
					break;
				}
			}
			if (!occupied) {
				freeHexes.Add (totalHexes [i]);
			}
		}

		if (freeHexes.Count > 0) {
			destination =  freeHexes [0];

		}

		controller.map.GeneratePathTo(destination.tileX, destination.tileY);

		if (unit.pathfinding.currentPath == null) {
			
			unit.pathfinding.NextTurn ();
		}


    }
}

