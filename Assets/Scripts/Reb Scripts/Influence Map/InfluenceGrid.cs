using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class InfluenceGrid
	{
		public LayerMask InfluenceMask;
		public Material TestMat;
		public Material TestMat2;
		public Material StartingMat;
		public Texture2D InfluenceMapTexture;
		//GridPosition[,] m_Grid;
		bool RenderGroundGrid;


		AdaptedMap map;


		List<Originator> m_OriginatorList = new List<Originator> ();
		public void RegisterOriginator(Originator newOriginator){
			m_OriginatorList.Add (newOriginator);
		}
		float updateTimer;


		public void SetMapInfluences(){
			for (int i = 0; i < map.width; i++) {
				for (int j = 0; j < map.height; j++) {

					//EL MAPA DE HEX
					map.map[i,j].influences = new List<KeyValuePair<int, float>> ();

					List<Node> nodeNeighbors = map.graph [i, j].neighbours;
					List<Hex> hexNeighbors = new List<Hex> ();
					foreach (Node node in nodeNeighbors) {
						hexNeighbors.Add (map.map [node.x, node.y]);
					}

					map.map [i, j].neighbors = hexNeighbors;
					map.map [i, j].worldPosition = map.map [i, j].worldObject.transform.position;
					map.map [i, j].worldObject.GetComponentInParent<InfluencePosition> ().Hexes = new int[2]{ i, j };
				}
			}
		}

		public InfluenceGrid(AdaptedMap map){
			this.map = map;
			InfluenceMapTexture = new Texture2D (map.width, map.height);
			SetMapInfluences ();
		}



		public void UpdateMap(){


			InfluenceMapTexture = new Texture2D (map.width, map.height);

			SetMapInfluences ();
//
//			updateTimer -= Time.deltaTime;
//			if (updateTimer < 0) {
//				return;
//			}
//			updateTimer = .5f;
			
			for (int i = 0; i < map.map.GetLength (0); i++) {
				for (int j = 0; j < map.map.GetLength (1); j++) {
					map.map [i, j].influences.Clear ();
					map.map [i, j].myColor = Color.black;
			
//					if (map.map [i, j].worldObject.GetComponent<Renderer> ().enabled) {
//						map.map [i, j].worldObject.GetComponent<Renderer> ().material.color = Color.black;
//						map.map [i, j].worldObject.GetComponent<Renderer> ().enabled = false;
//					}
				}
			}

			for (int i = 0; i < m_OriginatorList.Count; i++) {


				Debug.Log (m_OriginatorList [i].worldPosition);

				Collider[] influencePositions = Physics.OverlapSphere (m_OriginatorList [i].worldPosition, m_OriginatorList [i].influenceRange, InfluenceMask);

				// para que coga solo suelo (sus colliders)
				Collider[] nuevo;
				int cont = 0;
				for (int r = 0; r < influencePositions.Length; r++) {
					if (influencePositions [r].tag != "Suelo") {
						cont++;
					}
				}
				nuevo = new Collider[influencePositions.Length - cont];
				cont = 0;
				for (int r = 0; r < influencePositions.Length; r++) {
					if (influencePositions [r].tag == "Suelo"){
						nuevo [cont] = influencePositions [r];
						cont++;
					}
				}
				influencePositions = nuevo;


				for (int j = 0; j < influencePositions.Length; j++) {

					// LA POSICION X y Z de WORLDPOSITION DEBE SER MAYOR O IGUAL QUE INFLUENCE RANGE. NECESITAMOS CONTROLARLO AL COGER LA LISTA DE INFLUENCE POINTS SI AÑADIMOS UN NUEVO WAYPOINT
					Hex currentHex = map.map [influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [0], influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [1]];

					if (!map.map [influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [0], 
						influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [1]
								].HasInfluenceOfType (m_OriginatorList [i].type)) 
					{
						map.map [influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [0], 
							influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [1]]
								.influences.Add (
									new KeyValuePair<int, float> (m_OriginatorList [i].type, 
									((1/(Vector3.Distance (m_OriginatorList [i].worldPosition, influencePositions [j].transform.position)))) / m_OriginatorList [i].influenceRange));
					} else {
						KeyValuePair<int, float> iPos = map.map [influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [0],
							influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [1]].GetInfluenceOfType (m_OriginatorList [i].type);
					
						if (iPos.Value != -1) {
							map.map [influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [0], 
								influencePositions [j].GetComponentInParent<InfluencePosition> ().Hexes [1]].influences [iPos.Key] = 
									
									new KeyValuePair<int, float> (m_OriginatorList [i].type, iPos.Value +
									((1/(Vector3.Distance (m_OriginatorList [i].worldPosition, influencePositions [j].transform.position))) /m_OriginatorList [i].influenceRange));
						}
					}
					// no se fusionan bien los colores
					Color32 newColor = Color.white;
				//	currentHex.worldObject.GetComponent<Renderer> ().enabled = true;

					for (int k = 0; k < currentHex.influences.Count; k++) {
//						if (RenderGroundGrid) {

						if (currentHex.influences [k].Key == 0) {
							//newColor += (((Color)m_OriginatorList [i].color) * (currentHex.influences [k].Value) * (m_OriginatorList [i].influence));
							newColor.r -= (byte) (255 * (currentHex.influences [k].Value) * (m_OriginatorList [i].influence));
						}

						if (currentHex.influences [k].Key == 1) {
							//newColor +=  (((Color)m_OriginatorList [i].color) * (currentHex.influences [k].Value) * (m_OriginatorList [i].influence));
							newColor.b -= (byte) (255 * (currentHex.influences [k].Value) * (m_OriginatorList [i].influence));

						}


//
//						currentHex.worldObject.GetComponent<Renderer> ().enabled = true;
//						currentHex.worldObject.GetComponent<Renderer> ().material.color += ((Color)m_OriginatorList [i].color) * (currentHex.influences [k].Value) * (m_OriginatorList [i].influence);
//													
//					currentHex.myColor += ((Color)m_OriginatorList [i].color) * (currentHex.influences [k].Value) * (m_OriginatorList [i].influence);
//					
					}
					newColor.r = (byte) (Mathf.Abs ((float)newColor.r - 255));

					newColor.b = (byte) (Mathf.Abs ((float)newColor.b - 255));


					if ((float)newColor.r < (float)newColor.b) {
						newColor.r = (byte)0f;

					} else if ((float)newColor.r > (float)newColor.b){
						newColor.b = (byte)0f;

					}


					newColor.g = (byte)0f;

					currentHex.myColor = newColor;
					//currentHex.worldObject.GetComponent<Renderer> ().sharedMaterial.color = newColor;
				}
			}
				
			//no se dibuja la textura
			for (int i = 0; i < map.map.GetLength (0); i++) {
				for (int j = 0; j < map.map.GetLength (1); j++) {
					map.map [i, j].myColor.a = 255;
					InfluenceMapTexture.SetPixel (i, j, map.map [i, j].myColor);

					//map.map[i,j].worldObject.GetComponent<Renderer>().sharedMaterial.color = map.map[i,j].myColor;

				}
			}
				
			InfluenceMapTexture.Apply ();

		}

	}








