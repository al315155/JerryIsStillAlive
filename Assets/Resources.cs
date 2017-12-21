using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour {

	public List<GameObject> resources;
	private List<Vector2> resourcesPos;
	public AdaptedMap map;

	public void GetResources(){
		Resource[] list = map.gameObject.transform.GetComponentsInChildren<Resource> ();
		for (int i = 0; i < list.Length; i++) {
			resources.Add (list [i].gameObject);
		}
	}

	void Start () {
		resources = new List<GameObject> ();
		GetResources ();
	}
	
	void Update () {
		
	}

	public Hex GetNearerResourcePoint(Unidad unit){

		SetPositions ();

		Vector2 myPos = new Vector2 (unit.pathfinding.tileX, unit.pathfinding.tileY);


		int cont = 0;
		float minDistance = Vector2.Distance (resourcesPos [0], myPos);;
		for (int i = 0; i < resourcesPos.Count; i++){
			if (Vector2.Distance(resourcesPos[i], myPos) < minDistance){
				minDistance = Vector2.Distance (resourcesPos [i], myPos);
				cont = i;
			}
		}

		return resources [cont].GetComponentInParent<Hex> ();
	}

	public  void SetPositions(){
		resourcesPos = new List<Vector2> ();
		for (int i = 0; i < resources.Count; i++) {
			resourcesPos.Add (new Vector2(resources [i].GetComponentInParent<Hex> ().tileX, resources [i].GetComponentInParent<Hex> ().tileY));
		}
	}

}
