using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public float startTime, endTime;
	void Start ()
    {
        startTime = Time.time;
	}
	
    public float getScore(float endTime)
    {
        return endTime - startTime;
    }
}
