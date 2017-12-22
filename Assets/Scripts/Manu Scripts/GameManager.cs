//@author: M Gavilan
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Player Jugador;
    public Player CPU;
    public Player ActivePlayer;

	public GameObject TurnCanvas;
	private Text PlayerText;
    [Header ("Las fuentes de recursos de enzimas")]
    public List<GameObject> enzymesSources = new List<GameObject>();
    [Header("Las fuentes de recursos de oxigeno")]
    public List<GameObject> oxygenSources = new List<GameObject>();


    private void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
		ActivePlayer = Jugador;
		ActivePlayer.isMyTurn = true;
		PlayerText = TurnCanvas.transform.GetChild (1).GetComponent<Text> ();
		ShowCanvas ();
    }

	public void ChangeTurn(){
		if (ActivePlayer.Equals (Jugador)) {
            Debug.Log("CAMBIO DE TURNO A CPU");
			ActivePlayer = CPU;
			CPU.ResetUnits ();
            ExecuteCPUIA();
            ShowCanvas();
		} else {
            Debug.Log("CAMBIO DE TURNO A JUGADOR");
            ActivePlayer = Jugador;
			Jugador.ResetUnits ();
            ShowCanvas();
        }
	}

    private void ExecuteCPUIA()
    {
        CPU.Ciudad[0].GetComponent<NexoIA>().Act();
        if (CPU.Ejercito.Count > 0)
        {
            foreach (Unidad soldado in CPU.Ejercito)
            {
                if(!soldado.finished) soldado.GetComponent<SoldierIA>().Act();
            }
        }
        if (CPU.Pueblo.Count > 0)
        {
            Debug.Log("OH YEAH TENGO " + CPU.Pueblo.Count + " CIUDADANOS");
            //foreach(Unidad ciudadano in CPU.Pueblo)
            //{
            //    ciudadano.finished = true;
            //}
            foreach (Unidad ciudadano in CPU.Pueblo)
            {
                if(!ciudadano.finished) ciudadano.GetComponent<WorkerIA>().Act();
            }
        }


        //foreach(Unidad edificio in CPU.Ciudad){
        //}
        //TODO: MOVIMIENTO DE LOS TRABAJADORES:
    }

    public void CheckPlayerTurn(){
        Debug.Log("Compruebo turno");
		if (ActivePlayer.isEndOfTurn())
			ChangeTurn ();
	}

	public void ShowCanvas(){
		TurnCanvas.SetActive (true);
		PlayerText.text = ActivePlayer.name;
		StartCoroutine (WaitAndHide (4f));
	}

	public IEnumerator WaitAndHide(float nSeconds){
		yield return new WaitForSeconds (nSeconds);
		TurnCanvas.SetActive (false);
	}

}
