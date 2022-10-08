//需要用到Bsethttp插件

using System;
using System.IO;
using BestHTTP;
using BestHTTP.WebSocket;
using BestHTTP.WebSocket.Frames;
using UnityEngine;



public class WebSocketClient:MonoBehaviour
{
    private WebSocket m_WebSocket = null;

    private bool m_IsInit = false;

    public string Url = "ws://127.0.0.1:6000";

    public Action<WebSocket> OnAction_Opened = null;
    public Action<WebSocket, UInt16, string> OnAction_Closed = null;
    public Action<WebSocket, string> OnAction_Error = null;
    public Action<WebSocket, string> OnAction_MessageReceived = null;
    public Action<WebSocket, byte[]> OnAction_Binary = null;
    public Action<WebSocket, WebSocketFrameReader> OnAction_IncompleteFrame = null;

    public bool IsConnected
    {
        get { return m_WebSocket != null && m_WebSocket.IsOpen; }
    }

    public HTTPRequest HttpRequest
    {
        get { return m_WebSocket != null ? m_WebSocket.InternalRequest : null; }
    }

    #region 单例
    static protected WebSocketClient _instance;
    static protected bool IsCreate = false;

    public static WebSocketClient Instance
    {
        get
        {
            if (IsCreate == false)
            {
                CreateInstance();
            }

            return _instance;
        }
    }

    public static void CreateInstance()
    {
        if (IsCreate == true)
            return;

        IsCreate = true;
        _instance = new WebSocketClient();
    }

    public static void ReleaseInstance()
    {
        _instance = default(WebSocketClient);
        IsCreate = false;
    }
    #endregion

    #region 构造函数
    private WebSocketClient()
    {

    }
    #endregion

    #region 初始化
    /// <summary>
    /// 初始化
    /// </summary>
    public bool Init(string url)
    {
        if (!m_IsInit)
        {
            m_IsInit = true;

            m_WebSocket = new WebSocket(new System.Uri(url));
            m_WebSocket.OnOpen += OnOpen;
            m_WebSocket.OnMessage += OnMessageReceived;
            m_WebSocket.OnError += OnError;
            m_WebSocket.OnClosed += OnClosed;
            m_WebSocket.OnBinary += Onbinary;
            m_WebSocket.OnIncompleteFrame += OnIncompleteFrame;

            Debug.Log("初始化WebSocket成功，URL：" + url);

            return true;
        }
        Debug.LogWarning("初始化WebSocket失败，URL：" + url);
        return false;
    }
    public bool Init(string url, string origin, string protocol)
    {
        if (!m_IsInit)
        {
            m_IsInit = true;

            m_WebSocket = new WebSocket(new System.Uri(url), origin, protocol);
            m_WebSocket.OnOpen += OnOpen;
            m_WebSocket.OnMessage += OnMessageReceived;
            m_WebSocket.OnError += OnError;
            m_WebSocket.OnClosed += OnClosed;
            m_WebSocket.OnBinary += Onbinary;
            m_WebSocket.OnIncompleteFrame += OnIncompleteFrame;

            Debug.Log("初始化WebSocket成功，URL：" + url);

            return true;
        }
        Debug.LogWarning("初始化WebSocket失败，URL：" + url);
        return false;
    }

    public bool Init(Uri url)
    {
        if (!m_IsInit)
        {
            m_IsInit = true;

            m_WebSocket = new WebSocket(url);
            m_WebSocket.OnOpen += OnOpen;
            m_WebSocket.OnMessage += OnMessageReceived;
            m_WebSocket.OnError += OnError;
            m_WebSocket.OnClosed += OnClosed;
            m_WebSocket.OnBinary += Onbinary;
            m_WebSocket.OnIncompleteFrame += OnIncompleteFrame;

            Debug.Log("初始化WebSocket成功，URL：" + url);

            return true;
        }
        Debug.LogWarning("初始化WebSocket失败，URL：" + url);
        return false;
    }

