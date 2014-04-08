using UnityEngine;
using System.Collections;

public class MinionHomeScript : MonoBehaviour {

	public static int minionCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerEnter(Collider minion){
		if (minion.tag == "Player") {
			minionCount ++;
			StopDetectionScript.score += 100;
		}
	}
}
