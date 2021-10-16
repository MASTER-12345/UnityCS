using System.Collections;
using System.Collections.Generic;
using Com.MyCompany.MyGame;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class Django_RoomManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject player_ui;
    
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    private PhotonView pv;
    private GameObject player_uii;

    public GameObject Map_now;

    public GameObject redlight;
    public GameObject greenlight;

    public GameObject ready_btn;

    public GameObject redbutton;
    public GameObject greenbutton;
    
    //
    public GameObject start_button;
    public GameObject change_map_button;
    
    //是否所有人已经准备
    private bool all_had_ready;
    
    
    //标记
    public GameObject identity;
    

   
    void Start()
    {
        player_uii = PhotonNetwork.Instantiate(this.player_ui.name, transform.position, Quaternion.identity);
        pv = GetComponent<PhotonView>();
        all_had_ready = true;
        
        //如果不是主机的话，禁用切换地图和开始按钮
        print(PhotonNetwork.MasterClient.NickName);
        if (PhotonNetwork.IsMasterClient == false)
        {
            start_button.GetComponent<Button>().interactable = false;
            change_map_button.GetComponent<Dropdown>().interactable = false;

        }
        //通过rpc将所有人自己的playerui刷新一遍
        photonView.RPC("resh", RpcTarget.All);

       
        

    }

    [PunRPC]

    void resh()
    {
        var playerui_list = GameObject.FindGameObjectsWithTag("playerui");
        foreach (GameObject i in playerui_list)
        {
            if (i.GetComponent<PhotonView>().IsMine == true)
            {
                //如果是自己的，那么didready切换
                i.GetComponent<PlayerUI>().did_ready = false;
                Debug.LogError("刷新完毕");


            }


        }
    }

    
    // Update is called once per frame
    void Update()
    {
        
        //监控四个座位的子物体，来判断是否有人占座
        if (player1.transform.childCount >= 2)
        {
            player1.GetComponent<Button>().interactable = false;
        }
        else
        {
            player1.GetComponent<Button>().interactable = true;
        }
        
        
        if (player2.transform.childCount >= 2)
        {
            player2.GetComponent<Button>().interactable = false;
        }
        else
        {
            player2.GetComponent<Button>().interactable = true;
        }
        
        if (player3.transform.childCount >= 2)
        {
            player3.GetComponent<Button>().interactable = false;
        }
        else
        {
            player3.GetComponent<Button>().interactable = true;
        }
        if (player4.transform.childCount >= 2)
        {
            player4.GetComponent<Button>().interactable = false;
        }
        else
        {
            player4.GetComponent<Button>().interactable = true;
        }



        
        
        
        
    }


    public void change_map_img()
    {
        print(Map_now.GetComponent<Text>().text.ToString());
        //改变对应的图片从resource中找
    }

    public void PlayerUi_ready_start()
    {
        
        
        
        //
        var playerui_list=GameObject.FindGameObjectsWithTag("playerui");
        foreach (GameObject i in playerui_list)
        {
            if (i.GetComponent<PhotonView>().IsMine == true)
            {
                
                //首先判断是否自己坐下在位置上，才能准备
                if (i.GetComponent<PlayerUI>().is_seted == true)
                {
                    
                    if (i.GetComponent<PlayerUI>().did_ready == false)
                    {
                        i.GetComponent<PlayerUI>().did_ready = true;
                        Transform chi1 = i.transform.GetChild(0);
                        GameObject ready_light = chi1.transform.GetChild(0).gameObject;
                        //ready_light.GetComponent<Image>().sprite = greenlight.GetComponent<SpriteRenderer>().sprite;
                        ready_btn.transform.GetChild(0).GetComponent<Text>().text = "已准备";
                        ready_btn.GetComponent<Image>().sprite = redbutton.GetComponent<SpriteRenderer>().sprite;



                        var Playerui_list = GameObject.FindGameObjectsWithTag("playerui");

                        //准备时候身份牌打好标记
                        foreach (GameObject y in Playerui_list)
                        {
                            if (y.GetComponent<PhotonView>().IsMine)
                            {

                                print("当前的身份为 " + y.transform.parent.gameObject.name);
                                string ide = y.transform.parent.gameObject.name;
                                if (ide == "1")
                                {
                                    identity.GetComponent<Identity>().my_identity = Identity.identity_.red;
                                }
                                else if (ide == "2")
                                {
                                    identity.GetComponent<Identity>().my_identity = Identity.identity_.yellow;
                                }
                                else if (ide == "3")
                                {
                                    identity.GetComponent<Identity>().my_identity = Identity.identity_.blue;

                                }
                                else if (ide == "4")
                                {
                                    identity.GetComponent<Identity>().my_identity = Identity.identity_.green;
                                }

                            }
                        }


                    }
                    else
                    {
                        i.GetComponent<PlayerUI>().did_ready = false;
                        Transform chi1 = i.transform.GetChild(0);
                        GameObject ready_light = chi1.transform.GetChild(0).gameObject;
                        //ready_light.GetComponent<Image>().sprite = redlight.GetComponent<SpriteRenderer>().sprite;
                        ready_btn.transform.GetChild(0).GetComponent<Text>().text = "准备";
                        ready_btn.GetComponent<Image>().sprite = greenbutton.GetComponent<SpriteRenderer>().sprite;
                    
                    }
                    
                    
                    
                }
                
              

            }
        }

    }
    
    public void pre_join_set(int number)
    {
        if (player_uii.GetComponent<PlayerUI>().did_ready == false)
        {
            player_uii.GetComponent<PlayerUI>().set_number = number;
            
            
        }
        
        

    }
    

    public void Leave_Room()
    {

        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }


    public void start_Game()
    {
        var Playerui_list = GameObject.FindGameObjectsWithTag("playerui");
        //检查如果有一个人是未准备状态，那么将所有人准备bool改成false
        foreach (GameObject i in Playerui_list)
        {
            if (i.GetComponent<PlayerUI>().did_ready == false)
            {
                all_had_ready = false;

            }
        }
       

        //
        if (all_had_ready == true)
        {
            //所有人已经准备完毕，检查人数是否不为基数,3
            if (Playerui_list.Length != 3)
            {

                
                photonView.RPC("Project_identity", RpcTarget.All);
                //加载场景
                PhotonNetwork.LoadLevel("LayDriver");
                
            }
            
        }
        

    }

    [PunRPC]
    
    void Project_identity()
    {
        DontDestroyOnLoad(identity);

    }
    
}
