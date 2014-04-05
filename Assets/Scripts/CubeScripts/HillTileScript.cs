using UnityEngine;
using System.Collections;

public class HillTileScript : MonoBehaviour {

	public Texture grass;

	void OnMouseDown() {
		renderer.material.mainTexture = grass;
		Vector3 size = new Vector3 (1f, 1f, 1f);
		GetComponent<Transform> ().localScale = size;
	}
}
