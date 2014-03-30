using UnityEngine;
using System.Collections;

public class CreateMinions : MonoBehaviour {

	public GameObject yellowMelee;
	public GameObject yellowRange;
	public GameObject yellowDefense;
	public GameObject purpleMelee;
	public GameObject purpleRange;
	public GameObject purpleDefense;

	private Vector3 startPos1 = new Vector3(-9.5f, 3, -9.5f);
	private Vector3 startPos2 = new Vector3(-9.5f, 3, -8.5f);
	private Vector3 startPos3 = new Vector3(-9.5f, 3, -7.5f);

	private Vector3 startPos4 = new Vector3(9.5f, 3, 9.5f);
	private Vector3 startPos5 = new Vector3(9.5f, 3, 8.5f);
	private Vector3 startPos6 = new Vector3(9.5f, 3, 7.5f);

	void startHelper(Vector3 pos, GameObject obj) {
		GameObject ym = Instantiate(obj, pos, Quaternion.identity) as GameObject;

		ym.transform.Rotate(0f, 0f, 180f);
	}
	
	// Use this for initialization
	void Start () {
		startHelper(startPos1, yellowMelee);
		startHelper(startPos2, yellowMelee);
		startHelper(startPos3, yellowRange);
		startHelper(startPos4, yellowMelee);
		startHelper(startPos5, yellowRange);
		startHelper(startPos6, yellowRange);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	public static void example_function(){

	}
	*/
}
