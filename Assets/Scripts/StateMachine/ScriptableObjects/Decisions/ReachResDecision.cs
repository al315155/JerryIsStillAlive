using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "AI/ResDecision")]
public class ReachResDecision : Decision {

	public override bool Decide (StateController controller){

		//Si llega al destino true y si no false;
		return true;
	}
}