    public bool Init(Uri url, string origin, string protocol)
    {
        if (!m_IsInit)
        {
            m_IsInit = true;

            m_WebSocket = new WebSocket(url, origin, protocol);
            m_WebSocket.OnOpen += OnOpen;
            m_WebSocket.OnMessage += OnMessageReceived;
            m_WebSocket.OnError += OnError;
            m_WebSocket.OnClosed += OnClosed;
            m_WebSocket.OnBinary += Onbinary;
            m_WebSocket.OnIncompleteFrame += OnIncompleteFrame;

            Debug.Log("初始化WebSocket成功，URL：" + url);

            return true;
        }
        Debug.LogWarning("初始化WebSocket失败，URL：" + url);
        return false;
    }

    /// <summary>
    /// 重新初始化
    /// </summary>
    public bool ReInit(string url)
    {
        if (m_WebSocket != null)
        {
            m_WebSocket.Close();

            m_WebSocket.OnOpen = null;
            m_WebSocket.OnMessage = null;
            m_WebSocket.OnError = null;
            m_WebSocket.OnClosed = null;
            m_WebSocket.OnBinary = null;
            m_WebSocket.OnIncompleteFrame = null;

            m_WebSocket = null;

            m_IsInit = false;

            Debug.Log("关闭重置WebSocket成功");
        }

        return Init(url);
    }
    public bool ReInit(string url, string origin, string protocol)
    {
        if (m_WebSocket != null)
        {
            m_WebSocket.Close();

            m_WebSocket.OnOpen = null;
            m_WebSocket.OnMessage = null;
            m_WebSocket.OnError = null;
            m_WebSocket.OnClosed = null;
            m_WebSocket.OnBinary = null;
            m_WebSocket.OnIncompleteFrame = null;

            m_WebSocket = null;

            m_IsInit = false;

            Debug.Log("关闭重置WebSocket成功");
        }

        return Init(url, origin, protocol);
    }
    public bool ReInit(Uri url)
    {
        if (m_WebSocket != null)
        {
            m_WebSocket.Close();

            m_WebSocket.OnOpen = null;
            m_WebSocket.OnMessage = null;
            m_WebSocket.OnError = null;
            m_WebSocket.OnClosed = null;
            m_WebSocket.OnBinary = null;
            m_WebSocket.OnIncompleteFrame = null;

            m_WebSocket = null;

            m_IsInit = false;

            Debug.Log("关闭重置WebSocket成功");
        }

        return Init(url);
    }
    public bool ReInit(Uri url, string origin, string protocol)
    {
        if (m_WebSocket != null)
        {
            m_WebSocket.Close();

            m_WebSocket.OnOpen = null;
            m_WebSocket.OnMessage = null;
            m_WebSocket.OnError = null;
            m_WebSocket.OnClosed = null;
            m_WebSocket.OnBinary = null;
            m_WebSocket.OnIncompleteFrame = null;

            m_WebSocket = null;

            m_IsInit = false;

            Debug.Log("关闭重置WebSocket成功");
        }

        return Init(url, origin, protocol);
    }
    #endregion

    #region 连接服务
    /// <summary>
    /// 主动连接
    /// </summary>
    /// <param name="url">连接地址</param>
    /// <returns></returns>
    public bool Connect(string url)
    {
        if (ReInit(url))
        {
            m_WebSocket.Open();
            Debug.Log("打开WebSocket");

            return m_WebSocket.IsOpen;
        }
        else
        {
            return false;
        }
    }
    public bool Connect(string url, string origin, string protocol)
    {
        if (ReInit(url, origin, protocol))
        {
            m_WebSocket.Open();
            Debug.Log("打开WebSocket");

            return m_WebSocket.IsOpen;
        }
        else
        {
            return false;
        }
    }
    public bool Connect(Uri url)
    {
        if (ReInit(url))
        {
            m_WebSocket.Open();
            Debug.Log("打开WebSocket");

            return m_WebSocket.IsOpen;
        }
        else
        {
            return false;
        }
    }
    public bool Connect(Uri url, string origin, string protocol)
    {
        if (ReInit(url, origin, protocol))
        {
            m_WebSocket.Open();
            Debug.Log("打开WebSocket");

            return m_WebSocket.IsOpen;
        }
        else
        {
            return false;
        }
    }

