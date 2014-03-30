using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

	public static Vector3 pos = new Vector3 (0f, 6f, 0f);
	public static int clickCount = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = pos;
	}
}
