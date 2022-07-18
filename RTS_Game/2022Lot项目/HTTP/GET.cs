using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class HTTP : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(download());
    }

   IEnumerator download()
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
}
