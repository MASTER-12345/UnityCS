

//获取房间列表

private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
//**必须先加入大厅才能接受消息
TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
PhotonNetwork.JoinLobby(customLobby);

public override void OnRoomListUpdate(List<RoomInfo> roomList)
  {
      UpdateCachedRoomList(roomList);
      update_Roomlist_Info();
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
    foreach(var item in cachedRoomList)
    {
        RoomInfo info = item.Value;
        print("房间名："+info.Name);

        //life_and_pursuit
        print("人数" + info.PlayerCount);
        Debug.LogError(info.IsOpen+"isopn");
        GameObject new_roomUI=Instantiate(roomInfo_Ui,transform.position,Quaternion.identity);
        new_roomUI.transform.SetParent(Roomlist_Content.transform,false);
        new_roomUI.GetComponent<RectTransform>().sizeDelta = new Vector2(410, 60);
        //更改ui详细内容
        new_roomUI.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text = info.Name;

        new_roomUI.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Text>().text = info.PlayerCount.ToString()+"/4";

        if (info.IsOpen == false)
        {

            //不可加入
            new_roomUI.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = false;
            new_roomUI.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>()
                .text = "游戏中";
            new_roomUI.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().color=Color.red;
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


