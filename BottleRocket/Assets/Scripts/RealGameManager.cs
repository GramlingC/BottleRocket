using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealGameManager : MonoBehaviour {

    public static bool created = false;
    public int numPlayers;
    private enum GameState {Calm, Slow, Fast, Anime};
    private GameState curentState = GameState.Calm;
    public bool gameRunning = false;// set to true upon leaving the player num selection screen


    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

	// Use this for initialization
	void Start () {
        numPlayers = 2;
	}
	
    public void reset()
    {
        gameRunning = false;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
