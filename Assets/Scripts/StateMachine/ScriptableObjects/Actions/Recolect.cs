using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Recolect")]
public class Recolect : Action {

	// Use this for initialization
	public override void Act(StateController controller)
	{
		if (controller.myResource != null) {
			if (controller.time > controller.resourceTimer) {
				Debug.Log ("entro aqui en recolectar");
				controller.myResource.Extract (5);
				controller.GetComponent<Unidad> ().Owner.AddResources (controller.myResource.resourceType, 5);
				controller.time = 0f;
                controller.GetComponent<Unidad>().finished = true;
                Debug.Log("UFFF Ya me estoy tocando los huevos.");
                GameManager.Instance.CheckPlayerTurn();
			}
		}
	}
}
