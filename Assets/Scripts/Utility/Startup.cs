using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

	void Start(){
		Screen.showCursor = false;
		Screen.lockCursor = true;
	}

	void Update() {
		// this is a very bad way to lock cursor, lol
		//Screen.lockCursor = true;
		//Screen.lockCursor = false;
	}
	
}