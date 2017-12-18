using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour {

	public int currentLevel;

	public GameObject[] Levels;

	public Transform StartPosition;
	public Transform FinishedPosition;

	public Transform Player;

	// Use this for initialization
	void Start () 
	{
		currentLevel = 0;
		Levels [currentLevel].SetActive (true);
		StartPosition = lookForEntry ();
		FinishedPosition = lookForExit ();

		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
        Player.position = StartPosition.position;
	}
		
	void Awake()
	{
		foreach (GameObject level in Levels) 
		{
			level.SetActive (false);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}

	Transform lookForEntry()
	{
		Transform parent = Levels [currentLevel].transform;

		for (int obj = 0; obj < parent.childCount; obj++) 
		{
			if (parent.GetChild (obj).tag == "Entry") 
			{
				return parent.GetChild (obj);
			}
		}

		return null;
	}

	Transform lookForExit()
	{
		Transform parent = Levels [currentLevel].transform;

		for (int obj = 0; obj < parent.childCount; obj++) 
		{
			if (parent.GetChild (obj).tag == "Exit") 
			{
				return parent.GetChild (obj);
			}
		}

		return null;
	}

	public void changeLevel()
	{
		Levels [currentLevel].SetActive (false);
		Levels [currentLevel+1].SetActive (true);

		currentLevel += 1;
		StartPosition = lookForEntry ();
		Player.position = StartPosition.position;
	}
}
