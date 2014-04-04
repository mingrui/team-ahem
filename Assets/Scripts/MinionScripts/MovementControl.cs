using UnityEngine;
using System.Collections;

public class MovementControl : MonoBehaviour {

	public int speed;
	public GameObject direction_control;
	//private GlobalConst.Direction minionDir;
	private GameObject dir_menu;
	public bool inWater = false;

	/*public void resetSpeed(){
	    rigidbody2D.velocity = rigidbody2D.velocity.normalized * speed;
	}*/

	public void changeSpeed(float scale){
		rigidbody.velocity *= scale;
	}

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


		if (inWater == true) {
			rigidbody.velocity *= 0.5f;
				}
	}

	// start moving right
	void Start () {

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
		Vector3 dir = collision.transform.position - transform.position;
		
		if(dir.z > 0.5){
			transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z - 1);
		}
		else if(dir.z < -0.5){
			Debug.Log("down");
			transform.position = new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z + 1);
		}
		else if(dir.x < -0.5){
			Debug.Log("left");
			transform.position = new Vector3(collision.transform.position.x + 1, transform.position.y, collision.transform.position.z);
		}
		else if(dir.x > 0.5){
			Debug.Log("right");
			transform.position = new Vector3(collision.transform.position.x - 1, transform.position.y, collision.transform.position.z);
		}


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


	void OnMouseEnter() {
		
		CursorScript.pos = new Vector3(gameObject.transform.position.x, 6f, gameObject.transform.position.z);
	}
}
