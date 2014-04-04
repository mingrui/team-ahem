using UnityEngine;
using System.Collections;

public class PurpleTileScript : MonoBehaviour {

	public Texture grass;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider minion){
		if (minion.tag == "Player") {
						minion.GetComponent<MovementControl> ().changeSpeed (-1f);
			minion.GetComponentInChildren<TurnPurpleScript>().turnPurple();

						renderer.material.mainTexture = grass;
						GetComponent<BoxCollider> ().enabled = false;
				}
	}
	
	/*void OnTriggerExit(Collider minion){
		if (minion.tag == "Player") {
			minion.GetComponent<MovementControl>().changeSpeed(2f);
		}
	}*/
}