//
//		public void CreateMap(int x, int y, float spacing, GameObject gridObjects, bool RenderGround){
//			RenderGroundGrid = RenderGround;
//			InfluenceMapTexture = new Texture2D (x, y);
//			updateTimer = .5f;
//			m_Grid = new GridPosition[x, y];
//			for (int i = 0; i < x; i++) {
//				for (int j = 0; j < y; j++) {
//					GridPosition gTemp = new GridPosition ();
//					gTemp.influences = new List<KeyValuePair<int, float>> ();
//					gTemp.neighbors = new List<GridPosition> ();
//					gTemp.worldObject = GameObject.Instantiate (gridObjects, new Vector3 ((-1 * x / 2 + i) * spacing, 0, (-1 * y / 2 + j) * spacing), Quaternion.identity) as GameObject;
//					gTemp.worldPosition = gTemp.worldObject.transform.position;
//					gTemp.worldObject.GetComponent<InfluencePosition> ().GridPosition = new int[2] { i, j };
//					m_Grid [i, j] = gTemp;
//				}
//			}
//
//			for (int i = 0; i < x; i++) {
//				for (int j = 0; j < y; j++) {
//					if (i % x == 0) {
//						m_Grid [i, j].neighbors.Add (m_Grid [i + 1, j]);
//						if (j % y == 0) {
//							m_Grid [i, j].neighbors.Add (m_Grid [i, j + 1]);
//						} else if (j % y == y - 1) {
//							m_Grid [i, j].neighbors.Add (m_Grid [i, j - 1]);
//						} else {
//							m_Grid [i, j].neighbors.Add (m_Grid [i, j - 1]);
//							m_Grid [i, j].neighbors.Add (m_Grid [i, j + 1]);
//						}
//					} else if (i % x == x - 1) {
//						m_Grid [i, j].neighbors.Add (m_Grid [i - 1, j]);
//						if (j % y == 0) {
//							m_Grid [i, j].neighbors.Add (m_Grid [i, j + 1]);
//						} else if (j % y == y - 1) {
//							m_Grid [i, j].neighbors.Add (m_Grid [i, j - 1]);
//						} else {
//							m_Grid [i, j].neighbors.Add (m_Grid [i, j - 1]);
//							m_Grid [i, j].neighbors.Add (m_Grid [i, j + 1]);
//						}
//					} else if (j % y == y - 1) {
//						m_Grid [i, j].neighbors.Add (m_Grid [i, j - 1]);
//						m_Grid [i, j].neighbors.Add (m_Grid [i + 1, j]);
//						m_Grid [i, j].neighbors.Add (m_Grid [i - 1, j]);
//					} else {
////						m_Grid [i, j].neighbors.Add (m_Grid [i, j - 1]);
//						m_Grid [i, j].neighbors.Add (m_Grid [i, j + 1]);
//						m_Grid [i, j].neighbors.Add (m_Grid [i + 1, j]);
//						m_Grid [i, j].neighbors.Add (m_Grid [i - 1, j]);
//
//					}
//				}
//			}
//		}

