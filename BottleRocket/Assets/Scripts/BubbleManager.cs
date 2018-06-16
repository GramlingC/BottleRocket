using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour {

    Dictionary<int, int[]> settings;
    ParticleSystem bubble;

    
    private void Start()
    {
        settings = new Dictionary<int, int[]>();
        
        settings.Add(0, new int[] { 1, 5 });
        settings.Add(1, new int[] { 2, 10 });
        settings.Add(2, new int[] { 5, 30 });
        settings.Add(3,new int[] { 40, 80 });

        bubble = GetComponent<ParticleSystem>();
    }
    public void ChangeBubble(int gears)
    {
      //  bubble.Clear();
        bubble.playbackSpeed = settings[gears][0];
        bubble.emissionRate = settings[gears][1];
    }
  //  int count = 0;
    private void Update()
    {
     
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (count ==3)
        //    {
        //        ChangeBubble(count);
        //        count = 0;
        //    }
        //    else
        //    {
        //        ChangeBubble(count);
        //        count++;
        //    }
        //} debug purposes
    }

}
