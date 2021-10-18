using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Django_Luancher : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// 版本
    /// </summary>
    private string GameVersion = "1";
    
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    public GameObject Connect_button;
    public GameObject Login_panel;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        
    }
    /// <summary>
    /// 尝试链接服务器
    /// </summary>
    public void Connect()
    {
        Connect_button.GetComponent<Button>().enabled = false;
        
        
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            //PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = GameVersion;
        }
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("空房间，正在创建房间");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("加入成功.");
        PhotonNetwork.LoadLevel("Room");
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("连接成功");
        Login_panel.SetActive(false);
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("断开链接 {0}", cause);
        Connect_button.GetComponent<Button>().enabled = true;
    }

    public void random_Join_room()
    {
        PhotonNetwork.JoinRandomRoom();

    }

    
    
}
