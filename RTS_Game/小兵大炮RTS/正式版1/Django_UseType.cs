using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[Serializable]
public class _AGENT
{
    public bool isAgent = false;
    public GameObject agent;

    public bool isGround = false;


}
[Serializable]
public class _Identity
{

    public Identity.identity_ obj_identity;

    public enum _Unit_Kind
    {
        Sodier,Vechle,Building

    }
    public _Unit_Kind unit_kind = _Unit_Kind.Sodier;
}

public class Django_UseType : MonoBehaviourPunCallbacks,IPunObservable
{
    //仅仅只是判断大致类型，具体的兵种车种由自带的方法进行判断实现不同的效果方式
    public _AGENT agent;
    public _Identity identity;

    
      

    

    private void Start()
    {
        if (photonView.IsMine)
        {
            //改身份
            GetComponent<Django_UseType>().identity.obj_identity = GameObject.Find("Identity").GetComponent<Identity>().my_identity;

        }
      


    }

   

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(identity.obj_identity);
        }
        else
        {
            this.identity.obj_identity = (Identity.identity_)stream.ReceiveNext();
        }


    }









}
