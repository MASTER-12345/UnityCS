using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class fpsMouseLock : MonoBehaviourPun {

    private Transform camare;

    private Vector3 camerarotation;

	// Use this for initialization
	void Start () {
        camare = transform;
	}
	
	// Update is called once per frame
	void Update () {
        //Cursor.lockState = CursorLockMode.Locked;

        if (photonView.IsMine)
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





    }
}
