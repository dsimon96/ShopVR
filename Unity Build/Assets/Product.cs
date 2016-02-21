using UnityEngine;

public class Product : MonoBehaviour {
    public TextAsset objectData;
    public int TCIN;
    public string productName;
    public int priceDollars;
    public int priceCents;
	private Vector3 shelfPosition;
	private Quaternion initialRotation;

	// Use this for initialization
	void Start () {
        string json = objectData.text;
        JsonUtility.FromJsonOverwrite(json, this);
		shelfPosition = transform.position;
		initialRotation = transform.rotation;
	}
    
	// Update is called once per frame
	void Update () {
	
	}

	public void RestorePosition () {
		transform.position = shelfPosition;
		transform.rotation = initialRotation;
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
}


