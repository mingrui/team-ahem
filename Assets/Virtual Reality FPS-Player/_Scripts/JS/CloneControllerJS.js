#pragma strict

// Player
private var health : int;
private var maxHealth : int = 100;		
private var isDead = false;
private var controller : CharacterController;	
private var startRotation : float;

function Start () 
{
	startRotation += transform.localEulerAngles.y;		
	controller = GetComponent("CharacterController");
	controller.enabled = true;
	animation.enabled = true;
	isDead = false;		
	health = maxHealth;
}
	
function ActivateRagdoll(values : float[])
{	
	var ForceDirection : Vector3 = new Vector3(values[0], values[1], values[2]);
	var hitHeight : float = values[3];
		
	isDead = true;
	
	// Deactivate Scripts and components
	controller.enabled = false;
	animation.enabled = false;
		
		
	for( var cap : CapsuleCollider in GetComponentsInChildren(CapsuleCollider)) 
	{
		cap.enabled = true;
	}
	for( var box : BoxCollider in GetComponentsInChildren(BoxCollider)) 
	{
		box.enabled = true; 
	}
	for( var sphere : SphereCollider in GetComponentsInChildren(SphereCollider)) 
	{
		sphere.enabled = true;
	}
	for( var rigi : Rigidbody in GetComponentsInChildren(Rigidbody)) 
	{
		rigi.isKinematic = false;
		var heightDif : float = hitHeight - transform.position.y;			
		
		// To ensure a realistic behavior of the player, we calculate on which altitude the player was hit.
		// There are three different types of behaviors.
		// Hit on the head
		if(heightDif > 1.35f)
		{	
			if(rigi.name != "LeftLeg" && rigi.name != "RightLeg" && rigi.name != "LeftUpLeg" && rigi.name != "RightUpLeg" && 
				  rigi.name != "Spines")
			{
				rigi.AddForce(ForceDirection * 0.5f);
			}					
		}
		// Hit on the upper body
		else if(heightDif > 0.7f)
		{					
			if(rigi.name != "LeftLeg" && rigi.name != "RightLeg" && 
			   rigi.name != "LeftArm" && rigi.name != "RightArm" && rigi.name != "LeftForeArm" && rigi.name != "RightForeArm")
			{
				rigi.AddForce(ForceDirection);
			}
		}	
		// Hit on the legs
		else if(heightDif > 0)
		{					
			if(rigi.name != "Spine" && rigi.name != "Spines" && rigi.name != "Head" &&
				rigi.name != "LeftArm" && rigi.name != "RightArm" && rigi.name != "LeftForeArm" && rigi.name != "RightForeArm")
			{
				rigi.AddForce(ForceDirection);
			}
		}			
	}	
}
				
	
	
function Hit(values : float[])
{
	// Health -= Weaponpower
	health -= values[4];
		
	// Activate Ragdoll with the weapons power and the hit-height stored in values
	if(health <= 0)		
		ActivateRagdoll(values);			
}


