using UnityEngine;
using System.Collections;

public class CreateMinions : MonoBehaviour {

	public GameObject yellowMinion1;
	public GameObject yellowMinion2;
	public GameObject yellowMinion3;
	public GameObject purpleMinion1;
	public GameObject purpleMinion2;
	public GameObject purpleMinion3;
	
	public float[] startPos1 = new float[] {-9.5f, 3f, -9.5f};
	public float[] startPos2 = new float[] {-9.5f, 3f, -8.5f};
	public float[] startPos3 = new float[] {-9.5f, 3f, -7.5f};

	GameObject ym;

	void startHelper(float[] pos, GameObject o) {
		ym = Instantiate(o, new Vector3(pos[0], pos[1], pos[2]), Quaternion.identity) as GameObject;
		ym.transform.Rotate(0f, 0f, 180f);
	}
	
	// Use this for initialization
	void Start () {
	
		ym = Instantiate(yellowMinion1, new Vector3(-9.5f, 3f, -9.5f), Quaternion.identity) as GameObject;
		ym.transform.Rotate(0f, 0f, 180f);

		ym = Instantiate(yellowMinion2, new Vector3(-9.5f, 3f, -8.5f), Quaternion.identity) as GameObject;
		ym.transform.Rotate(0f, 0f, 180f);

		ym = Instantiate(yellowMinion1, new Vector3(-9.5f, 3f, -7.5f), Quaternion.identity) as GameObject;
		ym.transform.Rotate(0f, 0f, 180f);

		ym = Instantiate(yellowMinion1, new Vector3(9.5f, 3f, 9.5f), Quaternion.identity) as GameObject;
		ym.transform.Rotate(0f, 0f, 180f);
		
		ym = Instantiate(yellowMinion2, new Vector3(9.5f, 3f, 8.5f), Quaternion.identity) as GameObject;
		ym.transform.Rotate(0f, 0f, 180f);
		
		ym = Instantiate(yellowMinion1, new Vector3(9.5f, 3f, 7.5f), Quaternion.identity) as GameObject;
		ym.transform.Rotate(0f, 0f, 180f);


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	public static void example_function(){

	}
	*/
}
