using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class MT_TANK : MonoBehaviourPunCallbacks
{
    private Identity.identity_ my_ideneity;

    public GameObject color;

    public Material red;
    public Material yellow;
    public Material blue;
    public Material green;

    public GameObject Paota;

    public GameObject returnPositon;

    public bool isAim=false;


    void Start()
    {
        
        StartCoroutine(keep_identity());
    }

    // Update is called once per frame
    void Update()
    {

        Quaternion shotRotation = Quaternion.LookRotation(returnPositon.transform.position - Paota.transform.position);
        Paota.transform.rotation = Quaternion.Lerp(Paota.transform.rotation, shotRotation, 0.5f * Time.deltaTime);
        Paota.transform.localEulerAngles = new Vector3(0, Paota.transform.localEulerAngles.y, 0);







    }
    public void Takemember_Go(Vector3 point)
    {
        

        GetComponent<NavMeshAgent>().SetDestination(point);

    }

    IEnumerator keep_identity()
    {

        yield return new WaitForSeconds(0.5f);
        my_ideneity = GetComponent<Django_UseType>().identity.obj_identity;

        //判断自己identy是否和当前玩家一直一致
        if (my_ideneity == Identity.identity_.red)
        {

            color.GetComponent<Renderer>().material = red;
        }
        else if (my_ideneity == Identity.identity_.yellow)
        {
            print("1");
            color.GetComponent<Renderer>().material = yellow;
        }
        else if (my_ideneity == Identity.identity_.blue)
        {


        }
        else if (my_ideneity == Identity.identity_.green)
        {


        }

    }


}
