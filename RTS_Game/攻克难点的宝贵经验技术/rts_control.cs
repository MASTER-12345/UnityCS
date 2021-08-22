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
        //������������Ҫ������ui���ᱻ���ߴ�͸�����ߵ��uiʱ������������
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
            print("��ǰ����"+dis);
            yield return new WaitForSeconds(0.05f);
            if (dis >= 7)
            {
                nav.SetDestination(enemy.transform.position);
            }
            else
            {

             

                    print("�������");
                    nav.enabled = false;

                break;
                
                
                
            }
        }

        
    }

    public void sod()
    {
        print("�ұ�����ˣ�Ȼ������δ������δ�ƶ���");

    }
}
