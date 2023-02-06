//可实现垂直水平后坐力以及其回归,,配合lowpoly 3.2的移动与枪口摇摆功能，实现大厂的反馈感
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    float h;
    float v;
    public float initTime;
     float fireTime=0.1f;

    float hou;
    float shui;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h += Input.GetAxis("Mouse X");
        v += Input.GetAxis("Mouse Y");

        hou = Mathf.MoveTowards(hou, 0, 0.03f);//将后坐力降至0,回归
        shui = Mathf.MoveTowards(shui, 0, 0.03f);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time - initTime > fireTime)
            {
                print("23");
                initTime = Time.time;
                hou = hou + 3.5f;//添加后坐力
                shui = shui + Random.RandomRange(-8, 8);
            }
            
        }
        transform.localEulerAngles = new Vector3 (-(v + hou), h+shui, 0);
    }

}
