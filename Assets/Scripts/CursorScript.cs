using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

	public static Vector3 pos = new Vector3 (0f, 5f, 0f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = pos;
	
	}
}
