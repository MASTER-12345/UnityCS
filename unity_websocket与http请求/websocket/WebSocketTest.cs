using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSocketTest : MonoBehaviour
{
    public string URL = "ws://127.0.0.1:6000";

    public string Content = "测试内容";

    // Start is called before the first frame update
    void Start()
    {
        WebSocketClient.Instance.Init(URL);
        WebSocketClient.Instance.HttpRequest.AddHeader("X-AUTH-TOKEN", "eyJhbGciOiJIUzI1NiJ9.eyJqdGkiOiJkNTU0Njg3Zi1jNGZkLTQ0MjQtOGJlOS1lMzEzMWU0Yzk4MDYiLCJzdWIiOiJUSi1ZTCIsImlhdCI6MTY1MzM1NjQ1N30.jE7h6z50etjpEXrC86VXgs3GXnr1y5l7GkV-FiPvaZ4");
        //WebSocketClient.Instance.HttpRequest.AddHeader("Content-Type", "application/json");
        WebSocketClient.Instance.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            WebSocketClient.Instance.SendMessage(Content);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            WebSocketClient.Instance.DisConnect();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            WebSocketClient.Instance.Connect(URL);
        }
    }
}
