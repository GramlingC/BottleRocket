using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButtonClick : MonoBehaviour {
    public int numPlayer = 1;

    public void OnButtonDown(string command)
    {
        if (command == "start")
            SceneManager.LoadScene("Game");

        else if (command == "quit")
            Application.Quit();

        else if (command == "home")
            SceneManager.LoadScene("MainMenu");

        else if (command == "about")
            SceneManager.LoadScene("Rules");

        else if (command == "add") { if (numPlayer != 10) numPlayer++; }

        else if (command == "sub") { if (numPlayer != 0) numPlayer--; }

        else if (command == "prompt") //to ask players to play again
            SceneManager.LoadScene("EndGame");

    }
}
