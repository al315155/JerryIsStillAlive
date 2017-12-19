using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCamera : MonoBehaviour {

	//Falta ponerle límites al tamaño del mapa (Cuando se decida)

	// Viewport: The bottom-left of the camera is (0,0); the top-right is (1,1). 
	public float velocity;
	public float xLimit, yLimit;

	void Start () {

	}

	void LateUpdate () {

		float actualX = GetComponent<Camera> ().ScreenToViewportPoint (Input.mousePosition).x;
		float actualY = GetComponent<Camera> ().ScreenToViewportPoint (Input.mousePosition).y;

		if (actualX < xLimit && !CheckForEndOfMap ("ParedIzquierda")) {
			transform.position += Vector3.left * Time.deltaTime * velocity;
		} 
		if (actualX > 1 - xLimit && !CheckForEndOfMap("ParedDerecha")) {
			transform.position += Vector3.right * Time.deltaTime * velocity;
		}

		if (actualY < yLimit && !CheckForEndOfMap("ParedInferior")) {
			transform.position += Vector3.back * Time.deltaTime * velocity;
		}
		if (actualY > 1 - yLimit && !CheckForEndOfMap("ParedSuperior")) {
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
}
