using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Photon.Pun;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class Lot_Luancher : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// 版本
    /// </summary>
    private string GameVersion = "1";

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField,BoxGroup("Parameter")]
    private byte maxPlayersPerRoom = 8;
 

   

    //房间列表的UI
    public GameObject roomInfo_Ui;

    //房间列表容器
    public GameObject Roomlist_Content;

    //创建房间时候需要的参数
    public GameObject creatRoom_Name;
    public GameObject creatRoom_Pwd;
    public GameObject creatRoom_Map;
    public GameObject creatRoom_mode;


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
        GetComponent<Lot_LobyUiManager>().Play_Mic(GetComponent<Lot_LobyUiManager>().click);

        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            //PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.NetworkingClient.SerializationProtocol = SerializationProtocol.GpBinaryV16;
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

    

    public void Creat_Room()
    {

        ExitGames.Client.Photon.Hashtable my_RoomOptions = new ExitGames.Client.Photon.Hashtable();



        string roomName = creatRoom_Name.GetComponent<Text>().text.ToString();
        string password = creatRoom_Pwd.GetComponent<Text>().text.ToString();
        string map = creatRoom_Map.GetComponent<Text>().text.ToString();
        string mode = creatRoom_mode.GetComponent<Text>().text.ToString();


        my_RoomOptions.Add("pwd", password);
        my_RoomOptions.Add("map", map);
        my_RoomOptions.Add("mode", mode);

        PhotonNetwork.CreateRoom(roomName, new RoomOptions()
        { MaxPlayers = maxPlayersPerRoom, PublishUserId = true, CustomRoomProperties = my_RoomOptions }, TypedLobby.Default);


    }

    public void Join_Room(string name)
    {

        PhotonNetwork.JoinRoom(name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("加入成功.");
        PhotonNetwork.LoadLevel("TestMap");
    }

    public override void OnConnectedToMaster()
    {

        Debug.Log("连接成功");
        GetComponent<Lot_LobyUiManager>().status.GetComponent<Text>().text = "连接状态：已连接";
        fill_Info();
        TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
        PhotonNetwork.JoinLobby(customLobby);
        StartCoroutine(figeure());

    }

    public void fill_Info()
    {
        //昵称储存
        GetComponent<Lot_LobyUiManager>().LoginPanel.SetActive(false);
        GetComponent<Lot_LobyUiManager>().PlayerLoginPanel.SetActive(false);
        PhotonNetwork.NickName = GetComponent<Lot_LobyUiManager>().NickName_Input.GetComponent<Text>().text;
        print(PhotonNetwork.NickName);
        GetComponent<Lot_LobyUiManager>().NickName_Lable.GetComponent<Text>().text = PhotonNetwork.NickName;
        
    }



    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("断开链接 {0}", cause);

    }

    public void random_Join_room()
    {
        GetComponent<Lot_LobyUiManager>().Play_Mic(GetComponent<Lot_LobyUiManager>().click);

        PhotonNetwork.JoinRandomRoom();

    }


    IEnumerator figeure()
    {
        //统计人数和房间数
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int player_lobby = PhotonNetwork.CountOfPlayers;
            int rooms = PhotonNetwork.CountOfRooms;

            GetComponent<Lot_LobyUiManager>().online.GetComponent<Text>().text = "在线人数:"+player_lobby;
            GetComponent<Lot_LobyUiManager>().roomcount.GetComponent<Text>().text = "房间数:" + rooms;
            GetComponent<Lot_LobyUiManager>().version.GetComponent<Text>().text = "版本:0.1f~1.0f";



        }

    }
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //UpdateCachedRoomList(roomList);
        //update_Roomlist_Info();
        //

    }

    public void update_Roomlist_Info()
    {

        //清空房间信息
        foreach (Transform i in Roomlist_Content.transform)
        {
            Destroy(i.gameObject);
        }

        //更新房间信息


        print("房间数：");
        print(cachedRoomList.Count);
        foreach (var item in cachedRoomList)
        {
            RoomInfo info = item.Value;
            print("房间名：" + info.Name);

            //life_and_pursuit
            print("人数" + info.PlayerCount);
            //Debug.LogError(info.IsOpen+"isopn");
            GameObject new_roomUI = Instantiate(roomInfo_Ui, transform.position, Quaternion.identity);
            new_roomUI.transform.SetParent(Roomlist_Content.transform, false);
            new_roomUI.GetComponent<RectTransform>().sizeDelta = new Vector2(410, 60);
            //更改ui详细内容
            new_roomUI.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text = info.Name;

            new_roomUI.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Text>().text = info.PlayerCount.ToString() + "/4";

            if (info.IsOpen == false)
            {

                //不可加入
                new_roomUI.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
                new_roomUI.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>()
                    .text = "游戏中";
                new_roomUI.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.red;
            }


        }

    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }



}
