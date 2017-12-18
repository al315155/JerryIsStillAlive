// @author: M Gavilan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public int oxygen;
    public int enzymes;
    public bool isMyTurn;

	public Text unitLabel;
	public Text resourcesLabel;

    public List<Unidad> Squad = new List<Unidad>();

    private void Start()
    {
        //Inicializamos los recursos a 0
        oxygen = 500;
        enzymes = 500;

		ActualizeLabels ();
    }

    public void EnlistUnit(Unidad u)
    {
        Squad.Add(u);
    }

	public void DeleteUnit(Unidad u){
		Squad.Remove (u);
	}

    public void AddResources(ResourceType r, int i)
    {
        switch (r)
        {
            case ResourceType.enzyme:
                enzymes+= i;
                break;
            case ResourceType.oxygen:
                oxygen+= i;
                break;
        }
    }

	public void ResetUnits(){
		foreach (Unidad u in Squad) {
			u.finished = false;
		}
	}

	public bool isEndOfTurn(){
		foreach (Unidad u in Squad) {
			if (!u.finished) {
				isMyTurn = false;
				return false;
			}
		}

		return true;
	}

	public void ActualizeLabels(){
		unitLabel.text = "Units " + Squad.Count;
		resourcesLabel.text = "Oxygen " + oxygen + "\t\t Enzymes " + enzymes;
	}
}
