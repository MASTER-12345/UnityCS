using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using System.IO;

public class WebRequest : MonoBehaviour
{

    public Slider slider;
    public Text text;//内容显示
    public Text progressText;//进度显示

    void Start()
    {
        StartCoroutine(GetText());
    }
    IEnumerator GetText()
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get("https://gtx-1300923661.cos.ap-chongqing.myqcloud.com/GTX106.apk"))
        {
            uwr.SendWebRequest();//开始请求
            while (!uwr.isDone)
            {
               
                slider.value = uwr.downloadProgress;//展示下载进度
                progressText.text = Math.Floor(uwr.downloadProgress * 100) + "%";
                yield return 1;
            }
            if (uwr.isDone)
            {
                progressText.text = 100 + "%";
                slider.value = 1;

                byte[] results = uwr.downloadHandler.data;
                string savePath = "/data/gtx";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                FileInfo fileInfo = new FileInfo(savePath + "/" + "gtxoooo.apk");
                FileStream fs = fileInfo.Create();
                //fs.Write(字节数组, 开始位置, 数据长度);
                fs.Write(results, 0, results.Length);
                fs.Flush(); //文件写入存储到硬盘
                fs.Close(); //关闭文件流对象
                fs.Dispose(); //销毁文件对象
 


            }
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                
            }
        }
    }
}
