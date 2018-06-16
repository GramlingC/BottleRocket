using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playSound(int i)
	{
		transform.GetComponentsInChildren<AudioSource>()[i].Play();
	}

	public void stopSound(int i)
	{
		transform.GetComponentsInChildren<AudioSource>()[i].Stop();
	}
	
}