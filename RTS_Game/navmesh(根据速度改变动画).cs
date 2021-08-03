using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Soldier_Control : MonoBehaviour
{

    public Animator anim;
    private NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        var tu = nav.velocity.magnitude;
        print(tu);
        if (tu >= 0.9)
        {
            anim.SetBool("isrun", true);
        }
        else
        {
            anim.SetBool("isrun", false);
        }


    }
}
