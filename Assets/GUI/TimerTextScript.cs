using UnityEngine;
using System.Collections;

public class TimerTextScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//float timeR = (float)(System.Math.Truncate((double)(TimerScript.timeRemaining)*100.0) / 100.0);
		float timeR = (int)(TimerScript.timeRemaining);
		if (timeR > 0) {
			guiText.text = "Time Remaining: " + timeR.ToString ();
		} else {
			guiText.text = "Times Up ";
			Time.timeScale = 0;
		}
		
	}
}
