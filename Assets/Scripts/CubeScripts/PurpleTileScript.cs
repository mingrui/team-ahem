﻿using UnityEngine;
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
			Vector3 size = new Vector3 (1f, 1f, 1f);
			GetComponent<BoxCollider> ().size = size;
				}
	}
	
	/*void OnTriggerExit(Collider minion){
		if (minion.tag == "Player") {
			minion.GetComponent<MovementControl>().changeSpeed(2f);
		}
	}*/

	void OnMouseEnter() {
		
		CursorScript.pos = new Vector3(gameObject.transform.position.x, 6f, gameObject.transform.position.z);
	}

	void OnMouseDown() {
				renderer.material.mainTexture = grass;
				Vector3 size = new Vector3 (1f, 1f, 1f);
				GetComponent<BoxCollider> ().size = size;
		}
		
	}