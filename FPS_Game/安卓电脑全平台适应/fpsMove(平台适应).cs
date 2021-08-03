using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class fpsMove : MonoBehaviour {

    private CharacterController cc;
    private Vector3 direction;

    private Animator anima;


    private float speed;

    public float height;

    private ETCJoystick moveJoystic;

    public bool isWindows=false;


    public void Awake()
    {


    #if UNITY_ANDROID
            Debug.Log("这里是安卓设备^_^");
    #endif

    #if UNITY_IPHONE
            Debug.Log("这里是苹果设备>_<");

        
    #endif

    #if UNITY_STANDALONE_WIN
            Debug.Log("我是从Windows的电脑上运行的T_T");
        isWindows = true;
    #endif
        }


    // Use this for initialization
    void Start () {

        cc = GetComponent<CharacterController>();

        speed = 3;
        anima =GetComponent<Animator>();

        //固定鼠标在中心，并且不显示
        //Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        moveJoystic = ETCInput.GetControlJoystick("moveJoystick");



        
    }
	
	// Update is called once per frame
	void Update () {

        if (isWindows == false)
        {

            float h2 = moveJoystic.axisX.axisValue;

            //获取摇杆垂直轴的值

            float v2 = moveJoystic.axisY.axisValue;

            if (h2 != 0 || v2 != 0)

            {

                //获取摇杆的方向

                Vector3 dir = new Vector3(h2, 0, v2);

                //把方向转换到相机的坐标系中
                cc.SimpleMove(cc.transform.TransformDirection(dir * speed));

            }


        }
        if (isWindows == true)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            direction = new Vector3(h, 0, v);




            cc.SimpleMove(cc.transform.TransformDirection(direction * speed));



        }





    }



}
