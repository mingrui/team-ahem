using UnityEngine;
using System.Collections;

public class HillTileScript : MonoBehaviour {

	public Texture grass;
	public Texture grassLight;

	public Texture hill;
	public Texture hillLight;

	public bool hillGone = false;

	void OnMouseDown() {
		renderer.material.mainTexture = grass;
		Vector3 size = new Vector3 (1f, 1f, 1f);
		GetComponent<Transform> ().localScale = size;
		if (hillGone == false) {
						StopDetectionScript.score -= 10;
				}
		hillGone = true;
	}


	void OnMouseEnter() {
		if (hillGone == false) {
			renderer.material.mainTexture = hillLight;
		} else {
			renderer.material.mainTexture = grassLight;
		}
	}
	
	void OnMouseExit() {
		if (hillGone == false) {
			renderer.material.mainTexture = hill;
		}
		else {
			renderer.material.mainTexture = grass;
		}
	}
}
