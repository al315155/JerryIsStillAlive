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

		iG.UpdateMap ();


//		for (int i = 0; i < map.map.GetLength (0); i++) {
//			for (int j = 0; j < map.map.GetLength (1); j++) {
//
//				Debug.Log (map.map [i, j].worldObject);
//				map.map[i,j].worldObject.GetComponent<Renderer>().sharedMaterial.color = map.map[i,j].myColor;
//
//			}
//		}
//
//		for (int i = 0; i < map.map.GetLength (0); i++) {
//			for (int j = 0; j < map.map.GetLength (1); j++) {
//				map.map [i, j].myColor.a = 255;
//				iG.InfluenceMapTexture.SetPixel (i, j, map.map [i, j].myColor);
//
//				map.map[i,j].worldObject.GetComponent<Renderer>().sharedMaterial.color = map.map[i,j].myColor;
//
//			}
//		}
//
		iG.InfluenceMapTexture.Apply ();
	//	for (int i = 0; i < map.map.GetLength (0); i++) {
		//	for (int j = 0; j < map.map.GetLength (1); j++) {
			//	map.map[i,j].worldObject.GetComponent<Renderer> ().enabled = true;

//				map.map[i,j].worldObject.GetComponent<Renderer>().sharedMaterial.color = map.map[i,j].myColor;

			//}
	//	}
//

		InfluenceMapTexture.GetComponent<RawImage> ().texture = iG.InfluenceMapTexture;

	}
}
