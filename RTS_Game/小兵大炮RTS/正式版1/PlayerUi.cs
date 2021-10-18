using System;
using UnityEngine;
using UnityEngine.UI;


using System.Collections;
using Photon.Pun;


namespace Com.MyCompany.MyGame
{
    
    public class PlayerUI : MonoBehaviourPunCallbacks,IPunObservable
    {
        
        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;

        private PhotonView pv;
        [SerializeField] public int set_number;

   

        private GameObject position1;
        private GameObject positon2;
        private GameObject positon3;
        private GameObject positon4;


        [SerializeField] public bool did_ready;
        [SerializeField] public bool is_seted;

        public GameObject ready_light;

        public GameObject redlight;
        public GameObject greenlight;



        private GameObject ready_btn;

        public GameObject redbutton;
        public GameObject greenbutton;





        private void Start()
        {
            set_number = 0;
            did_ready = false;
            is_seted = false;
  
            pv = GetComponent<PhotonView>();
           
            position1 = GameObject.Find("1");
            positon2 = GameObject.Find("2");
            positon3 = GameObject.Find("3");
            positon4 = GameObject.Find("4");
       
            if (playerNameText != null)
            {
                playerNameText.text = pv.Owner.NickName;
            }

            ready_btn = GameObject.Find("ready");
        }

        private void Update()
        {
            
           
                if (set_number == 1)
                {
                    transform.SetParent(position1.transform,false);
                    transform.position = position1.transform.position;
                    is_seted = true;

                }
                else if(set_number==2)
                {
                    transform.SetParent(positon2.transform,false);
                    transform.position = positon2.transform.position;
                    is_seted = true;
                }
                else if(set_number==3)
                {
                    transform.SetParent(positon3.transform,false);
                    transform.position = positon3.transform.position;
                    is_seted = true;
                }
                else if(set_number==4)
                {
                    transform.SetParent(positon4.transform,false);
                    transform.position = positon4.transform.position;
                    is_seted = true;
                }

                if (did_ready == false)
                {
                    ready_light.GetComponent<Image>().sprite = redlight.GetComponent<SpriteRenderer>().sprite;
                         if (photonView.IsMine)
                        {
                         ready_btn.GetComponent<Image>().sprite = greenbutton.GetComponent<SpriteRenderer>().sprite;
                            ready_btn.transform.GetChild(0).GetComponent<Text>().text = "准备";

                        }
                   


            }
                else
                {
                    ready_light.GetComponent<Image>().sprite = greenlight.GetComponent<SpriteRenderer>().sprite;

                    if (photonView.IsMine)
                    {
                        ready_btn.GetComponent<Image>().sprite = redbutton.GetComponent<SpriteRenderer>().sprite;
                        ready_btn.transform.GetChild(0).GetComponent<Text>().text = "已准备";

                    }
                    
            }
            
          
        }

    

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(set_number);
                stream.SendNext(did_ready);
                stream.SendNext(is_seted);
          
                
            }
            else
            {
                this.set_number = (int)stream.ReceiveNext();
                this.did_ready = (bool)stream.ReceiveNext();
                this.is_seted = (bool) stream.ReceiveNext();

            }
            
            
            
        }
    }
}
