using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/EnemyStats")]
public class EnemyStats : ScriptableObject {

	public float moveSpeed = 1;
	public float lookRange = 40f;
	public float lookSphereCastRadius = 1f;

}
