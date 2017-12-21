using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/ResDecision")]
public class ReachResDecision : Decision {

	public override bool Decide (StateController controller){

//		Debug.Log (controller.player.GetComponent<Pathfinding> ().tileX == controller.destination.GetComponentInParent<Hex> ().tileX &&
//		controller.player.GetComponent<Pathfinding> ().tileY == controller.destination.GetComponentInParent<Hex> ().tileY);
//
//		if (controller.myResource == null ){
//			return false;
//		}
//
//		if (controller.player.GetComponent<Pathfinding> ().tileX == controller.destination.GetComponentInParent<Hex> ().tileX &&
//		    controller.player.GetComponent<Pathfinding> ().tileY == controller.destination.GetComponentInParent<Hex> ().tileY) {
//			return true;
//		}

		return false;
	}
}
