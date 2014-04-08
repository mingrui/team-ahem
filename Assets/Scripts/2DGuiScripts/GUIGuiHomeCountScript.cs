using UnityEngine;
using System.Collections;

public class GUIGuiHomeCountScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		guiText.text = "Minions Saved: " + MinionHomeScript.minionCount.ToString () + "/" + TileMakeScript.maxMinions.ToString();
	
	}
}
