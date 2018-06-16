using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnButtonClick : MonoBehaviour {
    private RealGameManager rgm;
    private Text numPlayers;

    public void OnButtonDown(string command)
    {
        if(!rgm)
            rgm = GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<RealGameManager>();

        if (command == "start")
        {
            SceneManager.LoadScene("Tina");
            rgm.setupGame();
        }

        else if (command == "press")//for detecting button input from game while running
        {
            rgm.onButtonDown();
            Debug.Log("Button Pressed!");
            if(rgm.gameRunning)
                GameObject.FindGameObjectWithTag("gameButton").GetComponent<Text>().text = "Shake It!";
        }

        else if(command == "release")//to detect when a button is no longer held in game while running
        {
            rgm.onButtonUp();
            Debug.Log("Button Released!");
            if(rgm.gameRunning)
                GameObject.FindGameObjectWithTag("gameButton").GetComponent<Text>().text = "Tap It!";
        }

        else if (command == "quit")
        {
            Application.Quit();
        }


        else if (command == "home")
        {
            //return to main menu
            SceneManager.LoadScene("MainMenu");
            rgm.reset();
        }

        else if (command == "about"){
            GameObject panel = GameObject.FindGameObjectWithTag("howtoplay");
            TogglePanel(panel);
        }

        else if (command == "partySize")
        {
            //go to change number of players
            SceneManager.LoadScene("PartySize");
            rgm.reset();
        }

        else if (command == "add")
        {
            if (rgm.numPlayers < 10)
            {
                rgm.numPlayers++;
            }
                
            if (!numPlayers)
            {
                numPlayers = GameObject.FindGameObjectWithTag("NumPlayerUI").GetComponent<Text>();
            }
            numPlayers.text = rgm.numPlayers.ToString();
        }

        else if (command == "sub")
        {
            if (rgm.numPlayers > 2)
            {
                rgm.numPlayers--;
            }
                
            if (!numPlayers)
            {
                numPlayers = GameObject.FindGameObjectWithTag("NumPlayerUI").GetComponent<Text>();
            }
            numPlayers.text = rgm.numPlayers.ToString();
        }

        else if (command == "prompt")
        {
            SceneManager.LoadScene("EndGame");
            rgm.reset();
        }
    }


    void TogglePanel(GameObject panel)
    {
        if (panel.GetComponent<CanvasGroup>().alpha == 0)
        {
            panel.GetComponent<CanvasGroup>().alpha = 1;
            panel.GetComponent<CanvasGroup>().interactable = true;
            panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            panel.GetComponent<CanvasGroup>().alpha = 0;
            panel.GetComponent<CanvasGroup>().interactable = false;
            panel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }


    }
}
