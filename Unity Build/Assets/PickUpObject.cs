using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class PickUpObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying;
	GameObject carriedObject;
	public float distance;
	public float smooth;

	SerialPort Arduino = new SerialPort("/dev/cu.usbmodem411",9600);

	// Use this for initialization
	void Start () {
		Arduino.Open();
		mainCamera = GameObject.FindWithTag ("MainCamera");
	}
	
	// Update is called once per frame
	void Update() {
		if(carrying) {
			carry(carriedObject);
			checkDrop();
		} else {
			pickup();
		}


		int readValue = 0;
		//if(Arduino.isOpen){
		try{
			readValue = Arduino.ReadByte();

			switch(readValue){
			case 2:{
					Debug.Log("Case 2");
					dropObject ();
					break;
				}
			case 3:{
					Debug.Log("Case 3");
					int x = Screen.width / 2;
					int y = Screen.height / 2;

					Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));
					RaycastHit hit;
					if (Physics.Raycast (ray, out hit)) {
						Product p = hit.collider.GetComponent<Product> ();
						if (p != null) {
							carrying = true;
							carriedObject = p.gameObject;
							p.GetComponent<Rigidbody>().isKinematic = true; 
						}
					}
					//down
					//click
					break;
				}
			case 4:{
					Debug.Log("Case 4");
					dropObject ();
					break;
				}
			case 5:{
					Debug.Log("Case 5");
					int x = Screen.width / 2;
					int y = Screen.height / 2;

					Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));
					RaycastHit hit;
					if (Physics.Raycast (ray, out hit)) {
						Product p = hit.collider.GetComponent<Product> ();
						if (p != null) {
							carrying = true;
							carriedObject = p.gameObject;
							p.GetComponent<Rigidbody>().isKinematic = true; 
						}
					}
					//up
					//click
					break;
				}
			}

		}
		catch{
		}




	
	}
	void carry(GameObject o) {
		o.transform.position = Vector3.Lerp(o.transform.position,mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
	}
	void pickup(){
		if (Input.GetKeyDown (KeyCode.E)) {
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay (new Vector3 (x, y));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				Product p = hit.collider.GetComponent<Product> ();
				if (p != null) {
					carrying = true;
					carriedObject = p.gameObject;
					p.GetComponent<Rigidbody>().isKinematic = true; 
				}
			}
		}
	}
	void checkDrop(){
		if (Input.GetKeyDown (KeyCode.E)) {
			dropObject ();
		}
	}
	void dropObject(){
		carrying = false; 
		carriedObject.GetComponent<Rigidbody>().isKinematic = false; 
		carriedObject = null;

	}

}
