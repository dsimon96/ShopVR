using UnityEngine;

public class Product : MonoBehaviour {
    public TextAsset objectData;
    public int TCIN;
    public string productName;
    public int priceDollars;
    public int priceCents;
    public string description;

	// Use this for initialization
	void Start () {
        string json = objectData.text;
        JsonUtility.FromJsonOverwrite(json, this);
	}
    
	// Update is called once per frame
	void Update () {
	
	}
}
