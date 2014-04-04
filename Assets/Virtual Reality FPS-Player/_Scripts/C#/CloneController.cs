//Virtual Reality FPS - Character System v1.0
//by Fabian Koch
 
using UnityEngine;
using System.Collections;

public class CloneController : MonoBehaviour 
{		
	// Player
	private int health;
	private int maxHealth = 100;		
	private bool isDead = false;
	private CharacterController controller;	
	private float startRotation;
	
		
	private void Start()
	{
		startRotation += transform.localEulerAngles.y;		
		controller = (CharacterController)GetComponent("CharacterController");
		controller.enabled = true;
		animation.enabled = true;
		isDead = false;		
		health = maxHealth;
	}
	
	private void Update()
	{	

	}	
	
	public void ActivateRagdoll(float[] values)
	{	
		Vector3 ForceDirection = new Vector3(values[0], values[1], values[2]);
		float hitHeight = values[3];

		isDead = true;
		
		// Deactivate Scripts and components
		controller.enabled = false;
		animation.enabled = false;
		
		
		foreach( CapsuleCollider cap in GetComponentsInChildren<CapsuleCollider>() ) 
		{
			cap.enabled = true;
		}
		foreach( BoxCollider box in GetComponentsInChildren<BoxCollider>() ) 
		{
			box.enabled = true;
		}
		foreach( SphereCollider sphere in GetComponentsInChildren<SphereCollider>() ) 
		{
			sphere.enabled = true;
		}
		foreach( Rigidbody rigi in GetComponentsInChildren<Rigidbody>() ) 
		{			
			rigi.isKinematic = false;	
			float heightDif = hitHeight - transform.position.y;
			
			if(heightDif > 1.35f)
			{	
				if(rigi.name != "LeftLeg" && rigi.name != "RightLeg" && rigi.name != "LeftUpLeg" && rigi.name != "RightUpLeg" && 
				   rigi.name != "Hips")
				{
					rigi.AddForce(ForceDirection * 0.5f);
				}					
			}	
			else if(heightDif > 0.7f)
			{					
				if(rigi.name != "LeftLeg" && rigi.name != "RightLeg" && 
				   rigi.name != "LeftArm" && rigi.name != "RightArm" && rigi.name != "LeftForeArm" && rigi.name != "RightForeArm")
				{
					rigi.AddForce(ForceDirection);
				}
			}	
			else if(heightDif > 0)
			{					
				if(rigi.name != "Spine" && rigi.name != "Hips" && rigi.name != "Head" &&
					rigi.name != "LeftArm" && rigi.name != "RightArm" && rigi.name != "LeftForeArm" && rigi.name != "RightForeArm")
				{
					rigi.AddForce(ForceDirection);
				}
			}	
		}		
	}
	
	public void Hit(float[] values)
	{
		// Health -= Weaponpower
		health -= (int)values[4];
		
		// Activate Ragdoll with the weapons power and the hit-height stored in values
		if(health <= 0)		
			ActivateRagdoll(values);			
	}
}

