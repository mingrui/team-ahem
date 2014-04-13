using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public GameObject camera_2d;
	public GameObject camera_3d;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(GlobalConst.camera_switch_key)){
			if(camera_2d.activeSelf){
				camera_2d.SetActive(false);
				camera_3d.SetActive(true);
			}
			else{
				camera_2d.SetActive(true);
				camera_3d.SetActive(false);
			}
		}
	}
}
