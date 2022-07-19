 IEnumerator post()
    {
        string url = "http://101.43.71.175:4000/combatunit/save";
        save saver = new save();
        saver.attributeJson = "dwad";
        string savejson = JsonUtility.ToJson(saver);

        UnityWebRequest www = UnityWebRequest.Post(url, savejson);
        www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(savejson);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        yield return www.SendWebRequest();
        if (www.isDone)
        {
            print(www.downloadHandler.text);       }




    }
