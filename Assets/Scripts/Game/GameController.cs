using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [Tooltip("State of the application at this moment")]
    [SerializeField]
    private GameState gameState;

    public GameState GameState
    {
        get
        {
            return gameState;
        }

        set
        {
            gameState = value;
        }
    }

    // Use this for initialization
    void Start () {
        gameState = GameState.MENU;
	}
		

}

public enum GameState
{
    MENU, IDLE, PURSUIT, ALERT, GAME_OVER
}