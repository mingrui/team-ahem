using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	public static float timeRemaining = 60f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
	}

	/*
	void OnGUI () {

		if (timeRemaining > 0) {
			string timeLeft = timeRemaining.ToString();
			GUI.Button(new Rect (10,10,100,60), timeLeft);
		} else if (timeRemaining > -3){
			GUI.Button(new Rect (10,10,100,60), "Times Up");
		}
	}
	*/
}
