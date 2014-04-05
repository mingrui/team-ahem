using UnityEngine;
using System.Collections;

public class WaterTileScript : MonoBehaviour {

	public Texture grass;
	public Texture grassLight;
	
	public Texture water;
	public Texture waterLight;
	
	public bool waterGone = false;

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

	void OnMouseDown() {
		renderer.material.mainTexture = grass;
		Vector3 size = new Vector3 (1f, 1f, 1f);
		GetComponent<BoxCollider> ().size = size;
		waterGone = true;
	}

	void OnMouseEnter() {
		if (waterGone == false) {
			renderer.material.mainTexture = waterLight;
		} else {
			renderer.material.mainTexture = grassLight;
		}
	}
	
	void OnMouseExit() {
		if (waterGone == false) {
			renderer.material.mainTexture = water;
		}
		else {
			renderer.material.mainTexture = grass;
		}
	}

}
