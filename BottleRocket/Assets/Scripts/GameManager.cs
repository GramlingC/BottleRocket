using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    // Use this for initialization
    private static bool created = false;
    private RealGameManager rgm;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            
        }
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            if (!rgm)
                rgm = GameObject.FindGameObjectWithTag("RealGameManager").GetComponent<RealGameManager>();
            rgm.reset();
        }
	}
}
