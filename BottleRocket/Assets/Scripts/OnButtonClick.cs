using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButtonClick : MonoBehaviour {
    
    public void OnButtonDown(string command)
    {
        if (command == "start")
            SceneManager.LoadScene("Game");

        else if (command == "quit")
            Application.Quit();

        else if (command == "home")
            SceneManager.LoadScene("MainMenu");
        



    }
}
