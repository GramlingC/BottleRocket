using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RealGameManager : MonoBehaviour {
    private BubbleManager bm;
    public float shakeThreshold;
    public int calmMin, calmMax, slowMin, slowMax, fastMin, fastMax;
    public static bool created = false;
    public int numPlayers;
    private int currentPlayer = 0;//0,1,2,...,numPlayers-1
    private enum GameState {Calm, Slow, Fast, Anime};
    //private Dictionary<GameState, int> shakeDict;
    private int[] shakeDict = {0,0,0};
    private int[] shakeDist;//counts the total counts per player
    private GameState currentState = GameState.Calm;
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
        shakeDict[0] = calmMin + Random.Range(0, calmMax - calmMin);
        shakeDict[1] = slowMin + Random.Range(0, slowMax - slowMin);
        shakeDict[2] = fastMin + Random.Range(0, fastMax - fastMin);
    }
	
    //called upon exiting the game scene
    public void reset()
    {
        gameRunning = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void setupGame()
    {
        gameRunning = true;
        shakeDist = new int[numPlayers];
        transform.GetChild(0).gameObject.SetActive(true);
        bm = transform.GetChild(0).GetChild(1).GetComponent<BubbleManager>();
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

        if (currentState != GameState.Anime)
        {
            if (true || prevAccInput.magnitude < accInput.magnitude && accInput.magnitude >= shakeThreshold)
            {
                acceptShake();
            }
        }
    }

    public void acceptShake()
    {
        //Handheld.Vibrate();

        //if(!--shakeDict[currentState])   ;-;
        
        --shakeDict[(int)currentState];
        if (shakeDict[(int)currentState] == 0)
        {
            ++currentState;
            bm.ChangeBubble((int)currentState);
        }
    }

	// Update is called once per frame
	void Update () {

        if (gameRunning)
        {
            if (isButtonDown)
            {
                checkForShake();
                if (currentState == GameState.Anime)
                {

                }
            }
        }
	}
}
