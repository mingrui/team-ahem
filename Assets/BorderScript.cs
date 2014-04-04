using UnityEngine;
using System.Collections;

public class BorderScript : MonoBehaviour {

	public GameObject hillTile;

	// Use this for initialization
	void Start () {

		GameObject border;

		for (int i = 0; i < 90; i ++) {
						border = Instantiate (hillTile, new Vector3 (-9.5f + i, 1f, 20.5f), Quaternion.identity) as GameObject;
				}
		for (int i = 0; i < 90; i ++) {
			border = Instantiate (hillTile, new Vector3 (-9.5f + i, 1f, -10.5f), Quaternion.identity) as GameObject;
		}

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
