//Virtual Reality FPS - Charactersystem v1.1
//by Fabian Koch
 
/*
	HOW TO IMPLEMENT THIS SYSTEM IN YOUR OWN CHACRACTER MODEL
 
-------------------------------------- http://www.youtube.com/watch?v=23QFX2khIN8 ------------------------------------------
 	
1.	Buy or create your own character with a bone system similar to the one in the demo.	
	(The easiest way is to buy a model from Mixamo from the unity store)

2.	Drag your model and the prefab into the scene and create a ragdoll for your player in T-position.
	(If this is your first time doing this watch my video)
	
3.	For safety go to all bones, set "Is kinematic = true" and "disable" all colliders like boxCollider, sphereCollider etc.
	(This prevents the ragdoll behaviour, when the game starts) 
	
4.	Before we come to the animation, make sure that only the legs of your model are animated!
	Open your animation model with blender or 3ds Max and delete all the animations keys above the hip. (T-Position)
	If you don't have animations yet, create them anyway as an example, so that you don't get errors later.
	Have you done so, choose your model.fbx in the project view, go to Animations and create the animations "Idle, Walk and Jump".
	Then go to Rig, set "Animation type = Legacy" and "Generation = Store in Root(new)")
	
5.	Click on the model in the scene and add the three animations to the animation component.
	(You can always watch at the settings of the prefab, you will need him now anyway)
	
6.	Open the hip of your model and put the "Neck/Neck1/..." into the Spine
	(We have more control options this way)
	
7.	Now we have to do some drag and drop(gameobjects) from the prefab to your model.
	Hence make sure that your model has the same position than the prefab.
	- Move(not copy) the DeadCamera3D, FPS Camera and Pistol from the prefab to the same location of your model.
	
8. 	Add these components to your model:
	- VRController
	- VRInputController(includes Character Motor)
	- Audio Listener
	- Audio Source	
	->Afterwards copy all component values from the prefab to your model
	(right click on the component of the prefab and choose "Copy Component", 
	then right click on the component of your player and "add Component as values")

9.	Add the MouseLook(script) to the Neck.
 	Start the Game and you will see, that apart from the arm position your player is ready to go;)
	
10. In case your player makes any strange movements, here is a list of possible reasons:
	- Incorrect axes from Model (Z-Axis -> is the view direction of the model and bones)
	- You forgot to delete some animation keys. Use "STRG-A" to get them all, then hold "alt" and remove legs from selection.
	- Your model has not the same bone structure. 
	- Change the arm rotation only below the shoulders(look at the template)
*/
#pragma strict

// WeaponClass
class WeaponClass
{
	var WeaponRenderer : Renderer;
	var Muzzle : Renderer;
	var ShootSound : AudioClip;
	var fireArm;
	var fireRate : float;
	var nextFireTime : float;
	var gunRage : float;
	var recoilUp : float;
	var maxMagazine : int;
	var maxBulletPerMagazine : int;
	var magazineLeft : int;
	var bulletLeft : int;
	var power : int;
}

// Weapon
var ShootSound : AudioClip;
var Particle_Metal : GameObject;
var Particle_Blood : GameObject;
var CrossHair : GameObject;		
var Muzzle : Renderer;	
var BulletOrigin : Transform;	
var crossHairSize : float;	
var ragdollEffectCoef : float;
private var HitRay : RaycastHit;	
private var muzzleRotate : int = 45;	
private var breathAngle : float;	
private var reload;
private var shootAllowed = true;
private var CurrentWeapon : WeaponClass;	
	
// Rotation	
var rotateCoef : float;
var movmentType : int = 1;
private var rotationY : float;
private var rotationX : float;
private var minimumY : float = -50f;	
private var maximumY : float = 40f;
private var startRotation : float;
private var ViewValues : Vector2 = new Vector2(0,0);
private var inputRotX;
private var inputRotY;	
private var Script_MouseLook : MouseLookJS;

// From when the screen should follow the weapon		
// This gives you a more realistic feeling!
// Set these values to "0" if you don't like it
private var minViewX : float;
private var maxViewX : float;
private var minViewY : float;
private var maxViewY : float;

