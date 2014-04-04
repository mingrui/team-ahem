#pragma strict

@script AddComponentMenu ("Camera-Control/Mouse Look")

enum RotationAxes  
{ 
	MouseXAndY = 0, 
	MouseX = 1, 
	MouseY = 2 
}
var axes : RotationAxes = RotationAxes.MouseXAndY;

var sensitivityX : float = 15F;
var sensitivityY : float = 15F;

var minimumX : float = -360F;
var maximumX : float = 360F;

var minimumY : float = -100F;
var maximumY : float = 100F;

var rotationY : float = 0F;
var rotationX : float = 0F;

var mouse_on=true;

	
function Disable()
{
	enabled = false;	
}	
	
function Start () 
{
	if (Application.platform == RuntimePlatform.Android)
      	mouse_on=false;
    else if(Application.platform == RuntimePlatform.IPhonePlayer)
    	mouse_on=false;
    	
	// Make the rigid body not change rotation
	if (rigidbody)
		rigidbody.freezeRotation = true;
}

function Update ()
{
	if (mouse_on)
	{
		rotationX += Input.GetAxis("Mouse X") * sensitivityX;			
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);	
			
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);		
	}	
}