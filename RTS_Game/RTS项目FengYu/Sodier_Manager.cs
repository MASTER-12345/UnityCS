using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Sodier_Manager : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator anim;
    public GameObject bullet;

    public GameObject firePos;

    private bool bulletmove = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var speed = GetComponent<NavMeshAgent>().velocity.magnitude;
        //print("当前我们的速度为" + speed);
        if (speed >= 0.3)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }




    }

    public void playGun(GameObject enemy)
    {
        anim.SetTrigger("shoot");



        var newbullet=Instantiate(bullet, firePos.transform.position, transform.rotation);




        //newbullet.transform.position = Vector3.MoveTowards(newbullet.transform.position, enemy.transform.position, Time.deltaTime * 100);


        anim.SetTrigger("reload");

    }



}
