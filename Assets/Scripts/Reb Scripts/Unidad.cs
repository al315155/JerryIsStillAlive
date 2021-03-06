﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unidad : MonoBehaviour
{
	public InfluenceMap iM;

	public SelectionManager selectionManager;

	public TypeOfUnit unitType;
	public GameObject panel;
    public GameObject mySelf;
    public Player Owner;


	public Resource resource;
	public float resourceTimer;
	public float resourceCount;

	public bool finished;

    [Header("UI")]
	//mientras el proyecto no esté ordenado
	public Sprite icon;
	public string kingdom;
	public float lifeSpawn;
	private float life;
    public GameObject mapa;

	//para walkable
	public Material Material;
	public int range;
	public float damage;

	[HideInInspector]public Pathfinding pathfinding;

	[Tooltip("Coste de oxigeno para generar")]
	public int costeOxigeno;
	[Tooltip("Coste de enzimas para generar")]
	public int costeEnzimas;


	bool extract;

	void Awake(){
		selectionManager = GameObject.Find ("Selection Manager").GetComponent<SelectionManager> ();
		pathfinding = GetComponent<Pathfinding> ();
	}

	void Start ()
	{
//		extractingOxygen = false;
//		extractingEnzymes = false;

		resourceTimer = 5f;

		life = lifeSpawn;
		finished = false;
	}

	void Update(){
		if (this.life == 0)
        {
            Destroy(mySelf);
        }

		if (resource != null) {
			resourceCount += Time.deltaTime;

			if (resourceCount > resourceTimer) {
				resource.Extract (5);
				Owner.AddResources (resource.resourceType, 5);
				resourceCount = 0f;
			}
		}

	}

	public bool Finished {
		get{ return finished; }
		set{ finished = value; }
	}

	public float Damage {
		get{ return damage; }
		set{ damage = value; }
	}

	public string Kingodm {
		get{ return kingdom; }
		set{ kingdom = value; }
	}

	public TypeOfUnit UnitType{
		get{ return unitType; }
		set{ unitType = value; }
	}

	public GameObject Panel{
		get{ return panel; }
		set{ panel = value; }
	}

	public Sprite Icon{
		get{ return icon; }
		set{ icon = value; }
	}

	public float LifeSpawn{
		get{ return lifeSpawn; }
		set{ lifeSpawn = value; }
	}

	//Walkable Functions!
	public void DoAttack(Unidad unit){

		if (resource != null) {
			resource = null;
		}

        if (this.pathfinding.tileY % 2 == 0)
        {
            if ((unit.pathfinding.tileX == this.pathfinding.tileX - 1 && unit.pathfinding.tileY == this.pathfinding.tileY)||(unit.pathfinding.tileX == this.pathfinding.tileX-1 && unit.pathfinding.tileY == this.pathfinding.tileY+1)||(unit.pathfinding.tileX == this.pathfinding.tileX  && unit.pathfinding.tileY == this.pathfinding.tileY + 1)||(unit.pathfinding.tileX == this.pathfinding.tileX - 1 && unit.pathfinding.tileY == this.pathfinding.tileY - 1)||(unit.pathfinding.tileX == this.pathfinding.tileX  && unit.pathfinding.tileY == this.pathfinding.tileY - 1)||(unit.pathfinding.tileX == this.pathfinding.tileX +1 && unit.pathfinding.tileY == this.pathfinding.tileY ))
            {
                unit.life -= this.damage;
            }
        }
        else
        {
            if((unit.pathfinding.tileX == this.pathfinding.tileX - 1 && unit.pathfinding.tileY == this.pathfinding.tileY)||(unit.pathfinding.tileX == this.pathfinding.tileX  && unit.pathfinding.tileY == this.pathfinding.tileY+1)||(unit.pathfinding.tileX == this.pathfinding.tileX  && unit.pathfinding.tileY == this.pathfinding.tileY-1)||(unit.pathfinding.tileX == this.pathfinding.tileX + 1 && unit.pathfinding.tileY == this.pathfinding.tileY+1)||(unit.pathfinding.tileX == this.pathfinding.tileX +1 && unit.pathfinding.tileY == this.pathfinding.tileY-1)||(unit.pathfinding.tileX == this.pathfinding.tileX + 1 && unit.pathfinding.tileY == this.pathfinding.tileY))
            {
                unit.life -= this.damage;
            }
        }
     }

	public void DoMove(GameObject hitObject){

		if (resource != null) {			
			resource = null;
		}

		pathfinding.currentPath = null;
		pathfinding.SetSelected (gameObject);
		pathfinding.TileAction (hitObject);
		pathfinding.NextTurn ();
	}

	public void DoWork(Resource resource, Hex placement)
    {

		if (this.resource != null) {
			this.resource = null;
		}


		transform.position = placement.worldPosition;
		this.resource = resource;
		extract = true;
//        switch (_type)
//        {
//            case ResourceType.enzyme:
//                //TODO
//                break;
//            case ResourceType.oxygen:
//                //TODO
//                break;
//        }
    }
//
//	public void Build(Unit building, Transform parent, Player currentPlayer){
//		GameObject newObject = Instantiate(building, parent);
//		newObject.transform.parent = parent;
////		if(newObject.GetComponent<Cuartel>()) newObject.GetComponent<Cuartel>().owner = currentPlayer;
////		if (building.GetComponent<Unidad> ().UnitType.Equals (TypeOfUnit.Turret)) {
////			
////		}
//		building.GetComponent<Unidad>().Owner = currentPlayer;
//	}

	public Unidad BuildUnit(GameObject newUnit, Hex placement, List<Unidad> ListToAdd){

		if (resource != null) {
			resource = null;
		}


		// newUnit es un prefab. Hex es donde se ha clickado.
		if( Owner.oxygen >= costeOxigeno && Owner.enzymes >= costeEnzimas)
		{
			GameObject newObject = Instantiate(newUnit, placement.transform);

			Unidad unit = newObject.GetComponent<Unidad>();

			newObject.transform.position = placement.worldObject.transform.position;
			newObject.SetActive (true);

			if (unit.unitType.Equals (TypeOfUnit.WalkableUnit)) {
				newObject.GetComponent<Unidad> ().pathfinding.tileX = placement.tileX;
				newObject.GetComponent<Unidad> ().pathfinding.tileY = placement.tileY;
			} else {
				iM.AddInfluencePoint (newObject);

			}

			unit.Owner = this.Owner;
            //unit.Owner.Squad.Add(unit);
            ListToAdd.Add(unit);

			Owner.Buy (unit);
            return unit;
		}
        return null;
	}
}

public enum TypeOfUnit
{
	WalkableUnit,
	Building,
    Turret,
    Barracks,
	Nexus
}

public enum TypeOfAction{
	Attack,
	Move,
	WorkOn,
    Build,
	None
}

//public enum TypeOfBuilding
//{
//    Tower,
//    Barracks
//}


	
