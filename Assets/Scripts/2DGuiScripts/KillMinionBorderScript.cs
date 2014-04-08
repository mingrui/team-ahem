using UnityEngine;
using System.Collections;

public class KillMinionBorderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnTriggerEnter(Collider minion){
		if (minion.tag == "Player") {
			Destroy (minion.gameObject);
			StopDetectionScript.minionDead ++;
			TileMakeScript.maxMinions++;
			StopDetectionScript.score -= 50;
		}
	}
}
