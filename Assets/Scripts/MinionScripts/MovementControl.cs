﻿using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {

	public Texture purpleMinion;
	public Texture purpleMinionSel;

	public Texture yellowMinion;
	public Texture yellowMinionSel;

	public bool isPurple = false;

	private Vector3 v3Pos;
	private int threshold = 9;


	public int speed;
	public GameObject direction_control;
	//private GlobalConst.Direction minionDir;
	private static GameObject dir_menu;
	public bool inWater = false;

	public void changeSpeed(float scale){
		rigidbody.isKinematic = false;
		rigidbody.velocity *= scale;
	}

	public void Move_Character(GlobalConst.Direction dir_enum){
		rigidbody.velocity = moving_direction(dir_enum) * speed;


		if (inWater == true) {
			rigidbody.velocity *= 0.5f;
				}
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
		if (rigidbody.velocity == new Vector3(0,0,0)) {
			return;
		}
		Vector3 dir = collision.transform.position - transform.position;

		StopDetectionScript.minionStopCount ++;

		if(dir.z > 0.5){
			transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z - 1);
		}
		else if(dir.z < -0.5){
			transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z + 1);
		}
		else if(dir.x < -0.5){
			transform.position = new Vector3(collision.transform.position.x + 1, transform.position.y, collision.transform.position.z);
		}
		else if(dir.x > 0.5){
			transform.position = new Vector3(collision.transform.position.x - 1, transform.position.y, collision.transform.position.z);
		}

		if (isPurple == true && collision.collider.tag == "Player") {
			Destroy (collision.collider.gameObject);
			StopDetectionScript.minionDead++;
			StopDetectionScript.minionStopCount--;
			TileMakeScript.maxMinions++;
			StopDetectionScript.score -= 50;
				}

		rigidbody.velocity *= 0;
		rigidbody.isKinematic = true;
	}

	// click on this to command this to move in another direction is stopped
	void OnMouseUp() {
        // can only click when not moving
        if(rigidbody.velocity != new Vector3(0, 0, 0)){
            return;
        }
        
		rigidbody.isKinematic = false;
		Debug.Log(" Clicking ...");

		if(dir_menu != null){
			if(dir_menu.GetComponent<DirectionMenu>().Same_Center(gameObject)){
				// if both false, true
				// if activeSelf true, false
				if(dir_menu.activeSelf){
					dir_menu.SetActive(false);
				}
				else {
					dir_menu.SetActive(true);
				}
			}
			else {
				dir_menu.SetActive(true);
			}
			dir_menu.GetComponent<DirectionMenu>().Change_Menu_Center(gameObject);
			dir_menu.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
			// if clicked on the same thing twice, menu should disappear
			return;
		}
		else {	
			// instantiate a menu, should be Singleton
			GameObject dir_obj =  (GameObject)Instantiate(direction_control);
			dir_menu = dir_obj;
			dir_menu.GetComponent<DirectionMenu>().Find_Buttons();
			dir_menu.GetComponent<DirectionMenu>().Change_Menu_Center(gameObject);
			dir_menu.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
			dir_menu.SetActive(true);
		}
	}

	void OnMouseDown() {
		v3Pos = Input.mousePosition;
	}
	void OnMouseDrag() {

		if(rigidbody.velocity != new Vector3(0, 0, 0)){
			return;
		}
		
		rigidbody.isKinematic = false;

		var v3 = Input.mousePosition - v3Pos;
		GlobalConst.Direction dir_enum;
		v3.Normalize();
		var f = Vector3.Dot(v3, Vector3.up);
		if (Vector3.Distance(v3Pos, Input.mousePosition) < threshold) {
			//Debug.Log("No movement");
			return;
		}
		
		if (f >= 0.5) {
			Debug.Log("Up");
			dir_enum = GlobalConst.Direction.Up;
		}
		else if (f <= -0.5) {
			Debug.Log("Down");
			dir_enum = GlobalConst.Direction.Down;
		}
		else {
			f = Vector3.Dot(v3, Vector3.right);
			if (f >= 0.5) {
				Debug.Log("Right");
				dir_enum = GlobalConst.Direction.Right;
			}
			else {
				Debug.Log("Left");
				dir_enum = GlobalConst.Direction.Left;
			}
		}
		if (dir_menu != null) {
			dir_menu.SetActive(false);
		}
		Move_Character (dir_enum);
	}



	void OnMouseEnter() {
		if (isPurple == false) {
			GetComponentInChildren<TurnPurpleScript>().renderer.material.mainTexture = yellowMinionSel;
		} else {
			GetComponentInChildren<TurnPurpleScript>().renderer.material.mainTexture = purpleMinionSel;
		}
	}
	
	void OnMouseExit() {
		if (isPurple == false) {
			GetComponentInChildren<TurnPurpleScript>().renderer.material.mainTexture = yellowMinion;
		}
		else {
			GetComponentInChildren<TurnPurpleScript>().renderer.material.mainTexture = purpleMinion;
		}
	}
}
