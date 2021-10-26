   ExitGames.Client.Photon.Hashtable my_RoomOptions = new ExitGames.Client.Photon.Hashtable();
        
        

        string roomName = creatRoom_Name.GetComponent<Text>().text.ToString();
        string password = creatRoom_Pwd.GetComponent<Text>().text.ToString();
        string map = creatRoom_Map.GetComponent<Text>().text.ToString();
        string mode=creatRoom_mode.GetComponent<Text>().text.ToString();
     
        
        my_RoomOptions.Add("pwd",password);
        my_RoomOptions.Add("map",map);
        my_RoomOptions.Add("mode",mode);

        PhotonNetwork.CreateRoom(roomName, new RoomOptions()
            { MaxPlayers =maxPlayersPerRoom,PublishUserId = true,CustomRoomProperties = my_RoomOptions},TypedLobby.Default);
