using UnityEngine;
using System.Collections;

public class PurpleTileScript : MonoBehaviour {

	public Texture grass;
	public Texture grassLight;

	public Texture purple;
	public Texture purpleLight;

	public bool bananaGone = false;

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
			minion.GetComponent<MovementControl>().isPurple = true;

						renderer.material.mainTexture = grass;
			Vector3 size = new Vector3 (1f, 1f, 1f);
			GetComponent<BoxCollider> ().size = size;
			bananaGone = true;
				}
	}

	void OnMouseDown() {
				renderer.material.mainTexture = grass;
				Vector3 size = new Vector3 (1f, 1f, 1f);
				GetComponent<BoxCollider> ().size = size;
		bananaGone = true;
		}

	void OnMouseEnter() {
		if (bananaGone == false) {
						renderer.material.mainTexture = purpleLight;
				} else {
						renderer.material.mainTexture = grassLight;
				}
	}
	
	void OnMouseExit() {
		if (bananaGone == false) {
						renderer.material.mainTexture = purple;
				}
		else {
			renderer.material.mainTexture = grass;
		}
	}
		
	}
