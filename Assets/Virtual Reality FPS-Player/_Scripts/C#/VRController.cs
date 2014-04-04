
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


using UnityEngine;
using System.Collections;

public class VRController : MonoBehaviour 
{		
	// WeaponClass
	public class WeaponClass
	{
		public Renderer WeaponRenderer;
		public Renderer Muzzle;
		public AudioClip ShootSound;
		public bool fireArm;
		public float fireRate;
		public float nextFireTime;
		public float gunRage;
		public float recoilUp;
		public int maxMagazine;
		public int maxBulletPerMagazine;
		public int magazineLeft;
		public int bulletLeft;
		public int power;
	}

	// Weapon
	public AudioClip ShootSound;
	public GameObject Particle_Metal;
	public GameObject Particle_Blood;
	public GameObject CrossHair;		
	public Renderer Muzzle;	
	public Transform BulletOrigin;
	private RaycastHit HitRay;	
	private int muzzleRotate = 45;	
	public float crossHairSize;	
	public float ragdollEffectCoef;
	private float breathAngle;	
	private bool reload;
	private bool shootAllowed = true;
	private WeaponClass CurrentWeapon;	
	private WeaponClass[] WeaponList = new WeaponClass[1];	
	
	// Rotation
	private float rotationY;
	private float rotationX;
	private float minimumY = -50f;	
	private float maximumY = 40f;
	private float startRotation;
	public float rotateCoef;
	public int movmentType = 1;
	private Vector2 ViewValues = Vector2.zero;
	private string inputRotX;
	private string inputRotY;
	private MouseLook Script_MouseLook;
	
	// From when the screen should follow the weapon		
	// This gives you a more realistic feeling!
	// Set these values to "0" if you don't like it
	float minViewX;
	float maxViewX;
	float minViewY;
	float maxViewY;
	
	// Player					// Connect these bones with the transform variables 
	public Transform Spine; 	// Spine 		-> lean spine while looking up/down
	public Transform UpperBody; // Spine1 		-> rotation of the upper body while looking left/right
	public Transform LeftArm; 	// LeftSoulder 	-> rotation of the left arm while looking up/down 
	public Transform RightArm;	// RightSoulder -> rotation of the right arm while looking up/down 
	public Transform Shoulders; // Spine2 		-> for the recoil effect
	public Transform Neck;		// Neck			-> gets the mouselook script
	public Transform Head; 		// Neck1		-> rotation of the head	
	public GameObject CameraStandard;	
	public GameObject Camera3D;
	public GameObject DeadCamera;
	public GameObject DeadCamera3D;
	private VRCharacterMotor Script_CharacterMotor;
	private CharacterController Character_Controller;
	private bool isDead = false;
	private int health;

		
	private void Start()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		// Settings for the pistol
		WeaponList[0] = new WeaponClass();
		WeaponList[0].Muzzle = Muzzle;
		WeaponList[0].ShootSound = ShootSound;
		WeaponList[0].fireArm = true;
		WeaponList[0].fireRate = 0.12f;
		WeaponList[0].gunRage = 1000;
		WeaponList[0].recoilUp = 0f;
		WeaponList[0].maxMagazine = 3;
		WeaponList[0].maxBulletPerMagazine = 10;			
		WeaponList[0].power = 100;
		CurrentWeapon = WeaponList[0];
			
		// Correction value for the startrotation
		startRotation += transform.localEulerAngles.y;
		
		// The player always starts with full ammo
		foreach(WeaponClass weapon in WeaponList)
		{			
			weapon.magazineLeft = weapon.maxMagazine;			
			weapon.bulletLeft =  weapon.maxBulletPerMagazine;			
		}	
		
		// Due to the ragdoll, we need access to some scripts and components, when the player respawn
		Script_CharacterMotor = (VRCharacterMotor)GetComponent("VRCharacterMotor");
		Character_Controller = (CharacterController)GetComponent("CharacterController");
		Script_MouseLook = (MouseLook)Neck.GetComponent("MouseLook");
		DeadCamera.SetActive(false);
		DeadCamera3D.SetActive(false);	
		Script_CharacterMotor.enabled = true;
		animation.enabled = true;
		Character_Controller.enabled = true;
		isDead = false;	
		
