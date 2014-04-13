using UnityEngine;
using System.Collections;

public class CameramanMovement : MonoBehaviour {

    public Vector3 movement;
    public GameObject leftBounds;
    public GameObject rightBounds;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            movement.z = transform.position.z + 0.3f;
            transform.position = movement;

        }

        if (Input.GetKey(KeyCode.S))
        {
            movement.z = transform.position.z - 0.3f;
            transform.position = movement;

        }

        if (Input.GetKey(KeyCode.D))
        {
            movement.x = transform.position.x + 0.3f;
            transform.position = movement;

        }

        if (Input.GetKey(KeyCode.A))
        {
            movement.x = transform.position.x - 0.3f;
            transform.position = movement;

        }

		if (Input.GetKey(KeyCode.Z))
		{
			movement.y = transform.position.y + 0.3f;
			transform.position = movement;
			
		}

		if (Input.GetKey(KeyCode.X))
		{
			movement.y = transform.position.y - 0.3f;
			transform.position = movement;
			
		}
    }
}
