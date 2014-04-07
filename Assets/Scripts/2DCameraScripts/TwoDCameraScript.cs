using UnityEngine;
using System.Collections;

public class TwoDCameraScript : MonoBehaviour {

	public Vector3 movement;
	public GameObject leftBounds;
	public GameObject rightBounds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		movement = transform.position;
		
		if (Input.GetKey(KeyCode.W) && (transform.position.z < 17f)) {
			movement.z = transform.position.z + 0.3f;
			transform.position = movement;
			
		}
		
		if (Input.GetKey (KeyCode.S) && (transform.position.z > -7f)) {
			movement.z = transform.position.z - 0.3f;
			transform.position = movement;
			
		}
		
		if (Input.GetKey (KeyCode.D) && (transform.position.x < 74.7f)) {
			movement.x = transform.position.x + 0.3f;
			transform.position = movement;
			
		}
		
		if (Input.GetKey (KeyCode.A) && (transform.position.x > -4.6f)) {
			movement.x = transform.position.x - 0.3f;
			transform.position = movement;
			
		}
		
		
		if (Input.GetKey (KeyCode.Z) && (camera.orthographicSize < 15f) ) {
			camera.orthographicSize = camera.orthographicSize + 0.3f;
		}
		
		if (Input.GetKey (KeyCode.X) && (camera.orthographicSize > 3f)) {
			camera.orthographicSize = camera.orthographicSize - 0.3f;
		}
	
	}
}
