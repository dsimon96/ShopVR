using UnityEngine;
using System.Collections;

public class BasketScript : MonoBehaviour {
	public float move_speed = 1.0f;

	// Use this for initialization

	// Update is called once per frame
	public class ExampleClass : MonoBehaviour {
		public Vector3 teleportPoint;
		public Rigidbody rb;
		void Start() {
			rb = GetComponent<Rigidbody>();
		}
		void FixedUpdate() {
			rb.MovePosition(transform.position + transform.forward * Time.deltaTime);
		}
	}
}