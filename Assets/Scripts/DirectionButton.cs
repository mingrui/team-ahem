using UnityEngine;
using System.Collections;

public class DirectionButton : MonoBehaviour {

	private GameObject center;
	private GlobalConst.Direction dir_enum;

	// Use this for initialization
	void Start () {
		center = transform.parent.GetComponent<DirectionMenu>().center;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp() {
		// check position relative to parent
		Vector3 dir = transform.position - center.transform.position;

		if(dir.z > 0.5){
			Debug.Log("up");
			dir_enum = GlobalConst.Direction.Up;
		}
		else if(dir.z < -0.5){
			Debug.Log("down");
			dir_enum = GlobalConst.Direction.Down;
		}
		else if(dir.x < -0.5){
			Debug.Log("left");
			dir_enum = GlobalConst.Direction.Left;
		}
		else if(dir.x > 0.5){
			Debug.Log("right");
			dir_enum = GlobalConst.Direction.Right;
		}

		Move_Center(dir_enum);
		transform.parent.gameObject.SetActive(false);
	}

	public GlobalConst.Direction Get_Direction(){
		return dir_enum;
	}

	void Move_Center(GlobalConst.Direction dir_enum){
		center.GetComponent<MovementControl>().Move_Character(dir_enum);
	}
}