		switch (movmentType)
		{			
			case 1:	// FPS - Virtual reality mode for goggles like Oculus Rift or Durovis Dive		
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
	
	
	private void Update()
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
				ActivateRagdoll(new float[5] { VRCharacterMotor.movementVelocity.x * 100, // Velocity
											   VRCharacterMotor.movementVelocity.y * 100,
											   VRCharacterMotor.movementVelocity.z * 100,
											   1,		// Hit height
											   0 });	// Weapon power
			}
		}
	}
	
	private void RotatePlayer()
	{
		// Gets the gamepad input
		rotationX += (Input.GetAxis(inputRotX) * rotateCoef * Time.smoothDeltaTime);
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
	
	public void OthersPlayerControl()
	{		
		// Raycast for the shot
		// Because of the 3D Effekt, the Bulletorigin.position(X) is always in the middle of the two cameras
		Vector3 bulletOrigin = BulletOrigin.position;
		Vector3 gunDirection = BulletOrigin.TransformDirection(Vector3.forward);
		
		// Instantiates a new raycasthit in the direction we look
		HitRay = new RaycastHit();
		Physics.Raycast(bulletOrigin, gunDirection, out HitRay, CurrentWeapon.gunRage);
		
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
		float crossHairDistanze = Vector3.Distance(HitRay.point, transform.position);
		
		// It gets the same position as the HitRay point
		CrossHair.transform.position = new Vector3(HitRay.point.x, HitRay.point.y, HitRay.point.z);	
		
		// The scale should be always the same
		CrossHair.transform.localScale = new Vector3(crossHairDistanze, crossHairDistanze, crossHairDistanze) * crossHairSize;	
		
		// Gets the same rotation as the arms
		CrossHair.transform.localEulerAngles = new Vector3(UpperBody.transform.eulerAngles.x + 90, 0, 0);
		
		
		
		// Breath or wobble effect when walking or standing
		float angleSpeed;		
		float sinValue;
		
		// WalkSoundSource is handled in the characterMotor.cs
		// Depending on the walking speed
		if(!VRCharacterMotor.SoundSource_Walk)
			VRCharacterMotor.SoundSource_Walk = (AudioSource) GameObject.Find("WalkSound").GetComponent("AudioSource");
		if(!VRCharacterMotor.SoundSource_Walk.isPlaying)
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
        Vector3 armPosition = UpperBody.localPosition;
        breathAngle += angleSpeed;
		armPosition.y = armPosition.y + ((float)Mathf.Sin(breathAngle)*sinValue);
		UpperBody.localPosition = armPosition;			
	}
	
	public void ActivateRagdoll(float[] values)
	{	
		Vector3 ForceDirection = new Vector3(values[0], values[1], values[2]);
		float hitHeight = values[3];
			
		isDead = true;			
		CrossHair.transform.localScale = Vector3.zero;
		VRCharacterMotor.SoundSource_Walk.Stop();
		
		// Activate the DeadCamera3D
		DeadCamera3D.SetActive(true);
		Camera3D.SetActive(false);
		
		// Deactivate scripts and components
		Script_CharacterMotor.enabled = false;
		animation.enabled = false;
		Script_MouseLook.enabled = false;
		Character_Controller.enabled = false;
		
		// Enabled all components we need for the ragdoll
		foreach( CapsuleCollider cap in GetComponentsInChildren<CapsuleCollider>()) 
		{
			cap.enabled = true;
		}
		foreach( BoxCollider box in GetComponentsInChildren<BoxCollider>()) 
		{
			box.enabled = true;
		}
		foreach( SphereCollider sphere in GetComponentsInChildren<SphereCollider>()) 
		{
			sphere.enabled = true;
		}
		foreach( Rigidbody rigi in GetComponentsInChildren<Rigidbody>()) 
		{
			rigi.isKinematic = false;
			float heightDif = hitHeight - transform.position.y;

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
	
	private void Shot(Vector3 gunDirection)
	{				
		// Recoul up
		CurrentWeapon.recoilUp += 5;		
		
		// In this case, the player has an infinite amount of bullets and does not need to reload
		//CurrentWeapon.bulletLeft -=1;
		
		// If something was hit
		if (HitRay.collider)
		{
			// We send an message to that object with some informations regarding the shot
			HitRay.collider.SendMessage("Hit", new float[5]{gunDirection.x * CurrentWeapon.power * ragdollEffectCoef, 
														    gunDirection.y * CurrentWeapon.power * ragdollEffectCoef, 
														    gunDirection.z * CurrentWeapon.power * ragdollEffectCoef, 
														    HitRay.point.y, 
															CurrentWeapon.power}, 
														    SendMessageOptions.DontRequireReceiver);
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
	IEnumerator Reload()
	{
		reload = true;
		
		yield return new WaitForSeconds (2);
		CurrentWeapon.bulletLeft = CurrentWeapon.maxBulletPerMagazine;
		
		reload = false;
	}   
	
	// This will be called, when the player was hit
	public void Hit(float[] values)
	{
		// Health -= Weaponpower
		health -= (int)values[4];
		
		// Activate Ragdoll with the weapons power and the hit-height stored in values
		if(health <= 0)		
			ActivateRagdoll(values);		
	}
	
	// Change the type of control
	public void SetMovmentType(int type)
	{
		movmentType = type;
	}
	
	// Description of control and buttons
	private void OnGUI()
	{		
		GUIStyle styleLabel = new GUIStyle();
		styleLabel.fontSize = 12;
		float height = Screen.height*0.6f;
		
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
			Application.LoadLevel("Demo1");
		}
		else if(GUI.Button(new Rect(10,height + 180,200,40),"Realistic FPS"))
		{
			Application.LoadLevel("Demo2");
		}
		else if(GUI.Button(new Rect(10,height + 230,200,40),"Standard FPS"))
		{
			Application.LoadLevel("Demo3");
		}
	}
}