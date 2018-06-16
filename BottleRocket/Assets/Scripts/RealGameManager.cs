using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        currentState = GameState.Calm;
        if(bm)
            bm.ChangeBubble((int)currentState);
        currentPlayer = 0;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void setupGame()
    {
        
        gameRunning = true;
        shakeDist = new int[numPlayers];
        shakeDict[0] = calmMin + Random.Range(0, calmMax - calmMin);
        shakeDict[1] = slowMin + Random.Range(0, slowMax - slowMin);
        shakeDict[2] = fastMin + Random.Range(0, fastMax - fastMin);
        transform.GetChild(0).gameObject.SetActive(true);
        if(!bm)
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
        GetComponent<AudioManager>().playSound(2);
        Handheld.Vibrate();

        ++shakeDist[currentPlayer];

        //if(!--shakeDict[currentState])   ;-;
        
        --shakeDict[(int)currentState];
        if (shakeDict[(int)currentState] == 0)
        {
            ++currentState;
            bm.ChangeBubble((int)currentState);
        }
    }

    public void restartGame()
    {
        reset();
        setupGame();
        GameObject.FindGameObjectWithTag("restartButton").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("gameButton").GetComponent<Text>().text = "Tap it!";
    }

    private string generateEndText()
    {
        //NOTE: index 0 player is actually player 1
        string toReturn = "";
        int mostShaken = int.MinValue;
        int mostShakenPlayer = 0;
        int leastShaken = int.MaxValue;
        int leastShakenPlayer = 0;
        for (int i = 0; i < numPlayers; ++i)
        {
            if(shakeDist[i] > mostShaken)
            {
                mostShaken = shakeDist[i];
                mostShakenPlayer = i;
            }else if(shakeDist[i] < leastShaken)
            {
                leastShaken = shakeDist[i];
                leastShakenPlayer = i;
            }
        }

        //toReturn += "Exploded on Player " + (currentPlayer + 1).ToString() + "'s turn!\n";
        //toReturn += "Player " + (mostShakenPlayer + 1).ToString() + " shook the bottle " + (shakeDist[mostShakenPlayer]).ToString() + " times!\n";
        //toReturn += "Player " + (leastShakenPlayer + 1).ToString() + " contributed the least.\n";
        //toReturn = "";
        toReturn += "Player : Shakes\n---------------\n";
        for(int i = 0; i < numPlayers; ++i)
        {
            toReturn += (i + 1).ToString() + " : " + shakeDist[i].ToString() + '\n';
            //toReturn += (shakeDist[i] == mostShaken) ? "  Max!" : "";
            //toReturn += (shakeDist[i] == leastShaken) ? "  Min!\n" : "\n";
        }
        return toReturn;
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
                    Debug.Log(generateEndText());//TODO change this to assign text to a menu
                    GameObject.FindGameObjectWithTag("gameButton").GetComponent<Text>().text = "Game Over!\n\n" + generateEndText();
                    //offer end game menu to go back to player num selection screen, or just to quit the app, cannot reset gamewithout reloading scene for some reason
                    gameRunning = false;

                    //testing reset ability here
                    //restartGame();
                    GameObject.FindGameObjectWithTag("restartButton").transform.GetChild(0).gameObject.SetActive(true);
                    
                }
            }
        }
	}
}
