using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {

	// Use this for initialization
	public abstract void Act (StateController controller);
}