// Player					// Connect these bones with the transform variables 
var Spine : Transform; 		// Spine 		-> lean spine while looking up/down
var UpperBody : Transform;  // Spine1 		-> rotation of the upper body while looking left/right
var LeftArm : Transform; 	// LeftSoulder 	-> rotation of the left arm while looking up/down 
var RightArm : Transform;	// RightSoulder -> rotation of the right arm while looking up/down 
var Shoulders : Transform;  // Spine2 		-> for the recoil effect
var Neck : Transform;		// Neck			-> gets the mouselook/sensor script 
var Head : Transform; 		// Neck1		-> rotation of the head	
var CameraStandard : GameObject;	
var Camera3D : GameObject;
var DeadCamera : GameObject;
var DeadCamera3D : GameObject;
private var health : int;
private var isDead = false;
private var Script_CharacterMotor : VRCharacterMotorJS;
private var Character_Controller : CharacterController;
	
function Start () 
{
	Screen.sleepTimeout = SleepTimeout.NeverSleep;
	
	// Settings for the pistol
	CurrentWeapon = new WeaponClass();
	CurrentWeapon.Muzzle = Muzzle;
	CurrentWeapon.ShootSound = ShootSound;
	CurrentWeapon.fireArm = true;
	CurrentWeapon.fireRate = 0.12f;
	CurrentWeapon.gunRage = 1000;
	CurrentWeapon.recoilUp = 0f;
	CurrentWeapon.maxMagazine = 3;
	CurrentWeapon.maxBulletPerMagazine = 10;			
	CurrentWeapon.power = 100;
	CurrentWeapon = CurrentWeapon;	
			
	// Correction value for the startrotation
	startRotation += transform.localEulerAngles.y;	
	
	// The player always starts with full ammo				
	CurrentWeapon.magazineLeft = CurrentWeapon.maxMagazine;			
	CurrentWeapon.bulletLeft =  CurrentWeapon.maxBulletPerMagazine;			
	
	// Due to the ragdoll, we need access to some scripts and components, when the player respawn
	Script_CharacterMotor = GetComponent("VRCharacterMotorJS");
	Character_Controller = GetComponent("CharacterController");
	Script_MouseLook = Neck.GetComponent("MouseLookJS");
	DeadCamera.SetActive(false);
	DeadCamera3D.SetActive(false);	
	
	Script_CharacterMotor.enabled = true;
	animation.enabled = true;
	Character_Controller.enabled = true;
	isDead = false;	
	
	switch (movmentType)
	{			
		case 1:	// FPS - Virtual reality mode for googles like Oculus Rift or Durovis Dive		
				Camera3D.SetActive(true);
				CameraStandard.SetActive(false);
				Script_MouseLook.enabled = true;	
				inputRotX = "Rot X";
				inputRotY = "Rot Y";
				minViewX = -10f;
				maxViewX = 10f;
				minViewY = -10f;
				maxViewY = 10f;					
				break;
		case 2:	// FPS - Realistic(Still a problem when you rotate to fast)					
				Camera3D.SetActive(false);
				CameraStandard.SetActive(true);
				Script_MouseLook.enabled = false;
				inputRotX = "Mouse X";
				inputRotY = "Mouse Y";	
				minViewX = -20f;
				maxViewX = 20f;
				minViewY = -10f;
				maxViewY = 10f;					
				break;
		case 3:	// FPS - Standard
				Camera3D.SetActive(false);
				CameraStandard.SetActive(true);
				Script_MouseLook.enabled = false;
				inputRotX = "Mouse X";
				inputRotY = "Mouse Y";	
				minViewX = 0f;
				maxViewX = 0f;
				minViewY = 0f;
				maxViewY = 0f;
				break;
	}		
}

function Update () 
{
	if(!isDead)
	{
		// Update rotation
		RotatePlayer();		
			
		// shot, crossHair, breath
		OthersPlayerControl(); 
			
		// We can activate the ragdoll manually, by pressing the B button		
		if(Input.GetButtonDown("Ragdoll"))	
		{
			var values = [  VRCharacterMotorJS.movementVelocity.x * 100, // Velocity
							VRCharacterMotorJS.movementVelocity.y * 100,
							VRCharacterMotorJS.movementVelocity.z * 100,
							1,		// Hit height
							0 ];	// Weapon power
							
			ActivateRagdoll(values);	
		}		
	}
}

