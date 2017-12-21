using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	
	public Player human;
	public Player cpu;

	public State currentState;
	public State remainState;

	public Resources resources;
	
    public string currentStateName;

    public AdaptedMap map;

	//Sensors
	

	
   
   
    public GameObject player;
    //public float acceleration_speed;
 

	//[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform chaseTarget;

	void Awake()
	{
		chaseTarget = null;


	

		
		Debug.Log ("HOLA JODER");



		/*navMeshAgent = GetComponent<NavMeshAgent> ();
		navMeshAgent.enabled = true;
        acceleration_speed = navMeshAgent.speed * 2f;
        basic_speed = navMeshAgent.speed;*/
	}

	void Update()
	{
        //TODO: Cambiar esto con listeners para que sea más eficiente.
        //if (isPlayerOnSight || isPlayerHeard) navMeshAgent.speed = acceleration_speed;
        //else navMeshAgent.speed = basic_speed;
        Debug.Log("Udapte");

		currentState.UpdateState (this);
	}

	void OnDrawGizmos()
	{
		
	}

	public void TrasitionToState (State nextState)
	{
		if (nextState != remainState) {
			currentState = nextState;
            currentStateName = nextState.name;
		}
	}

    public enum pursuitState { PATROL, FOLLOWING, ALERT, SCAPED }
}
