using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptedMap : MonoBehaviour
{

    public static AdaptedMap Instance;
	public GameObject selectedUnit;
	public TileType[] tileTypes;
	public Node[,] graph;

    public Hex[,] map;

	public int[,] tiles;
	public int height = 30;
	public int width = 50;

	float xOffset = 1.77f;
	float zOffset = 1.51f;

	void Awake(){
        Instance = this;
    }


	void Start () {
        map = new Hex[width, height];
        selectedUnit = null;
        GenerateMapData();
		GenerateDataInfo(gameObject);

		GeneratePathFindgGraph();
		//GenerateMapVisual();
	}

    private void GenerateDataInfo(GameObject mapa)
    {
        for(int i=0; i < gameObject.transform.childCount; i++)
        {
            Hex h = gameObject.transform.GetChild(i).GetComponent<Hex>();
            map[h.tileX, h.tileY] = h;

//			h.GetComponent<Hex>().x = h.transform.position.x;
//			h.GetComponent<Hex>().y = h.transform.position.y;


			//-----//-----//------//------//------//
			// INFLUENCE MAP //
			h.GetComponent<Hex> ().worldObject = h.transform.GetChild(0).gameObject;
			h.GetComponent<Hex> ().worldPosition = h.transform.position;
			//-----//-----//------//------//------//

//            tiles[h.tileX, h.tileY] = h.gameObject.transform.GetChild(0).GetComponent<hexType>().type;
        }
    }



    void Update () {

		if (selectedUnit == null)
			return;
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				
				if(selectedUnit.transform.position == map[x, y].transform.position)
				{
					selectedUnit.GetComponent<Pathfinding>().tileX = map[x, y].tileX;
					selectedUnit.GetComponent<Pathfinding>().tileY = map[x, y].tileY;
				}
			}
		}
	}


	void GenerateMapData()
	{
		tiles = new int[width, height];
		map = new Hex[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				tiles[x, y] = 0;
			}
		}
		// montar mapa a partir de aquí
		for (int x = 0; x <= 19; x++)
		{
			for (int y = 0; y < 1; y++)
			{
				tiles[x, y] = 2;
			}
		}
		for (int x = 20; x <= 23; x++)
		{
			for (int y = 0; y < 1; y++)
			{
				tiles[x, y] = 1;
			}
		}
		for (int x = 19; x <= 22; x++)
		{
			for (int y = 1; y < 2; y++)
			{
				tiles[x, y] = 1;
			}
		}
		for (int x = 20; x <= 23; x++)
		{
			for (int y = 2; y < 3; y++)
			{
				tiles[x, y] = 1;
			}
		}
		for (int x = 19; x <= 23; x++)
		{
			for (int y = 3; y < 4; y++)
			{
				tiles[x, y] = 1;
			}
		}
		for (int x = 20; x <= 23; x++)
		{
			for (int y = 4; y < 5; y++)
			{
				tiles[x, y] = 1;
			}
		}
		for (int x = 19; x <= 22; x++)
		{
			for (int y = 5; y < 6; y++)
			{
				tiles[x, y] = 1;
			}
		}
		for (int x = 19; x <= 22; x++)
		{
			for (int y = 6; y <7; y++)
			{
				tiles[x, y] = 1;
			}
		}
		for (int x = 18; x <= 21; x++)
		{
			for (int y = 7; y < 8; y++)
			{
				tiles[x, y] = 1;
			}
		}
		for (int x = 18; x <= 21; x++)
		{
			for (int y = 8; y < 9; y++)
			{
				tiles[x, y] = 1;
			}
		}
		//-------------
		for (int x = 17; x <= 20; x++)
		{
			for (int y = 9; y < 10; y++)
			{
				tiles[x, y] = 1;
			}
		}
		//-------------
		for (int x = 17; x <= 20; x++)
		{   
			for (int y = 10; y < 11; y++)
			{
				tiles[x, y] = 1;
			}
		}
		//-------------
		for (int x = 16; x <= 19; x++)
		{
			for (int y = 11; y <= 11; y++)
			{
				tiles[x, y] = 1;
			}
		}
		//-------------
		for (int x = 16; x <= 19; x++)
		{
			for (int y = 12; y <=12; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 31; x <= 38; x++)
		{
			for (int y = 0; y <= 0; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 30; x <= 35; x++)
		{
			for (int y = 1; y <= 1; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 31; x <= 34; x++)
		{
			for (int y = 2; y <= 2; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 31; x <= 34; x++)
		{
			for (int y = 3; y <= 3; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 32; x <= 35; x++)
		{
			for (int y = 4; y <= 4; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 32; x <= 35; x++)
		{
			for (int y = 5; y <= 5; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 33; x <= 36; x++)
		{
			for (int y = 6; y <= 6; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 33; x <= 36; x++)
		{
			for (int y = 7; y <= 7; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 34; x <= 36; x++)
		{
			for (int y = 8; y <= 8; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 34; x <= 35; x++)
		{
			for (int y = 9; y <= 9; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------
		for (int x = 0; x <= 0; x++)
		{
			for (int y = 2; y <= 19; y++)
			{
				tiles[x, y] = 1;
			}
		}//--
		//-------------

		for (int y = 4; y <= 18; y++)
		{
			tiles[1, y] = 1;
		}
		//--
		//-------------

		for (int y = 10; y <= 16; y++)
		{
			tiles[2, y] = 1;
		}
		//--
		//-------------

		for (int y = 3; y <= 5; y++)
		{
			tiles[7, y] = 1;
		}
		//--
		//-------------

		for (int y = 2; y <= 6; y++)
		{
			tiles[8, y] = 1;
		}
		//--
		//-------------

		for (int y = 2; y <= 6; y++)
		{
			tiles[9, y] = 1;
		}
		//--
		//-------------

		for (int y = 2; y <= 6; y++)
		{
			tiles[10, y] = 1;
		}
		//--


		//-------------

		for (int y = 13; y <= 29; y++)
		{
			tiles[49, y] = 1;
		}
		//--
		//-------------

		for (int y = 15; y <= 29; y++)
		{
			tiles[48, y] = 1;
		}

		//--
		//-------------

		for (int y = 23; y <= 29; y++)
		{
			tiles[47, y] = 1;
		}

		//--
		//-------------

		for (int y = 27; y <= 29; y++)
		{
			tiles[46, y] = 1;
		}

		//--
		//-------------

		for (int x = 38; x <= 45; x++)
		{
			tiles[x, 29] = 1;
		}

		//--
		//-------------
		for (int x = 39; x <= 44; x++)
		{
			tiles[x, 29] = 1;
		}
		//--
		//-------------
		for (int y = 3; y <= 5; y++)
		{
			tiles[7, y] = 1;
		}
		//--
		//-------------
		for (int y = 2; y <= 6; y++)
		{
			tiles[8, y] = 1;
		}
		//--
		//-------------
		for (int y = 2; y <= 7; y++)
		{
			tiles[9, y] = 1;
		}
		//--
		//-------------
		for (int y = 2; y <= 6; y++)
		{
			tiles[10, y] = 1;
		}
		//--
		//-------------
		for (int y = 17; y <= 29; y++)
		{
			tiles[14, y] = 1;
		}
		//--
		//-------------
		for (int y =17; y <= 26; y++)
		{
			tiles[15, y] = 1;
		}
		//--
		//-------------
		for (int y = 25; y <= 29; y++)
		{
			tiles[12, y] = 1;
		}
		//--
		//-------------
		for (int y = 27; y <= 29; y++)
		{
			tiles[11, y] = 1;
		}
		//--
		//-------------
		for (int y = 21; y <= 29; y++)
		{
			tiles[13, y] = 1;
		}
		//--
		//-------------
		for (int y = 17; y <= 24; y++)
		{
			tiles[16, y] = 1;
		}
		//--
		//-------------
		for (int y = 18; y <= 22; y++)
		{
			tiles[17, y] = 1;
		}
		//--
		//-------------
		for (int y = 22; y <= 28; y++)
		{
			tiles[38, y] = 1;
		}
		//--
		//-------------
		for (int y = 24; y <= 28; y++)
		{
			tiles[39, y] = 1;
		}
		//--
		//-------------
		for (int y = 26; y <= 28; y++)
		{
			tiles[40, y] = 1;
		}
		//--
		//-------------
		for (int y = 20; y <= 27; y++)
		{
			tiles[37, y] = 1;
		}
		//--
		//-------------
		for (int y = 16; y <= 25; y++)
		{
			tiles[36, y] = 1;
		}
		//--
		//-------------
		for (int y = 17; y <= 21; y++)
		{
			tiles[35, y] = 1;
		}
		//--
		//-------------
		for (int y = 11; y <= 19; y++)
		{
			tiles[28, y] = 1;
		}
		//--
		//-------------
		for (int y = 12; y <= 18; y++)
		{
			tiles[29, y] = 1;
		}
		//--
		//-------------
		for (int y = 12; y <= 18; y++)
		{
			tiles[30, y] = 1;
		}
		//--
		//-------------
		for (int y = 14; y <= 17; y++)
		{
			tiles[31, y] = 1;
		}
		//--
		//-------------
		for (int y = 14; y <= 16; y++)
		{
			tiles[32, y] = 1;
		}
		//--
		//-------------
		for (int x = 27; x <= 29; x++)
		{
			tiles[x, 25] = 1;
		}
		//--
		//-------------
		for (int x = 28; x <= 31; x++)
		{
			tiles[x, 24] = 1;
		}
		//--
		//-------------
		for (int x = 27; x <= 28; x++)
		{
			tiles[x, 26] = 1;
		}
		//--
		//-------------
		for (int y = 13; y <= 19; y++)
		{
			tiles[27, y] = 1;
		}
		//--
		//-------------
		for (int y = 17; y <= 19; y++)
		{
			tiles[26, y] = 1;
		}
		//--
		//-------------
		for (int y = 20; y <= 29; y++)
		{
			tiles[0, y] = 2;
		}
		//--
		//-------------
		for (int y = 11; y <= 14; y++)
		{
			tiles[9, y] = 2;
		}
		//--
		//-------------
		for (int y = 28; y <= 29; y++)
		{
			tiles[1, y] = 2;
		}
		//--
		//-------------
		for (int y = 18; y <= 21; y++)
		{
			tiles[6, y] = 2;
		}
		//--
		//-------------
		for (int y = 12; y <= 20; y++)
		{
			tiles[7, y] = 2;
		}
		//--
		//-------------
		for (int y = 12; y <= 16; y++)
		{
			tiles[8, y] = 2;
		}
		//--
		//-------------
		for (int x = 2; x <= 9; x++)
		{
			tiles[x, 29] = 2;
		}
		//--
		//-------------
		for (int y = 15; y <= 17; y++)
		{
			tiles[6, y] = 2;
		}
		//--
		//-------------
		for (int y = 12; y <= 18; y++)
		{
			tiles[25, y] = 2;
		}
		//--
		//-------------
		for (int y = 13; y <= 18; y++)
		{
			tiles[24, y] = 2;
		}
		//--
		//-------------
		for (int y = 14; y <= 17; y++)
		{
			tiles[23, y] = 2;
		}
		//--
		//-------------
		for (int x = 24; x <= 26; x++)
		{
			tiles[x, 3] = 2;
		}
		//--
		//-------------
		for (int x = 24; x <= 30; x++)
		{
			tiles[x, 0] = 2;
		}
		//--
		//-------------
		for (int x = 23; x <= 29; x++)
		{
			tiles[x, 1] = 2;
		}
		//--
		//-------------
		for (int x = 24; x <= 28; x++)
		{
			tiles[x, 2] = 2;
		}
		//--
		//-------------
		for (int y = 12; y <= 16; y++)
		{
			tiles[26, y] = 2;
		}
		//--
		//-------------
		for (int x = 39; x <= 49; x++)
		{
			tiles[x, 0] = 2;
		}
		//--
		//-------------
		for (int x = 37; x <= 49; x++)
		{
			tiles[x, 1] = 2;
		}
		//--
		//-------------
		for (int x = 39; x <= 49; x++)
		{
			tiles[x, 2] = 2;
		}
		//--
		//-------------
		for (int x = 39; x <= 41; x++)
		{
			tiles[x, 3] = 2;
		}
		//--
		//-------------
		for (int x = 47; x <= 49; x++)
		{
			tiles[x, 4] = 2;
		}
		//--
		//-------------
		for (int y = 4; y <= 12; y++)
		{
			tiles[49, y] = 2;
		}
		//--
		//-------------
		for (int y = 2; y <= 11; y++)
		{
			tiles[48, y] = 2;
		}
		//--
		//-------------
		for (int y = 2; y <= 8; y++)
		{
			tiles[47, y] = 2;
		}
		//--
		//-------------
		for (int y = 15; y <= 20; y++)
		{
			tiles[40, y] = 2;
		}
		//--
		//-------------
		for (int y =15; y <= 21; y++)
		{
			tiles[38, y] = 2;
		}
		//--
		//-------------
		for (int y = 15; y <= 23; y++)
		{
			tiles[39, y] = 2;
		}
		//--
		//-------------
		for (int y = 16; y <= 17; y++)
		{
			tiles[37, y] = 2;
		}
		//--
		//-------------
		for (int y = 16; y <= 18; y++)
		{
			tiles[41, y] = 2;
		}
		//--
		//-------------
		for (int x = 15; x <= 37; x++)
		{
			tiles[x, 29] = 2;
		}
		//--
		//-------------
		for (int x = 15; x <= 29; x++)
		{
			tiles[x, 28] = 2;
		}
		//--
		//-------------
		for (int x = 15; x <= 26; x++)
		{
			tiles[x, 27] = 2;
		}
		//--
		//-------------
		for (int x = 16; x <= 24; x++)
		{
			tiles[x, 26] = 2;
		}
		//--
		//-------------
		for (int x = 16; x <= 23; x++)
		{
			tiles[x, 25] = 2;
		}
		//--
		//-------------
		for (int x = 17; x <= 22; x++)
		{
			tiles[x, 24] = 2;
		}
		//--
		//-------------
		for (int x = 17; x <= 21; x++)
		{
			tiles[x, 23] = 2;
		}
		//--
		//-------------
		for (int x = 18; x <= 19; x++)
		{
			tiles[x, 21] = 2;
		}
		//--
		//-------------
		for (int x = 18; x <= 21; x++)
		{
			tiles[x, 22] = 2;
		}
		//--
		tiles[0, 1] = 2;
		tiles[18, 20] = 2;
		tiles[23,24]= 2;
		tiles[25,26]= 2;
		tiles[37, 19] = 2;
		tiles[46, 3] = 2;
		tiles[49, 3] = 2;
		tiles[47, 9] = 2;
		tiles[27,12]= 2;
		tiles[22,15]= 2;
		tiles[22, 16] = 2;
		tiles[21, 15] = 2;
		tiles[10,12]= 2;
		tiles[25,19]= 1;
		tiles[25,18]= 1;
		tiles[37, 18] = 1;
		tiles[41, 28] = 1;
		tiles[10, 29] = 1;
		tiles[11, 6] = 1;
		tiles[46, 25] = 1;
		tiles[46, 26] = 1;
		tiles[45, 27] = 1;
		tiles[45, 28] = 1;
		tiles[11, 6]= 1;
		tiles[11, 4] = 1;
		tiles[35, 10] = 1;
		//-------------------------------------------------------------
		for (int x = 10; x <= 17; x++)
		{
			for (int y = 1; y < 2; y++)
			{
				tiles[x, y] = 2;
			}
		}
		for (int x = 12; x <= 16; x++)
		{
			for (int y = 2; y < 3; y++)
			{
				tiles[x, y] = 2;
			}
		}
        /*tiles[4, 4] = 2;
		tiles[5, 4] = 2;
		tiles[6, 4] = 2;
		tiles[7, 4] = 2;
		tiles[8, 4] = 2;

		tiles[4, 5] = 2;
		tiles[4, 6] = 2;
		tiles[8, 5] = 2;
		tiles[8, 6] = 2;*/
    }

	public float CostToEnterTile (int sourceX, int sourceY, int targetX, int targetY)
	{
		TileType tt = tileTypes[tiles[targetX, targetY]];

		if (UnitCanEnterTile(targetX, targetY) == false)
			return Mathf.Infinity;
		float cost = tt.momevementCost;

		if (sourceX != targetX && sourceY != targetY)
		{
			cost += 0.001f;
		}

		return cost;
	}
		
	void GeneratePathFindgGraph()
	{
		graph = new Node[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				graph[x, y] = new Node();
				graph[x, y].x = x;
				graph[x, y].y = y;
			}
		}

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				//if pair row
				if (y % 2 == 0)
				{
					//left neighbours
					if (x > 0)
					{
						graph[x, y].neighbours.Add(graph[x - 1, y]);
						if (y > 0)
							graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
						if (y < height - 1)
							graph[x, y].neighbours.Add(graph[x - 1, y + 1]);

					}
					//Right neighbours
					if (x < width - 1)
						graph[x, y].neighbours.Add(graph[x + 1, y]);
					if (y > 0)
						graph[x, y].neighbours.Add(graph[x, y - 1]);
					if (y < height - 1)
						graph[x, y].neighbours.Add(graph[x, y + 1]);

				}

				//odd row
				else
				{
					//left neighbours
					if (x > 0)
						graph[x, y].neighbours.Add(graph[x - 1, y]);
					if (y > 0)
						graph[x, y].neighbours.Add(graph[x, y - 1]);
					if (y < height - 1)
						graph[x, y].neighbours.Add(graph[x, y + 1]);

					//Right neighbours
					if (x < width - 1)
					{
						graph[x, y].neighbours.Add(graph[x + 1, y]);
						if (y > 0)
							graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
						if (y < height - 1)
							graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
					}
				}
			}
		}
	}

	void GenerateMapVisual()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				float xPos = x * xOffset;

				if (y % 2 == 1)
				{
					xPos += xOffset / 2f;
				}
				TileType tt = tileTypes[tiles[x, y]];
				GameObject hex_go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(xPos, 0, y * zOffset), Quaternion.identity);
				hex_go.name = "Hex_" + x + "_" + y;

				hex_go.GetComponent<Hex>().x = x;
				hex_go.GetComponent<Hex>().y = y;

				hex_go.GetComponent<Hex>().tileX = x;
				hex_go.GetComponent<Hex>().tileY = y;
				map[x, y] = hex_go.GetComponent<Hex>();

				hex_go.transform.SetParent(this.transform);

				//-----//-----//------//------//------//
				// INFLUENCE MAP //
				hex_go.GetComponent<Hex> ().worldObject = hex_go.transform.GetChild(0).gameObject;
				hex_go.GetComponent<Hex> ().worldPosition = hex_go.transform.position;
				//-----//-----//------//------//------//

			}
		}
	}
	public Vector3 TileCoordToWorldCoord(int x, int y)
	{
		return new Vector3(map[x,y].x, 0,map[x,y].y);
	}

	public bool UnitCanEnterTile(int x, int y)
	{
		return tileTypes[tiles[x, y]].isWalkable;
	}

	public void GeneratePathTo(int x, int y)
	{
		if (selectedUnit == null)
			return;

		selectedUnit.GetComponent<Pathfinding> ().currentPath = null;


		Dictionary<Node, float> dist = new Dictionary<Node, float>();
		Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

		List<Node> unvisited = new List<Node>();

		Node source = graph[selectedUnit.GetComponent<Pathfinding>().tileX,selectedUnit.GetComponent<Pathfinding>().tileY];
		Node target = graph[x, y];

		dist[source] = 0;
		prev[source] = null;

		foreach (Node v in graph)
		{
			if (v != source)
			{
				dist[v] = Mathf.Infinity;
				prev[v] = null;
			}
			unvisited.Add(v);
		}

		while (unvisited.Count > 0)
		{

			Node u = null;

			foreach (Node possibleU in unvisited)
			{
				if ( u == null || dist[possibleU] < dist[u])
				{
					u = possibleU;
				}
			}

			if (u == target)
			{
				break;
			}

			unvisited.Remove(u);

			foreach (Node v in u.neighbours)
			{
				float alt = dist[u] +  CostToEnterTile(u.x,u.y,v.x,v.y);
				if(alt < dist[v])
				{
					dist[v] = alt;
					prev[v] = u;
				}
			}

		}

		if(prev[target] == null)
		{
			return;
		}

		List<Node> currentPath = new List<Node>();

		Node curr = target;

		while (curr != null)
		{
			currentPath.Add(curr);
			curr = prev[curr];
		}

		currentPath.Reverse();

		selectedUnit.GetComponent<Pathfinding>().currentPath = currentPath;
	}
}


