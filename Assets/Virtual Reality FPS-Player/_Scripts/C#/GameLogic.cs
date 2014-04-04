//Virtual Reality FPS - Character System v1.0
//by Fabian Koch

using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour 
{
	public GameObject playerPrefab;
	public GameObject player;	
	public GameObject clonePrefab;
	public GameObject clone;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Player respawn
		if(Input.GetButtonDown("Respawn"))
		{		
			PlayerRespawn();
		}
	}
	
	public void PlayerRespawn()
	{
		// Destroy the current players
		Destroy(player);
		Destroy(clone);
		
		// Instantiate new ones
		player = (GameObject)Instantiate(playerPrefab, player.transform.position, player.transform.rotation);	
		clone = (GameObject)Instantiate(clonePrefab, clone.transform.position, clone.transform.rotation);

		if(Application.loadedLevelName == "Demo1") 
		{
			player.SendMessage("SetMovmentType", 1,SendMessageOptions.DontRequireReceiver);
		}
		else if(Application.loadedLevelName == "Demo2") 
		{
			player.SendMessage("SetMovmentType", 2,SendMessageOptions.DontRequireReceiver);
		}
		else if(Application.loadedLevelName == "Demo3") 
		{
			player.SendMessage("SetMovmentType", 3,SendMessageOptions.DontRequireReceiver);	
		}
	}
}
