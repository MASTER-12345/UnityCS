using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    Vector3 startPosition;
    public RectTransform image;

    public GameObject menu;

    //ѡ�е�����û�ú����Բ���б�
    private List<GameObject> yuanHuanlist = new List<GameObject>();

    //ͼ��
    private List<GameObject> imgsprit_list = new List<GameObject>();

    //��������Բ���б�
    private List<GameObject> clickYuanHuan = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        


        if (Input.GetMouseButtonDown(0))
        {


            startPosition = Input.mousePosition;
            image.gameObject.SetActive(true);

            //��������Բ��
            foreach(GameObject cli in clickYuanHuan)
            {
                cli.transform.GetChild(0).gameObject.SetActive(false);
                
            }
            


            //��öԷ���־obj_type
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))        //��󳤶�����Ϊ100f
            {
                //print("UITAG" + hit.collider.name);
                ////����������ui���򷵻�,�赲����
                //if (hit.collider.tag == "cant")
                //{
                //    return;

                //}



                //��ȡֵ
                try
            {
                print(hit.collider.gameObject.GetComponent<UseType>().obj_type);
                GameObject budingss = hit.collider.gameObject;
                //����ǽ�����,��ʾ��Ȧ
                if (hit.collider.gameObject.GetComponent<UseType>().obj_type == UseType.object_type.buildings)
                {
                    budingss.transform.GetChild(0).gameObject.SetActive(true);
                    clickYuanHuan.Add(budingss);
                    //������Լ���
                    if (budingss.GetComponent<UseType>().group == UseType.Group_type.selfs)
                    {
                        menu.SetActive(true);
                    }


                }
            }
            catch
            {
                print("�����û�й���usetype����ȡʧ��,������������");
            }
               

                

                
                print("�����tag��" + hit.collider.tag);
                if (hit.collider.tag == "ground")
                {
                    print("��ʧ");
                    menu.SetActive(false);
                }
               

            }



            }
        if (Input.GetMouseButton(0))
        {
            DrawSelectFrame();
        }
        if (Input.GetMouseButtonUp(0))
        {
            //ˢ���б���Ķӳ�
            selectList.Clear();
            FrameSelect();
            image.gameObject.SetActive(false);

            //�����Բ��
            foreach(GameObject yuanhuan in yuanHuanlist)
            {
                yuanhuan.SetActive(false);
            }
            yuanHuanlist.Clear();

            //ͼ���ɻ�ɫ
            foreach(GameObject sprit in imgsprit_list)
            {
                sprit.GetComponent<head_follow>().return_black();
            }



            //�����ӳ��ǵ�������,������ʾԲ��
            foreach(GameObject gi in selectList)
            {
                print("���ĺ���");
                print(gi.transform.GetChild(2));
                yuanHuanlist.Add(gi.transform.GetChild(2).gameObject);
                gi.transform.GetChild(2).gameObject.SetActive(true);

                //�л�ͼƬ
                gi.transform.GetChild(3).GetChild(0).GetComponent<head_follow>().changeimg();

                imgsprit_list.Add(gi.transform.GetChild(3).GetChild(0).gameObject);

            }
            

        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            

            //�жϵ������ʲô
            RaycastHit hit_targt;
            Ray ray_target = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray_target, out hit_targt, 100f))        //��󳤶�����Ϊ100f
            {
                if (hit_targt.collider.tag == "enemy")
                {
                    print("����ĵ���");
                    foreach(GameObject gi in selectList)
                    {
                       //�����ӳ���

                        team_Manager te = gi.GetComponent<team_Manager>();
                        te.takemember_attack(hit_targt.collider.gameObject);

                        //ֹͣ��������ת��Ŀ��
                        //gi.GetComponent<NavMeshAgent>().isStopped = true;
                        gi.GetComponent<NavMeshAgent>().enabled = false;
                        gi.transform.LookAt(hit_targt.collider.gameObject.transform.position);
                        //���Ŷ���
                        gi.GetComponent<Sodier_Manager>().playGun(hit_targt.collider.gameObject);

                    }


                }else if (hit_targt.collider.tag == "ground")
                {
                    foreach (GameObject gi in selectList)
                    {
                        //�ƶ���Ŀ��λ��
                        NavMeshAgent agent = gi.GetComponent<NavMeshAgent>();
                        //����Ļ�ϵĵ�ת��Ϊ�������꣬�������ذ�
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, 100f))        //��󳤶�����Ϊ100f
                        {
                            try
                            {
                                agent.SetDestination(hit.point);
                            }
                            catch
                            {
                                print("��ʱ�������⣬���������ƶ���·��");
                            }
                            
                            //����team����manager����
                            team_Manager te = gi.GetComponent<team_Manager>();
                            te.takemember(hit.point);



                        }





                    }


                }
            }
               
        }
    }

    void DrawSelectFrame()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = Input.mousePosition;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        image.position = boxCenter;
        image.sizeDelta = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
    }

    public List<GameObject> gameObjectList; //����Ϊpublic��Unity������Զ�ʵ�������Ա����ڼ������ʹ�ã�����

    private List<GameObject> selectList = new List<GameObject>(); //����ʵ�����ſ���ʹ��

    void FrameSelect()
    {
        Vector3 start = startPosition;
        Vector3 end = Input.mousePosition;
        Vector3 p1 = Vector3.zero;
        Vector3 p2 = Vector3.zero;
        if (start.x > end.x)
        {
            p1.x = end.x;
            p2.x = start.x;
        }
        else
        {
            p1.x = start.x;
            p2.x = end.x;
        }
        if (start.y > end.y)
        {
            p1.y = end.y;
            p2.y = start.y;
        }
        else
        {
            p1.y = start.y;
            p2.y = end.y;
        }
        foreach (GameObject item in gameObjectList)
        {
            Vector3 location = Camera.main.WorldToScreenPoint(item.transform.position);
            if (location.x < p1.x || location.x > p2.x || location.y < p1.y || location.y > p2.y
               || location.z < Camera.main.nearClipPlane || location.z > Camera.main.farClipPlane)
            {
                ;
            }
            else
            {
                Debug.Log(item);
                selectList.Add(item);


            }
        }
    }
}