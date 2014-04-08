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
			StopDetectionScript.minionDead ++;
			TileMakeScript.maxMinions++;
			StopDetectionScript.score -= 50;
			Destroy (minion.gameObject);

		}
	}
}
