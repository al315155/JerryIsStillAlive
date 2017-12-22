// @author: M Gavilan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int oxygen;
    public int enzymes;
    public bool isMyTurn;

    public Text unitLabel;
    public Text resourcesLabel;

    public List<Unidad> Squad = new List<Unidad>();
    public List<Unidad> Ejercito = new List<Unidad>();
    public List<Unidad> Pueblo = new List<Unidad>();
    public List<Unidad> Ciudad = new List<Unidad>();

    private void Start()
    {
        //Inicializamos los recursos a 0
        oxygen = 500;
        enzymes = 500;

        if(unitLabel) ActualizeLabels();
    }

    public void EnlistUnit(Unidad u)
    {
        Squad.Add(u);
    }

    public void DeleteUnit(Unidad u)
    {
        Squad.Remove(u);
    }

    public void AddResources(ResourceType r, int i)
    {
        switch (r)
        {
            case ResourceType.enzyme:
                enzymes += i;
                break;
            case ResourceType.oxygen:
                oxygen += i;
                break;
        }
        if (unitLabel) ActualizeLabels();
    }

    public void ResetUnits()
    {
        foreach (Unidad u in Squad)
        {
            u.finished = false;
        }
    }

    public bool isEndOfTurn()
    {
        Debug.Log("Tengo " + Squad.Count + " unidades.");
        foreach (Unidad u in Squad)
        {
            Debug.Log(u.Finished);
            if (!u.finished)
            {
                Debug.Log("¡Aún no he acabado!");
                return false;
            }
        }
        return true;
    }

    public void ActualizeLabels()
    {
        unitLabel.text = "Units " + Squad.Count;
        resourcesLabel.text = "Oxygen " + oxygen + "\t\t Enzymes " + enzymes;
    }

    public void Buy(Unidad unit)
    {
        oxygen -= unit.costeOxigeno;
        enzymes -= unit.costeEnzimas;

        if (unitLabel) ActualizeLabels();
    }


}
