using UnityEngine;
using System.Collections;

public class FirstPersonScript : MonoBehaviour {

	public float move_speed = 1.0f;
	public float camera_speed = 3.0f;
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
		//Rotation
		float rotate_lr = Input.GetAxis ("Mouse X") * camera_speed;
		float rotate_ud = Input.GetAxis ("Mouse Y") * camera_speed;
		transform.Rotate (0, rotate_lr, 0);
		transform.Rotate (rotate_ud, 0, 0);

		
		//Movement
		float forwardspeed = Input.GetAxis ("Vertical") * move_speed;
		float sideSpeed = Input.GetAxis ("Horizontal") * move_speed;
		
		CharacterController cc = GetComponent<CharacterController>();
		Vector3 speed = new Vector3 (sideSpeed, 0, forwardspeed);
		speed = transform.rotation * speed;
		
		cc.SimpleMove (speed);

	
	}
}