function RotatePlayer()
{
	// Gets the gamepad input
	rotationX +=  Input.GetAxis(inputRotX) * rotateCoef * Time.smoothDeltaTime;
	rotationY += -Input.GetAxis(inputRotY) * rotateCoef * Time.smoothDeltaTime;
	rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);		
		
	// Rotates the arms up and down
	RightArm.transform.localEulerAngles = new Vector3(rotationY, RightArm.transform.localEulerAngles.y, RightArm.transform.localEulerAngles.z);
	LeftArm.transform.localEulerAngles = new Vector3(rotationY, LeftArm.transform.localEulerAngles.y, LeftArm.transform.localEulerAngles.z);	
		
	// Specifies the rotation difference between upper body and camera
	ViewValues += new Vector2(Input.GetAxis(inputRotX) * rotateCoef * Time.smoothDeltaTime,
								  Input.GetAxis(inputRotY) * rotateCoef * Time.smoothDeltaTime);
	
	// Prevents that the player can shoot all the way up or down
	ViewValues.x = Mathf.Clamp(ViewValues.x, minViewX, maxViewX);
	ViewValues.y = Mathf.Clamp(ViewValues.y, minViewY, maxViewY);
		
	// The entire body follows, when the arms override the limits(X-Axis)
	if((ViewValues.x >= maxViewX || ViewValues.x <= minViewX))	
	{		
		transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, 
												   transform.localEulerAngles.y + Input.GetAxis(inputRotX) * rotateCoef * Time.smoothDeltaTime, 0), 1f);
	}
	else
	{
		// Rotate the upper body left and right
		UpperBody.transform.eulerAngles = new Vector3(UpperBody.transform.eulerAngles.x, rotationX + startRotation, 0);
	}
		
	// The head/Camera and spine follows, when the arms override the limits(Y-Axis)
	if(rotationY < maximumY && rotationY > minimumY)
	{
		if((ViewValues.y >= maxViewY || ViewValues.y <= minViewY))
		{		
			if(movmentType > 1)
			{
				// Rotates the Head				
		     	Head.transform.localEulerAngles = Vector3.Slerp(Head.transform.localEulerAngles, 
																new Vector3(Head.transform.localEulerAngles.x  - Input.GetAxis(inputRotY)* 1.3f * rotateCoef * Time.smoothDeltaTime, 
																Head.transform.localEulerAngles.y, Head.transform.localEulerAngles.z), 1f);
			}
				
			// Bend the Spine
			Spine.transform.localEulerAngles = Vector3.Slerp(Spine.transform.localEulerAngles, 
															 new Vector3(Spine.transform.localEulerAngles.x  - Input.GetAxis(inputRotY) * 0.5f * rotateCoef * Time.smoothDeltaTime, 
															 Spine.transform.localEulerAngles.y, Spine.transform.localEulerAngles.z), 1f);									
		}	
	}
		
	// Shoulders gets the recoil impact
	Shoulders.transform.localEulerAngles = new Vector3(-CurrentWeapon.recoilUp, 0,0);
}

