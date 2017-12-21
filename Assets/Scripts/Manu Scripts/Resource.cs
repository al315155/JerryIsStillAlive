// @author: M Gavilan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public ResourceType resourceType;
    public int quantity;

	public int tileX, tileY;
	public AdaptedMap map;



//    public Resource(ResourceType _resourceType, int _quantity = 0)
//    {
//        this.resourceType = _resourceType;
//        this.quantity = _quantity;
//    }

	void Update(){

//
	}

	public void Extract(int value){
		quantity -= value;
		if (quantity < 0) {
			Destroy (gameObject);
		}
	}

}

public enum ResourceType
{
    oxygen, enzyme
}
