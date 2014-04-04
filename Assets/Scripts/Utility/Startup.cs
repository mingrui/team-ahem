using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {
	
	void Update() {
		// this is a very bad way to lock cursor, lol
		Screen.lockCursor = true;
		Screen.lockCursor = false;
	}
	
}