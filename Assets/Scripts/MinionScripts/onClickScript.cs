using UnityEngine;
using System.Collections;

public class onClickScript : MonoBehaviour {
	
	Vector3 curPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		/*
		if (GlobalConst.onMoveonMove) {
			//Debug.Log(CursorScript.pos);
			gameObject.transform.position = CursorScript.pos;

			if (curPos.x != gameObject.transform.position.x && curPos.z != gameObject.transform.position.z) {
				curPos = CursorScript.pos;
				onMove = false;
				Debug.Log(onMove);
			}
		}
		*/

	}

	void OnMouseDown() {
		//curPos = gameObject.transform.position;
		GlobalConst.selectedObj = gameObject;
		GlobalConst.onMove = true;

	}
}
