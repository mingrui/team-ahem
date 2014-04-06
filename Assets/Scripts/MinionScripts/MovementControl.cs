using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {

	public Texture purpleMinion;
	public Texture purpleMinionSel;

	public Texture yellowMinion;
	public Texture yellowMinionSel;

	public bool isPurple = false;




	public int speed;
	public GameObject direction_control;
	//private GlobalConst.Direction minionDir;
	private GameObject dir_menu;
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
		rigidbody.velocity *= 0;
		rigidbody.isKinematic = true;
	}

	// click on this to command this to move in another direction is stopped
	void OnMouseDown() {
        //Debug.Log("clicked");
        // can only click when not moving
        if(rigidbody.velocity != new Vector3(0, 0, 0)){
            //Debug.Log("moving!");
            return;
        }
        
		rigidbody.isKinematic = false;

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
