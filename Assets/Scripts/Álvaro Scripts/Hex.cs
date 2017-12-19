using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour {

    public float x ;
    public float y;

    public int tileX;
    public int tileY;

    [Header("2 para not walkable")]
    public int tileType;

    private void Start()
    {
        x =  this.transform.position.x;
        y = this.transform.position.z;

    }


	//-----//-----//------//------//------//
	// INFLUENCE MAP //
	public Vector3 worldPosition;
	public List<KeyValuePair<int, float>> influences;
	public List<Hex> neighbors;
	public Color32 myColor;
	public GameObject worldObject;

	public bool HasInfluenceOfType(int type){
		for (int i = 0; i < influences.Count; i++) {

			if (influences [i].Key == type) {

				return true;
			}
		}
		return false;
	}


	public KeyValuePair<int, float> GetInfluenceOfType(int type){

		for (int i = 0; i < influences.Count; i++) {

			if (influences [i].Key == type) {
				return new KeyValuePair<int, float> (i, influences [i].Value);
			}

		}
		return new KeyValuePair<int, float> (0, -1);
	}
	//-----//-----//------//------//------//

}
