// @author: M Gavilan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public ResourceType resourceType;
    public int quantity;

	public int tileX, tileY;
	public AdaptedMap map;

	bool extracting = false;

	int extractors = 0;

//    public Resource(ResourceType _resourceType, int _quantity = 0)
//    {
//        this.resourceType = _resourceType;
//        this.quantity = _quantity;
//    }

	void Update(){
		if (extracting) {
			quantity -= 2 * extractors;

			if (quantity < 0) {
				Destroy (this.gameObject);

			}
		}
	}
//
	void Start(){
		quantity = 5000;
	}

	public void StartExtracting(){
		extracting = true;
		extractors++;
	}

	public void StopExtracting(){
		extracting = false;
		extractors--;
	}
}

public enum ResourceType
{
    oxygen, enzyme
}