function OthersPlayerControl()
{		
	// Raycast for the shot
	// Because of the 3D Effekt, the Bulletorigin.position(X) is always in the middle of the two cameras
	var bulletOrigin : Vector3 = BulletOrigin.position;
	var gunDirection : Vector3 = BulletOrigin.TransformDirection(Vector3.forward);
		
	// Instantiates a new raycasthit in the direction we look
	HitRay = new RaycastHit();
	Physics.Raycast(bulletOrigin, gunDirection, HitRay, CurrentWeapon.gunRage);
	
	
	// Shot, when you have bullet left
	if(CurrentWeapon.bulletLeft > 0 && shootAllowed && (Input.GetAxis("Shoot") < -0.5f || Input.GetButtonDown("Shoot")))				
		Shot(gunDirection);		
	else	
		CurrentWeapon.Muzzle.enabled = false;
		
	// He can shoot again, when the Player has released the button
	if(Input.GetAxis("Shoot") > -0.5f)
		shootAllowed = true;
		
	// The recoil always cames back to zero
	if(CurrentWeapon.recoilUp > 0)
		CurrentWeapon.recoilUp -= 1f;		
		
	// Reload, when you don't have any bullet in current clip
	if (CurrentWeapon.bulletLeft == 0 && CurrentWeapon.magazineLeft > 0 && CurrentWeapon.fireArm && !reload )		
		StartCoroutine(Reload());		
		
		
		
	// Crosshair
	// Calculates the distance between hitPoint and player
	var crossHairDistanze : float = Vector3.Distance(HitRay.point, transform.position);
		
	// It gets the same position as the HitRay point
	CrossHair.transform.position = new Vector3(HitRay.point.x, HitRay.point.y, HitRay.point.z);	
		
	// The scale should be always the same
	CrossHair.transform.localScale = new Vector3(crossHairDistanze, crossHairDistanze, crossHairDistanze) * crossHairSize;	
		
	// Gets the same rotation as the arms
	CrossHair.transform.localEulerAngles = new Vector3(UpperBody.transform.eulerAngles.x + 90, 0, 0);
		
		
		
	// Breath or wobble effect when walking or standing
	var angleSpeed : float;		
	var sinValue : float;
		
	// WalkSoundSource is handled in the characterMotor.cs
	// Depending on the walking speed
	if(!VRCharacterMotorJS.SoundSource_Walk)
		VRCharacterMotorJS.SoundSource_Walk = GameObject.Find("WalkSound").GetComponent("AudioSource");
	if(!VRCharacterMotorJS.SoundSource_Walk.isPlaying)
	{
		angleSpeed = 0.04f;
		sinValue = 0.0005f;
	}
	else
	{
		angleSpeed = Input.GetAxis("Move Y") * 0.13f;
		sinValue = Input.GetAxis("Move Y") * 0.002f;
	}
	// The wobble is transferred to the arms
    var armPosition : Vector3 = UpperBody.localPosition;
    breathAngle += angleSpeed;
	armPosition.y = armPosition.y + (Mathf.Sin(breathAngle)*sinValue);
	UpperBody.localPosition = armPosition;			
}
	
function ActivateRagdoll(values : float[])
{	
	var ForceDirection : Vector3 = new Vector3(values[0], values[1], values[2]);
	var hitHeight : float = values[3];
		
	isDead = true;			
	CrossHair.transform.localScale = Vector3.zero;
	VRCharacterMotorJS.SoundSource_Walk.Stop();
		
	// Activate the DeadCamera3D
	DeadCamera3D.SetActive(true);
	Camera3D.SetActive(false);
		
	// Deactivate scripts and components
	Script_CharacterMotor.enabled = false;
	animation.enabled = false;
	Script_MouseLook.enabled = false;
	Character_Controller.enabled = false;
		
	// Enabled all components we need for the ragdoll	
	for(var cap : CapsuleCollider in GetComponentsInChildren(CapsuleCollider)) 
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
		
		// The ragdoll was activated manually when the hitheight is 1
		if(hitHeight == 1)
			heightDif = 1;			
			
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
	
	// DeadCamera3D gets the same Y-position as the spine
	if(movmentType == 1)
	{			
		DeadCamera3D.SetActive(true);
		Camera3D.SetActive(false);
	}
	else
	{
		DeadCamera.SetActive(true);
		CameraStandard.SetActive(false);
	}
}
	
function Shot(gunDirection : Vector3)
{				
	// Recoul up
	CurrentWeapon.recoilUp += 5;		
		
	// In this case, the player has an infinite amount of bullets and does not need to reload
	//CurrentWeapon.bulletLeft -=1;
		
	// If something was hit
	if (HitRay.collider)
	{
		var values = [ 	gunDirection.x * CurrentWeapon.power * ragdollEffectCoef, 
				   		gunDirection.y * CurrentWeapon.power * ragdollEffectCoef, 
				   		gunDirection.z * CurrentWeapon.power * ragdollEffectCoef, 
				   		HitRay.point.y, 
				   		CurrentWeapon.power ];
				   
		// We send an message to that object with some informations regarding the shot
		HitRay.collider.SendMessage("Hit", values, SendMessageOptions.DontRequireReceiver);
		// Instantiate particles
		if (HitRay.collider.tag == "Player")									
			Instantiate (Particle_Blood, HitRay.point, BulletOrigin.rotation);				
		else
			Instantiate (Particle_Metal, HitRay.point, BulletOrigin.rotation);
			
		if(HitRay.collider.rigidbody)
		{
			// After the player is dead, the body should just twitch a bit, when he was hit
			// Just change it, if you like it more realistic. I just took any value.
			HitRay.collider.rigidbody.AddForce(BulletOrigin.localEulerAngles * 2);
		}
	}
		
	// Show muzzle
	if(CurrentWeapon.Muzzle)
		CurrentWeapon.Muzzle.enabled = true;
		
	// Play shotsound
	audio.PlayOneShot(CurrentWeapon.ShootSound);
		
	//Transform muzzle
	muzzleRotate += 90;
	CurrentWeapon.Muzzle.transform.localRotation = Quaternion.AngleAxis(muzzleRotate, Vector3.forward);	
	shootAllowed = false;	
}
	
