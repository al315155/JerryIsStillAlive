using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {

	//Falta ponerle límites al tamaño del mapa (Cuando se decida)

	// Viewport: The bottom-left of the camera is (0,0); the top-right is (1,1). 
	public float velocity;
	public float xLimit, yLimit;

	bool blocked_right;
	bool blocked_left;
	bool blocked_up;
	bool blocked_down;


	void Start () {
		blocked_right = false;
		blocked_left = false;
		blocked_up = false;
		blocked_down = false;
	}

	void LateUpdate () {

		CheckBlocks ();

		float actualX = GetComponent<Camera> ().ScreenToViewportPoint (Input.mousePosition).x;
		float actualY = GetComponent<Camera> ().ScreenToViewportPoint (Input.mousePosition).y;

		if (actualX < xLimit && !blocked_left) {
			transform.position += Vector3.left * Time.deltaTime * velocity;
		} 
		if (actualX > 1 - xLimit && !blocked_right) {
			transform.position += Vector3.right * Time.deltaTime * velocity;
		}

		if (actualY < yLimit && !blocked_down) {
			transform.position += Vector3.back * Time.deltaTime * velocity;
		}
		if (actualY > 1 - yLimit && !blocked_up) {
			transform.position += Vector3.forward * Time.deltaTime * velocity;
		}

	}

	public bool CheckForEndOfMap(string tag){
		RaycastHit ray;
		if (Physics.Raycast (transform.position, transform.forward, out ray)) {
			if (ray.collider.tag.Equals(tag)){
				return true;
			}
		}
		return false;
	}

	public void CheckBlocks(){
		if (CheckForEndOfMap ("ParedIzquierda"))
			blocked_left = true;
		else
			blocked_left = false;

		if (CheckForEndOfMap ("ParedDerecha"))
			blocked_right = true;
		else
			blocked_right = false;

		if (CheckForEndOfMap ("ParedInferior"))
			blocked_down = true;
		else
			blocked_down = false;

		if (CheckForEndOfMap ("ParedSuperior"))
			blocked_up = true;
		else
			blocked_up = false;

	}
}
