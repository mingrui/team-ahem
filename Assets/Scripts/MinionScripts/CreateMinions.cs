using UnityEngine;
using System.Collections;

public class CreateMinions : MonoBehaviour {

	public GameObject yellowMinion1;
	public GameObject yellowMinion2;
	public GameObject yellowMinion3;
	public GameObject purpleMinion1;
	public GameObject purpleMinion2;
	public GameObject purpleMinion3;

	private Vector3 startPos1 = new Vector3(-9.5f, 3, -9.5f);
	private Vector3 startPos2 = new Vector3(-9.5f, 3, -8.5f);
	private Vector3 startPos3 = new Vector3(-9.5f, 3, -7.5f);

	private Vector3 startPos4 = new Vector3(9.5f, 3, 9.5f);
	private Vector3 startPos5 = new Vector3(9.5f, 3, 8.5f);
	private Vector3 startPos6 = new Vector3(9.5f, 3, 7.5f);

	void startHelper(Vector3 pos, GameObject o) {
		GameObject ym = Instantiate(o, pos, Quaternion.identity) as GameObject;
		ym.transform.Rotate(0f, 0f, 180f);
	}


	// Use this for initialization
	void Start () {

		startHelper(startPos1, yellowMinion1);
		startHelper(startPos2, yellowMinion2);
		startHelper(startPos3, yellowMinion1);
		startHelper(startPos4, yellowMinion2);
		startHelper(startPos5, yellowMinion1);
		startHelper(startPos6, yellowMinion2);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	public static void example_function(){

	}
	*/
}
