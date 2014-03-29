using UnityEngine;
using System.Collections;

public class CreateMinions : MonoBehaviour {

	public GameObject yellowMinion1;
	public GameObject yellowMinion2;
	public GameObject yellowMinion3;
	public GameObject purpleMinion1;
	public GameObject purpleMinion2;
	public GameObject purpleMinion3;

	// first three postion is for yellowMinion 1, 2, 3 then purple mininon 1, 2, 3
	public float[] startPos1 = new float[3] {-9.5f, 3f, -9.5f};

	void Awake() {
		Debug.Log(startPos1[1]);
	}

	// Use this for initialization
	void Start () {

		GameObject ym1 = Instantiate(yellowMinion1, new Vector3(startPos1[0], 3f, startPos1[2]), Quaternion.identity) as GameObject;
		ym1.transform.Rotate(0f, 0f, 180f);
		Debug.Log(startPos1[1]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	public static void example_function(){

	}
	*/
}
