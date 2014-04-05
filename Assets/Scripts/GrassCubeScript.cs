using UnityEngine;
using System.Collections;

public class GrassCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnMouseDown() {
		if (GlobalConst.onMove) {
			GlobalConst.selectedObj.transform.position = new Vector3(gameObject.transform.position.x, GlobalConst.selectedObj.transform.position.y, gameObject.transform.position.z);
			//GlobalConst.Set_Position(GlobalConst.selectedObj.transform.position, transform.position);
			GlobalConst.onMove = false;
		}
	}
}