    public bool Connect()
    {
        if (m_WebSocket != null)
        {
            m_WebSocket.Open();
            Debug.Log("打开WebSocket");

            return m_WebSocket.IsOpen;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 断开连接
    /// </summary>
    /// <returns></returns>
    public bool DisConnect()
    {
        if (m_WebSocket != null)
        {
            m_WebSocket.Close();
            return true;
        }
        return false;
    }
    #endregion

    #region 发送消息
    public void SendMessage(int msgType, byte[] data)
    {
        //内存流对象
        using (MemoryStream ms = new MemoryStream())
        {
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                //先写入长度
                bw.Write(msgType);
                //再写入数据
                bw.Write(data);

                byte[] byteArray = new byte[(int)ms.Length];
                Buffer.BlockCopy(ms.GetBuffer(), 0, byteArray, 0, (int)ms.Length);

                m_WebSocket?.Send(byteArray);
            }
        }
    }

    public void SendMessage(byte[] data)
    {
        m_WebSocket?.Send(data);
    }

    public void SendMessage(string data)
    {
        m_WebSocket?.Send(data);
        Debug.Log(string.Format("{0:D2}:{1:D2}:{2:D2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) + data);
    }
    #endregion

    #region 回调
    /// <summary>
    /// 连接成功回调
    /// </summary>
    /// <param name="webSocket"></param>
    private void OnOpen(WebSocket webSocket)
    {
        Debug.Log("连接成功！");
        OnAction_Opened?.Invoke(webSocket);
    }

    /// <summary>
    /// 关闭连接回调
    /// </summary>
    /// <param name="webSocket"></param>
    private void OnClosed(WebSocket webSocket, UInt16 code, string message)
    {
        Debug.Log("关闭连接：" + message);
        OnAction_Closed?.Invoke(webSocket, code, message);
    }

    /// <summary>
    /// 错误回调
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="reason"></param>
    private void OnError(WebSocket webSocket, string reason)
    {
        Debug.LogError($"错误消息：{reason}");
        OnAction_Error?.Invoke(webSocket, reason);
    }

    /// <summary>
    /// 收到消息
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="message"></param>
    /// 
    [System.Serializable]
    public class et
    {
        public string messageType;
        public string messageInfo;
    }

    [System.Serializable]
    public class et2
    {
        public string syncByte;

    }
    private void OnMessageReceived(WebSocket webSocket, string message)
    {
        Debug.Log($"接收到消息：{message}");
        var data = JsonUtility.FromJson<et>(message);
        string msgtype = data.messageType;
        string msginfo = data.messageInfo;

        var second_ = JsonUtility.FromJson<et2>(msginfo);

        string sybyte = second_.syncByte;
        print(sybyte);

        OnAction_MessageReceived?.Invoke(webSocket, message);
    }

    /// <summary>
    /// 收到不完整帧时的消息
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="frame"></param>
    private void OnIncompleteFrame(WebSocket webSocket, WebSocketFrameReader frame)
    {

        Debug.LogWarning("接收到不完整帧的消息：" + frame.DataAsText);
        OnAction_IncompleteFrame?.Invoke(webSocket, frame);
    }

    /// <summary>
    /// 接收二进制消息
    /// </summary>
    /// <param name="webSocket"></param>
    /// <param name="data"></param>
    private void Onbinary(WebSocket webSocket, byte[] data)
    {
        Debug.Log("接收到二进制消息");
        OnAction_Binary?.Invoke(webSocket, data);
    }
    #endregion
}
