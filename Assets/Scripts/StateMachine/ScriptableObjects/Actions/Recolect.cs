using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Recolect")]
public class Recolect : Action {

	// Use this for initialization
	public override void Act(StateController controller)
	{
		if (controller.myResource != null) {
			controller.time += Time.deltaTime;
			if (controller.time > controller.resourceTimer) {
				controller.myResource.Extract (5);
				controller.GetComponent<Unidad> ().Owner.AddResources (controller.myResource.resourceType, 5);
			}
		}
	}
}
