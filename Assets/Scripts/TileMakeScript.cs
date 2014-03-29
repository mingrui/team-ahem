using UnityEngine;
using System.Collections;

public class TileMakeScript : MonoBehaviour {


	public GameObject grassTile;
	public GameObject hillTile;
	public GameObject waterTile;
	public GameObject[,] tileArray = new GameObject[20,20];
	private float cornerX = 0;
	private float cornerZ = 0;
	
	// Use this for initialization
	void Start () {

		cornerX = transform.position.x;
		cornerZ = transform.position.z;

		for (float x = 0f; x < 20f; x++) {
						for (float z = 0f; z < 20f; z++) {
								GameObject obj = Instantiate (grassTile, new Vector3 (cornerX + x, 1.701241f , cornerZ + z), Quaternion.identity) as GameObject;
						}
				}





	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
