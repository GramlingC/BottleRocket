using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextButton : MonoBehaviour {
	public GameObject button;
	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		TaskOnClick ();
	}

	void TaskOnClick(){
		if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject == button)
		{
			SceneManager.LoadScene ("EndGame");
		}
	}
}
