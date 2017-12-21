using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
using UnityEngine.UI;

public class InfluenceMap : MonoBehaviour {

	public int sizeX, sizeY;

	public GameObject InfluenceMapTexture;
	public GameObject GridPositionObject;
	public List<GameObject> OriginatorObject;
	public LayerMask influenceMask;

	public AdaptedMap map;

	InfluenceGrid iG;

	void Start(){

		iG = new InfluenceGrid (map);
		//iG.CreateMap (sizeX, sizeY, 1f, GridPositionObject, true);

		// coger el mapa!


		iG.InfluenceMask = influenceMask;
		for (int i = 0; i < OriginatorObject.Count; i++) {
			Debug.Log (OriginatorObject [i].transform.position);

			iG.RegisterOriginator (OriginatorObject [i].GetComponent<Influencer>().originator);
		}

		iG.UpdateMap ();


		iG.InfluenceMapTexture.Apply ();

		InfluenceMapTexture.GetComponent<RawImage> ().texture = iG.InfluenceMapTexture;

	}

	void Update(){
		iG.UpdateMap ();
		iG.InfluenceMapTexture.Apply ();
		InfluenceMapTexture.GetComponent<RawImage> ().texture = iG.InfluenceMapTexture;
	}

	public void ActualizeInfluenceMap(){
		
	}

	public void AddInfluencePoint(){
		
	}
}
