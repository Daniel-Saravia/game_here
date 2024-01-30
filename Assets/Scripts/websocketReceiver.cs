using UnityEngine;
using WebSocketSharp;

public class WebSocketController : MonoBehaviour
{
    WebSocket ws;

    GameObject objectToChange;
    NumberData parsedData;

    [System.Serializable]
    public class NumberData
    {
        public int number;
    }

    void Start()
    {

        objectToChange = GameObject.Find("Target");
        Renderer renderer = objectToChange.GetComponent<Renderer>();

        Debug.Log("Current color: " + renderer.material.color);

        ws = new WebSocket("ws://localhost:9999");

               ws.OnMessage += (sender, e) => {
                    //Debug.Log("Received from server: " + e.Data);
                    parsedData = JsonUtility.FromJson<NumberData>(e.Data);
               };

       ws.Connect();
    }

    Color materialColor(int n)
    {
        Debug.Log("getting color: " + n);

        switch (n)
        {
            case 1:
                return Color.red;
                break;
            case 2:
                return Color.blue;
                break;
            case 3:
                return Color.green;
                break;
            case 4:
                return Color.yellow;
                break;
            case 5:
                return Color.magenta;
                break;
            case 6:
                return Color.black;
                break;
            case 7:
                return Color.cyan;
                break;
            case 8:
                return Color.grey;
                break;
            case 9:
                return Color.white;
                break;
            default:
                return Color.black;

        }
    }
    
    void OnDestroy()
    {
        ws.Close();
    }

    private void Update()
    {

        Renderer renderer = objectToChange.GetComponent<Renderer>();
        renderer.material.color = materialColor(parsedData.number);
    }

}