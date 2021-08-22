using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
public class rts_control : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent nav;

    public GameObject enemy;

    public bool enough;

    public bool canmove;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {


        // && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()
        //这句代码至关重要，代表ui不会被射线穿透，或者点击ui时，不会有射线
        if (Input.GetKeyDown(KeyCode.Mouse1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            nav.enabled = true;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {



                if (hit.collider.tag == "ground")
                {

                    nav.SetDestination(hit.point);
                }
                else if (hit.collider.tag == "enemy")
                {
                    var dis = Vector3.Distance(transform.position, enemy.transform.position);
                    print(dis);
                    if (dis >= 7)
                    {
                        enough = false;
                        StartCoroutine(dopan());

                    }
                    else
                    {
                        nav.enabled = false;

                    }
                }


            }



        }



    }

    IEnumerator dopan()
    {
       
        while (true)
        {
            var dis = Vector3.Distance(transform.position, enemy.transform.position);
            print("当前距离"+dis);
            yield return new WaitForSeconds(0.05f);
            if (dis >= 7)
            {
                nav.SetDestination(enemy.transform.position);
            }
            else
            {

             

                    print("距离进了");
                    nav.enabled = false;

                break;
                
                
                
            }
        }

        
    }

    public void sod()
    {
        print("我被点击了，然后射线未发出，未移动着");

    }
}
