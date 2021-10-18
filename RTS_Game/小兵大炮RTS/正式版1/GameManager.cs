using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;


[Serializable]
public class _Red_JD
{
    public GameObject Base;
    public GameObject red_comsodier_jd;
    public GameObject red_projecter_jd;
    
    public GameObject red_instation;
    public GameObject red_projecter_positon;
    public GameObject red_comsodier_positon;

    public string Functon = "提前生成士兵资源";
    public GameObject red_comsodier;
    public GameObject instationPoint;
    
}

[Serializable]
public class _Yellow_JD
{
    public GameObject Base;
    public GameObject yellow_comsodier_jd;
    public GameObject yellow_projecter_jd;
    
    public GameObject yellow_instation;
    public GameObject yellow_projecter_positon;
    public GameObject yellow_comsodier_positon;

    public string Functon = "提前生成士兵资源";
    public GameObject yellow_comsodier;
    public GameObject instationPoint;


}


public class Django_GamaManager : MonoBehaviourPunCallbacks
{
    private GameObject my_identity;
    public _Red_JD red_Jd_plugin;
    public _Yellow_JD yellow_Jd_plugin;
    //判断是否游戏已经开始过了，就不会再次生成，用于断线重连
    public bool has_start = false;


    public GameObject Eyes;
  
    void Start()
    {
        my_identity=GameObject.FindGameObjectWithTag("identity");
        Identity.identity_ id = my_identity.GetComponent<Identity>().my_identity;

        if (has_start == false)
        {
            if (id == Identity.identity_.red)
            {
               
                
                GameObject redjd=PhotonNetwork.Instantiate("red_jd/"+red_Jd_plugin.Base.name, red_Jd_plugin.red_instation.transform.position, Quaternion.identity);
                GameObject redprojecer=PhotonNetwork.Instantiate("red_jd/"+red_Jd_plugin.red_projecter_jd.name, red_Jd_plugin.red_projecter_positon.transform.position,
                    Quaternion.identity);
                GameObject redsodier = PhotonNetwork.Instantiate("red_jd/" + red_Jd_plugin.red_comsodier_jd.name,
                    red_Jd_plugin.red_comsodier_positon.transform.position, Quaternion.identity);
                GameObject mt_tank = PhotonNetwork.Instantiate("MT_TANK/" + red_Jd_plugin.red_comsodier.name, red_Jd_plugin.instationPoint.transform.position, Quaternion.identity);
              
                //所有东西进行注册
                Eyes.GetComponent<Django_EyesControler>().register_List.Add(redjd);
                Eyes.GetComponent<Django_EyesControler>().register_List.Add(redprojecer);
                Eyes.GetComponent<Django_EyesControler>().register_List.Add(redsodier);

                
                Eyes.GetComponent<Django_EyesControler>().register_List.Add(mt_tank.transform.GetChild(0).gameObject);
             





            }
            else if(id==Identity.identity_.yellow)
            {
                GameObject yellowjd=PhotonNetwork.Instantiate("yellow_jd/"+yellow_Jd_plugin.Base.name, yellow_Jd_plugin.yellow_instation.transform.position, Quaternion.identity);
                GameObject yellowprojecter=PhotonNetwork.Instantiate("yellow_jd/"+yellow_Jd_plugin.yellow_projecter_jd.name, yellow_Jd_plugin.yellow_projecter_positon.transform.position,
                    Quaternion.identity);
                GameObject yellowsodier=PhotonNetwork.Instantiate("yellow_jd/" + yellow_Jd_plugin.yellow_comsodier_jd.name,
                    yellow_Jd_plugin.yellow_comsodier_positon.transform.position, Quaternion.identity);
                GameObject mt_tank = PhotonNetwork.Instantiate("MT_TANK/" + yellow_Jd_plugin.yellow_comsodier.name, yellow_Jd_plugin.instationPoint.transform.position, Quaternion.identity);



                Eyes.GetComponent<Django_EyesControler>().register_List.Add(yellowjd);
                Eyes.GetComponent<Django_EyesControler>().register_List.Add(yellowprojecter);
                Eyes.GetComponent<Django_EyesControler>().register_List.Add(yellowsodier);

                Eyes.GetComponent<Django_EyesControler>().register_List.Add(mt_tank.transform.GetChild(0).gameObject);


            }
            
            
            
            
        }

        


        has_start = true;





    }

    // Update is called once per frame
    void Update()
    {
        


    }

   

}
