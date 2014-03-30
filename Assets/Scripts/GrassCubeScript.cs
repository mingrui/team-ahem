using UnityEngine;
using System.Collections;

public class GrassCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}


	void OnMouseDown() {
		CursorScript.pos = gameObject.transform.position;
	}
}
