using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RealGameManager : MonoBehaviour {
    
    public float shakeThreshold;
    public static bool created = false;
    public int numPlayers;
    private int currentPlayer = 0;//0,1,2,...,numPlayers-1
    private enum GameState {Calm, Slow, Fast, Anime};
    private GameState curentState = GameState.Calm;
    public bool gameRunning = false;// set to true upon leaving the player num selection screen
    private bool isButtonDown = false;// only relevant when game is running

    private Vector3 accInput = new Vector3(), prevAccInput = new Vector3();

    GameObject debug;
    Text stuff;

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
	
    //called upon exiting the game scene
    public void reset()
    {
        gameRunning = false;
    }

    public void onButtonDown()
    {
        isButtonDown = true;
    }

    public void onButtonUp()
    {
        isButtonDown = false;
        advanceToNextPlayerTurn();
    }

    private void advanceToNextPlayerTurn()
    {
        if(currentPlayer == numPlayers - 1)
        {
            currentPlayer = 0;
        }
        else
        {
            currentPlayer++;
        }
    }

    public void checkForShake()
    {
        debug = GameObject.FindGameObjectWithTag("debug");
        stuff = debug.GetComponent<Text>();
        prevAccInput = accInput;
        accInput = Input.acceleration;
        Debug.Log(accInput.magnitude);
        
        if (prevAccInput.magnitude < accInput.magnitude && accInput.magnitude >= shakeThreshold)
        {
            //stuff.text = "Detected at:" + accInput.magnitude;
            acceptShake();

            
      
        }
        else
        {
           // stuff.text = "Not detected at: " + accInput.magnitude;
        }
    }

    public void acceptShake()
    {
        Debug.Log("ShakeDetected!");
        Handheld.Vibrate();
    }

	// Update is called once per frame
	void Update () {
        
        if (isButtonDown)
        {
            
            checkForShake();
        }
		
	}
}
