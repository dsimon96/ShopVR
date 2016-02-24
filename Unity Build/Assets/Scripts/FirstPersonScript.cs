using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class FirstPersonScript : MonoBehaviour {

	public float move_speed = 1.0f;
	public float camera_speed = 3.0f;
	public float controller_speed = 2.0f;

	//SerialPort Arduino = new SerialPort("COM10", 9600);

	// Use this for initialization

	// Update is called once per frame
	void Update () {
		/*
		if (Arduino.IsOpen) {
			try {
				int dir = Arduino.ReadByte ();
				if (dir == 2)
					transform.Rotate (0, controller_speed, 0, Space.World);
				else if (dir == 3)
					transform.Rotate (controller_speed, 0, 0);
			} catch (System.Exception) {
			}
		}*/
		//Rotation
		float rotate_lr = Input.GetAxis ("Mouse X") * camera_speed;
		float rotate_ud = Input.GetAxis ("Mouse Y") * camera_speed;	
		transform.Rotate (0, rotate_lr, 0, Space.World);
		transform.Rotate (rotate_ud, 0, 0);
		
		//Movement
		float forwardspeed = Input.GetAxis ("Vertical") * move_speed;
		float sideSpeed = Input.GetAxis ("Horizontal") * move_speed;
		
		CharacterController cc = GetComponent<CharacterController>();
		Vector3 speed = new Vector3 (-sideSpeed, 0, -forwardspeed);
		speed = transform.rotation * speed;
		
		cc.SimpleMove (speed);

	
	}
}
