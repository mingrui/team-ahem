using UnityEngine;
using System.Collections;

public class DeadTextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = "Minions Dead: " + StopDetectionScript.minionDead.ToString () + "/" + TileMakeScript.maxMinions.ToString();
	
	}
}
