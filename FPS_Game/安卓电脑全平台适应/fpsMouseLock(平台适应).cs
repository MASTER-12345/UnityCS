using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fpsMouseLock : MonoBehaviour {

    private Transform camare;

    private Vector3 camerarotation;

    public bool isWindows = false;


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
        camare = transform;
	}
	
	// Update is called once per frame
	void Update () {
        //Cursor.lockState = CursorLockMode.Locked;
        if (isWindows == true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {

                //Cursor.lockState = CursorLockMode.None;

            }


            var mouse_x = Input.GetAxis("Mouse X");
            var mouse_y = Input.GetAxis("Mouse Y");

            camerarotation.y += mouse_x;
            camerarotation.x -= mouse_y;

            camare.rotation = Quaternion.Euler(camerarotation.x * 3, camerarotation.y * 3, 0);
        }
        if (isWindows == false)
        {


        }

           



        





    }
}
