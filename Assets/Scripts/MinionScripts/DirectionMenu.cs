using UnityEngine;
using System.Collections;

public class DirectionMenu : MonoBehaviour {

	public GameObject center;
	private Component[] buttons;

	void Start(){
		buttons = GetComponentsInChildren<DirectionButton>();
		//Debug.Log(buttons.Length);
	}

	public bool Same_Center(GameObject _center){
		return center == _center;
	}

	public void Find_Buttons(){
		buttons = GetComponentsInChildren<DirectionButton>();
		//Debug.Log(buttons.Length);
	}

	public void Change_Menu_Center(GameObject _center){
		center = _center;
		foreach(DirectionButton button in buttons){
			button.Change_Center(_center);
		}
	}
}