//		public void UpdateMap(int sizeY){
//			updateTimer -= Time.deltaTime;
//			if (updateTimer < 0) {
//				return;
//			}
//			updateTimer = .5f;
//
//			for (int i = 0; i < m_Grid.GetLength (0); i++) {
//				for (int j = 0; j < m_Grid.GetLength (1); j++) {
//					m_Grid [i, j].influences.Clear ();
//					m_Grid [i, j].myColor = Color.black;
//
//					if (m_Grid [i, j].worldObject.GetComponent<Renderer> ().enabled) {
//						m_Grid [i, j].worldObject.GetComponent<Renderer> ().material.color = Color.black;
//						m_Grid [i, j].worldObject.GetComponent<Renderer> ().enabled = false;
//					}
//					
//				}
//			}
//
//			for (int i = 0; i < m_OriginatorList.Count; i++) {
//				Collider[] influencePositions = Physics.OverlapSphere (m_OriginatorList [i].worldPosition, m_OriginatorList [i].influenceRange, InfluenceMask);
//						
//
//				// método para evitar que se "salga"
////				Collider[] aux;
////				for (int inf = 0; inf < influencePositions.Length; inf++){
//				//	if (influencePositions[inf].transform.position > m_OriginatorList[inf] + m_OriginatorList[inf].influenceRange){
//					// NO LO AÑADO, PORQUE SE PASA DEL RANGO
//				//}
////				}
//				//influencePositions = aux;
//
//
//				for (int j = 0; j < influencePositions.Length; j++) {
//
//					// LA POSICION X y Z de WORLDPOSITION DEBE SER MAYOR O IGUAL QUE INFLUENCE RANGE. NECESITAMOS CONTROLARLO AL COGER LA LISTA DE INFLUENCE POINTS SI AÑADIMOS UN NUEVO WAYPOINT
//				
//						GridPosition currentPos = m_Grid [influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [0], influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [1]];
//						if (!m_Grid [influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [0], influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [1]].HasInfluenceOfType (m_OriginatorList [i].type)) {
//							m_Grid [influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [0], influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [1]].influences.Add (
//								new KeyValuePair<int, float> (m_OriginatorList [i].type, (((Vector3.Distance (m_OriginatorList [i].worldPosition, influencePositions [j].transform.position)))) / m_OriginatorList [i].influenceRange));
//						} else {
//							KeyValuePair<int, float> iPos = m_Grid [influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [0], influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [1]].GetInfluenceOfType (m_OriginatorList [i].type);
//							if (iPos.Value != -1) {
//								m_Grid [influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [0], influencePositions [j].GetComponent<InfluencePosition> ().GridPosition [1]].influences [iPos.Key] = new KeyValuePair<int, float> (m_OriginatorList [i].type, iPos.Value +
//								(((Vector3.Distance (m_OriginatorList [i].worldPosition, influencePositions [j].transform.position))) / m_OriginatorList [i].influenceRange));
//							}
//						}
//						// no se fusionan bien los colores
//						for (int k = 0; k < currentPos.influences.Count; k++) {
//							if (RenderGroundGrid) {
//								currentPos.worldObject.GetComponent<Renderer> ().enabled = true;
//								currentPos.worldObject.GetComponent<Renderer> ().material.color += ((Color)m_OriginatorList [i].color) * (currentPos.influences [k].Value) * (m_OriginatorList [i].influence);
//							}
//							currentPos.myColor += ((Color)m_OriginatorList [i].color) * (currentPos.influences [k].Value) * (m_OriginatorList [i].influence);
////						if (!RenderGroundGrid) {
////							currentPos.worldObject.GetComponent<Renderer> ().enabled = false;
////						}
//					}
//				}
//			}
//
//			//no se dibuja la textura
//			for (int i = 0; i < m_Grid.GetLength (0); i++) {
//				for (int j = 0; j < m_Grid.GetLength (1); j++) {
//					m_Grid [i, j].myColor.a = 255;
//					InfluenceMapTexture.SetPixel (i, j, m_Grid [i, j].myColor);
//				}
//			}
//
//			InfluenceMapTexture.Apply ();
//		}

}

