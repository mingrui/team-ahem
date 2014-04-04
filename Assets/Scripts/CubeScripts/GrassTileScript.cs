using UnityEngine;
using System.Collections;

public class GrassTileScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter() {

		CursorScript.pos = new Vector3(gameObject.transform.position.x, 6f, gameObject.transform.position.z);
	}
}
