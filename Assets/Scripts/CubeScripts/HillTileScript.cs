using UnityEngine;
using System.Collections;

public class HillTileScript : MonoBehaviour {

	public Texture grass;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {
		
		CursorScript.pos = new Vector3(gameObject.transform.position.x, 6f, gameObject.transform.position.z);
	}

	void OnMouseDown() {
		renderer.material.mainTexture = grass;
		Vector3 size = new Vector3 (1f, 1f, 1f);
		GetComponent<Transform> ().localScale = size;
	}
}
