using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public  class save
{
    public string attributeJson;
    public string behaviorJson;
    public string firstType;
    public string name;
    public string secondType;



}

public class HTTP : MonoBehaviour
{




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(post());
        //save();

       
    }

   IEnumerator get()
    {
        string url = "https://img-blog.csdnimg.cn/20200716093912452.png";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();
        if (www.isDone)
        {

            byte[] dt = www.downloadHandler.data;

         
            FileStream img = File.Create(Application.persistentDataPath + "/aaa.png");
            print(Application.persistentDataPath + "/aaa.png");
            img.Write(dt, 0, dt.Length);
            img.Flush();
            img.Close();
            img.Dispose();


        }


    }

    IEnumerator get_info()
    {
        string url = "http://101.43.71.175:4000/forceclient/online";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();
        if (www.isDone)
        {
            print(www.downloadHandler.text);
        }

    }

    public void save()
    {
        save2 saver = new save2();
        saver.attributeJson = "dwad";


        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        FileInfo file = new FileInfo(path);
        StreamWriter sw = file.CreateText();
        string savejson = JsonUtility.ToJson(saver);
        sw.WriteLine(savejson);
        sw.Close();
        sw.Dispose();



    }

    IEnumerator post()
    {
        string url = "http://101.43.71.175:4000/combatunit/save";
        save2 saver = new save2();
        saver.attributeJson = "dwad";
        string savejson = JsonUtility.ToJson(saver);

        UnityWebRequest www = UnityWebRequest.Post(url, savejson);
        www.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(savejson);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        yield return www.SendWebRequest();
        if (www.isDone)
        {
            print(www.downloadHandler.text);

        }




    }

}
