using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName = "AI/State")]
public class State : ScriptableObject {

	public Action[] actions;
	public Transition[] transitions;
	public Color sceneGizmoColor = Color.grey;

	public void UpdateState (StateController controller)
	{
		DoAction (controller);
		CheckTrasitions (controller);
	}

	private void DoAction(StateController controller)
	{
		for (int i = 0; i < actions.Length; i++) {

			actions [i].Act (controller);
		}
	}

	private void CheckTrasitions (StateController controller)
	{
		for (int i = 0; i < transitions.Length; i++) {
			bool decisionSuucceeded = transitions [i].decision.Decide (controller);

			if (decisionSuucceeded) {
				controller.TrasitionToState (transitions [i].trueState);
			} else {
				controller.TrasitionToState (transitions [i].falseState);
			}
		} 
	}
}
