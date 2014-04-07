using UnityEngine;
using System.Collections;

public class StoppedTextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "Minions Stuck: " + StopDetectionScript.minionStopCount.ToString ();
	
	}
}
