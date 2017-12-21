//@author: M Gavilan
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
            ShowCanvas();
		} else {
            Debug.Log("CAMBIO DE TURNO A JUGADOR");
            ActivePlayer = Jugador;
			Jugador.ResetUnits ();
            ShowCanvas();
        }
	}

	public void CheckPlayerTurn(){
        Debug.Log("Compruebo turno");
		if (ActivePlayer.isEndOfTurn ())
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