// Reload
function Reload()
{
	reload = true;
	
	yield WaitForSeconds (2);
	CurrentWeapon.bulletLeft = CurrentWeapon.maxBulletPerMagazine;
		
	reload = false;
}   
	
// This will be called, when the player was hit
function Hit(values : float[])
{
	// Health -= Weaponpower
	health -= values[4];
		
	// Activate Ragdoll with the weapons power and the hit-height stored in values
	if(health <= 0)		
		ActivateRagdoll(values);		
}
	
// Change the type of control
function SetMovmentType(type : int)
{
	movmentType = type;
}
	
// Description of control and buttons
function OnGUI()
{		
	var styleLabel : GUIStyle = new GUIStyle();
	var height : float = Screen.height*0.6f;
	styleLabel.fontSize = 12;	
	
	switch (movmentType)
	{			
		case 1:	// FPS - Virtual reality mode for googles like oculus rift or durovis dive
				GUI.Label(new Rect(10,height      ,300,30), "Move/Rotation : Xbox360 Controller", styleLabel);
				GUI.Label(new Rect(10,height +  20,300,30), "Gyro-Emulation : Mouse", styleLabel);
				GUI.Label(new Rect(10,height +  40,300,30), "Activate Ragdoll : B", styleLabel);
				GUI.Label(new Rect(10,height +  60,300,30), "Respawn : X", styleLabel);
				GUI.Label(new Rect(10,height + 80,300,30), "Shoot : RT", styleLabel);
				GUI.Label(new Rect(10,height + 100,300,30), "Jump : A", styleLabel);				
				break;
		case 2:	// FPS - Realistic(Still a problem when you rotate fast)
				GUI.Label(new Rect(10,height      ,300,30), "Move: W,A,S,D", styleLabel);
				GUI.Label(new Rect(10,height +  20,300,30), "Rotate : Mouse", styleLabel);
				GUI.Label(new Rect(10,height +  40,300,30), "Activate Ragdoll : E", styleLabel);
				GUI.Label(new Rect(10,height +  60,300,30), "Respawn : Q", styleLabel);
				GUI.Label(new Rect(10,height +  80,300,30), "Shoot : Click", styleLabel);
				GUI.Label(new Rect(10,height + 100,300,30), "Jump : Space", styleLabel);				
				break;
		case 3:	// FPS - Standard
				GUI.Label(new Rect(10,height      ,300,30), "Move: W,A,S,D", styleLabel);
				GUI.Label(new Rect(10,height +  20,300,30), "Rotate : Mouse", styleLabel);
				GUI.Label(new Rect(10,height +  40,300,30), "Activate Ragdoll : E", styleLabel);
				GUI.Label(new Rect(10,height +  60,300,30), "Respawn : Q", styleLabel);
				GUI.Label(new Rect(10,height +  80,300,30), "Shoot : Click", styleLabel);
				GUI.Label(new Rect(10,height + 100,300,30), "Jump : Space", styleLabel);
				break;
	}		
		
	// Load the scene
	if(GUI.Button(new Rect(10,height + 130,200,40),"Virtual Reality - FPS"))
	{
		Application.LoadLevel("Demo1JS");
	}
	else if(GUI.Button(new Rect(10,height + 180,200,40),"Realistic FPS"))
	{
		Application.LoadLevel("Demo2JS");
	}
	else if(GUI.Button(new Rect(10,height + 230,200,40),"Standard FPS"))
	{
		Application.LoadLevel("Demo3JS");
	}			
}
	