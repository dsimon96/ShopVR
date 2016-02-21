// Get the latest webcam shot from outside "Friday's" in Times Square
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
	public Renderer rend;

	public string url = "https://i.ytimg.com/vi/tntOCGkgt98/maxresdefault.jpg";
	IEnumerator Start() {
		WWW www = new WWW (url);
		yield return www;
		//Renderer renderer = GetComponent<Renderer>();
		rend.material.mainTexture = www.texture;
	}
	// Toggle the Object's visibility each second.
	void Update() {
		// Find out whether current second is odd or even
		bool oddeven = Mathf.FloorToInt(Time.time) % 2 == 0;
		
		// Enable renderer accordingly
		rend.enabled = oddeven;
	}
}