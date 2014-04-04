﻿#pragma strict

// Require a character controller to be attached to the same game object
@script RequireComponent(VRCharacterMotorJS)
@script AddComponentMenu("Character/VR Input Controller JS")


private var motor : VRCharacterMotorJS;

// Use this for initialization
function Awake()
{
	motor = GetComponent(VRCharacterMotorJS);
}

// Update is called once per frame
function Update()
{
	// Get the input vector from kayboard or analog stick
	var directionVector : Vector3 = new Vector3(Input.GetAxis("Move X"), 0, Input.GetAxis("Move Y"));

	if (directionVector != Vector3.zero)
	{
		// Get the length of the directon vector and then normalize it
        // Dividing by the length is cheaper than normalizing when we already have the length anyway
		var directionLength : float = directionVector.magnitude;
		directionVector = directionVector / directionLength;

		// Make sure the length is no bigger than 1
		directionLength = Mathf.Min(1.0f, directionLength);
			
		// Make the input vector more sensitive towards the extremes and less sensitive in the middle
		// This makes it easier to control slow speeds when using analog sticks
		directionLength = directionLength * directionLength;

		// Multiply the normalized direction vector by the modified length
		directionVector = directionVector * directionLength;
	}

        // Apply the direction to the CharacterMotor
        motor.inputMoveDirection = transform.rotation * directionVector;
        motor.inputJump = Input.GetButton("Jump");   
}