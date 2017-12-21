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

			iG.RegisterOriginator (OriginatorObject [i].GetComponent<Influencer>().originator);
		}

		StartCoroutine("FirstUpdate");

	}

	void Update(){
	}

	public void ActualizeInfluenceMap(){
		for (int i = 0; i < OriginatorObject.Count; i++) {
			iG.RegisterOriginator (OriginatorObject [i].GetComponent<Influencer> ().originator);

		}
		iG.UpdateMap ();
		iG.InfluenceMapTexture.Apply ();
		InfluenceMapTexture.GetComponent<RawImage> ().texture = iG.InfluenceMapTexture;
	}

	public void AddInfluencePoint(GameObject obj){
		if (!OriginatorObject.Contains (obj)) {
			OriginatorObject.Add (obj);

			ActualizeInfluenceMap ();
		}
	}

	public void RemoveInfluencePoint(GameObject obj){
		if (OriginatorObject.Contains (obj)) {
			OriginatorObject.Remove (obj);

			ActualizeInfluenceMap ();
		}
	}

	IEnumerator FirstUpdate(){
		yield return new WaitForSeconds(1);
		ActualizeInfluenceMap();
	}
}
