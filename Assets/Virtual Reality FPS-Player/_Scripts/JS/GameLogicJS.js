#pragma strict

var playerPrefab : GameObject;
var player : GameObject;
	
var clonePrefab : GameObject;
var clone : GameObject;


function Start () 
{
}

function Update () 
{
	// Player respawn
	if(Input.GetButtonDown("Respawn"))
	{		
		PlayerRespawn();
	}
}

function PlayerRespawn()
{
	// Destroy the current players
	Destroy(player);
	Destroy(clone);
	
	// Instantiate new ones
	player = Instantiate(playerPrefab, player.transform.position, player.transform.rotation);	
	clone = Instantiate(clonePrefab, clone.transform.position, clone.transform.rotation);
	
	if(Application.loadedLevelName == "Demo1JS") 
	{
		player.SendMessage("SetMovmentType", 1,SendMessageOptions.DontRequireReceiver);
	}
	else if(Application.loadedLevelName == "Demo2JS") 
	{
		player.SendMessage("SetMovmentType", 2,SendMessageOptions.DontRequireReceiver);
	}
	else if(Application.loadedLevelName == "Demo3JS") 
	{
		player.SendMessage("SetMovmentType", 3,SendMessageOptions.DontRequireReceiver);	
	}
}