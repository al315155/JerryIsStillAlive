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
	public Resource myResource;
	
    public string currentStateName;

    public AdaptedMap map;

	//Sensors
	
	public Hex destination;

	public float resourceTimer = 5f;
	public float time;
   
   
    public GameObject player;
    //public float acceleration_speed;

	//[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform chaseTarget;

	void Awake()
	{
		myResource = null;
		chaseTarget = null;

		time = 0f;
	

		
		Debug.Log ("HOLA JODER");
		/*navMeshAgent = GetComponent<NavMeshAgent> ();
		navMeshAgent.enabled = true;
        acceleration_speed = navMeshAgent.speed * 2f;
        basic_speed = navMeshAgent.speed;*/
	}

	void Update()
	{
		time += Time.deltaTime;

		
        //TODO: Cambiar esto con listeners para que sea más eficiente.
        //if (isPlayerOnSight || isPlayerHeard) navMeshAgent.speed = acceleration_speed;
        //else navMeshAgent.speed = basic_speed;

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
