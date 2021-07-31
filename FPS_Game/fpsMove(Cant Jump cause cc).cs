using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;



public class fpsMove : MonoBehaviourPunCallbacks,IPunObservable {

    private CharacterController cc;
    private Vector3 direction;

    private Animator anima;


    private float speed;

    public float height;

    private ETCJoystick moveJoystic;
   

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

        if (photonView.IsMine)
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







            //摇杆方向

            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            direction = new Vector3(h, 0, v);


            //将direction转化为自己世界的坐标系

            //cc.SimpleMove(cc.transform.TransformDirection(direction * speed));

            if (Input.GetKey(KeyCode.W))
            {
                anima.SetBool("walk", true);

            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                anima.SetBool("walk", false);

            }
            if (Input.GetKey(KeyCode.S))
            {
                anima.SetBool("back", true);

            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                anima.SetBool("back", false);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 6;


            }
            else
            {
                speed = 3;
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {


                transform.position = transform.position + new Vector3(0, 5, 0);

            }







        }




    }



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
     
    }


 
}
