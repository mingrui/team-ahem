using UnityEngine;
using System.Collections;

public class WaterTileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider minion){
		if (minion.tag == "Player") {
			minion.GetComponent<MovementControl>().changeSpeed(0.5f);
			minion.GetComponent<MovementControl>().inWater = true;
				}
		}

	void OnTriggerExit(Collider minion){
		if (minion.tag == "Player") {

			minion.GetComponent<MovementControl>().changeSpeed(2f);

			minion.GetComponent<MovementControl>().inWater = false;
		}
	}

}
