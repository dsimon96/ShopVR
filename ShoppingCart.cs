using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Networking;

public class ShoppingCart : MonoBehaviour {
    private string token = "d08083533559056453e711410cdd13e0dfd24f35";
    private int maxSize = 20;
    public int numberItems;
    public Product[] contents;
    public int[] quantities;

	// Use this for initialization
	void Start () {
        contents = new Product[maxSize];
        quantities = new int[maxSize];
        numberItems = 0;
	}

    int ItemIndex (Product item) {
        for (int i = 0; i < numberItems; i++) {
            if (item.productName == contents[i].productName) {
                return i;
            }
        }
        return -1;
    }

    void AddItem (Product item) {
        int index = ItemIndex(item);
        if (index != -1) {
            quantities[index] += 1;
        } else if (numberItems < maxSize) {
            contents[numberItems] = item;
            numberItems++;
        }
    }

    void RemoveItem (Product item) {
        int index = ItemIndex(item);
        if (index != -1) {
            quantities[index] -= 1;
            if (quantities[index] == 0) {
                for (int i = index + 1; i < maxSize; i++) {
                    contents[i - 1] = contents[i];
                    quantities[i - 1] = quantities[i];
                }
                numberItems -= 1;
            }
        }
    }

    IEnumerator SendAPIRequest(string commands) {
        WWWForm form = new WWWForm();
        form.AddField("token", token);
        form.AddField("commands", commands);

        using (UnityWebRequest www = UnityWebRequest.Post("https://todoist.com/API/v6/sync", form))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Form upload complete!");
            }
        }
    }

    void ExportItem (Product item, int quantity) {
        string content = string.Format("Buy %s", item.productName);
        if (quantity > 1) content += string.Format(" (x%d)", quantity);
        string UUID = System.Guid.NewGuid().ToString();
        string commands = string.Format("[{\"type\": \"item_add\", \"args\": {\"content\": \"%s\"}, \"uuid\":\"%s\", \"temp_id\":\"asdf\"}]", content, UUID);
        StartCoroutine(SendAPIRequest(commands));
    }

    void ExportContents () {
        for (int i = 0; i < numberItems; i++) {
            ExportItem(contents[i], quantities[i]);    
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
