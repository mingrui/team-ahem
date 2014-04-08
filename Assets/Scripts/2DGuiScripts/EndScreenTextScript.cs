using UnityEngine;
using System.Collections;

public class EndScreenTextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		guiText.text = null;

		if (TimerScript.timeRemaining > 1) {
			guiText.text = null;
		} else {
			guiText.text = "Score: " + StopDetectionScript.score.ToString () + '\n' +
				"Minions Saved: " + MinionHomeScript.minionCount.ToString () + "/" + TileMakeScript.maxMinions.ToString() + '\n' +
					"Minions Dead: " + StopDetectionScript.minionDead.ToString () + "/" + TileMakeScript.maxMinions.ToString();
		}
	}
}
