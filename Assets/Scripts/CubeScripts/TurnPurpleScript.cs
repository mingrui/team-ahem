using UnityEngine;
using System.Collections;

public class TurnPurpleScript : MonoBehaviour {

	public Texture purpleMinion;

	public void turnPurple(){
				renderer.material.mainTexture = purpleMinion;
		}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
