﻿using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {

	public int speed;
	public GameObject direction_control;

	private GameObject dir_menu;

	public void Move_Character(GlobalConst.Direction dir_enum){
		/*rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		if(dir_enum == GlobalConst.Direction.Left || dir_enum == GlobalConst.Direction.Right){
			rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
		}
		else{
			rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
		}
		*/
		rigidbody.velocity = moving_direction(dir_enum) * speed;
	}

	// start moving right
	void Start () {
		//rigidbody.velocity = moving_direction(GlobalConst.Direction.Right) * speed;
	}

	void Update () {
	}

	Vector3 moving_direction(GlobalConst.Direction _dir){
		Vector3 dir = new Vector3(0, 0, 0);

		if(_dir == GlobalConst.Direction.Left){
			dir = new Vector3(-1, 0, 0);
		}
		else if(_dir == GlobalConst.Direction.Right){
			dir = new Vector3(1, 0, 0);
		}
		else if(_dir == GlobalConst.Direction.Up){
			dir = new Vector3(0, 0, 1);
		}
		else if(_dir == GlobalConst.Direction.Down){
			dir = new Vector3(0, 0, -1);
		}

		return dir;
	}

	// stop minion on collision with object
	void OnCollisionEnter(Collision collision) {
		rigidbody.velocity *= 0;
	}

	// click on this to command this to move in another direction is stopped
	void OnMouseUp() {
		if(rigidbody.velocity != new Vector3(0, 0, 0)){
			return;
		}

		if(dir_menu != null){
			dir_menu.GetComponent<DirectionMenu>().center = gameObject;
			dir_menu.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
			dir_menu.SetActive(true);
			return;
		}

		// instantiate a menu, should be Singleton
		GameObject dir_obj =  (GameObject)Instantiate(direction_control);
		dir_menu = dir_obj;
		dir_obj.GetComponent<DirectionMenu>().center = gameObject;
		dir_obj.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
	}
}