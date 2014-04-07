using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	public GameObject waterTile;
	public GameObject grassTile;
	public GameObject hillTile;
	static int width = 20;
	static int lenth = 20;

	public float depth = 1.701241f;
	public float rightCornerX = 0.4738798f;
	public float rightCornerZ = -0.4742584f;
	public float zIncrement = 1.0f;

	public GameObject[,] array = new GameObject[width, lenth];

	// Use this for initialization
	void Start () {
		float curZ = rightCornerZ;

		for (int i = 0; i < width; ++i) {
			GameObject tile = Instantiate(grassTile, new Vector3(rightCornerX, depth, curZ), Quaternion.identity) as GameObject;
			curZ += zIncrement; 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
