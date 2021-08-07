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

    //选中的物体没用后清除圆环列表
    private List<GameObject> yuanHuanlist = new List<GameObject>();

    //图标
    private List<GameObject> imgsprit_list = new List<GameObject>();

    //点击后清除圆环列表
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

            //清除点击的圆环
            foreach(GameObject cli in clickYuanHuan)
            {
                cli.transform.GetChild(0).gameObject.SetActive(false);
                
            }
            


            //获得对方标志obj_type
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))        //最大长度设置为100f
            {
                //print("UITAG" + hit.collider.name);
                ////如果点击的是ui，则返回,阻挡射线
                //if (hit.collider.tag == "cant")
                //{
                //    return;

                //}



                //获取值
                try
            {
                print(hit.collider.gameObject.GetComponent<UseType>().obj_type);
                GameObject budingss = hit.collider.gameObject;
                //如果是建筑类,显示红圈
                if (hit.collider.gameObject.GetComponent<UseType>().obj_type == UseType.object_type.buildings)
                {
                    budingss.transform.GetChild(0).gameObject.SetActive(true);
                    clickYuanHuan.Add(budingss);
                    //如果是自己的
                    if (budingss.GetComponent<UseType>().group == UseType.Group_type.selfs)
                    {
                        menu.SetActive(true);
                    }


                }
            }
            catch
            {
                print("点击的没有挂上usetype，获取失败,属于正常现象");
            }
               

                

                
                print("点击的tag是" + hit.collider.tag);
                if (hit.collider.tag == "ground")
                {
                    print("消失");
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
            //刷新列表里的队长
            selectList.Clear();
            FrameSelect();
            image.gameObject.SetActive(false);

            //清除掉圆环
            foreach(GameObject yuanhuan in yuanHuanlist)
            {
                yuanhuan.SetActive(false);
            }
            yuanHuanlist.Clear();

            //图标变成灰色
            foreach(GameObject sprit in imgsprit_list)
            {
                sprit.GetComponent<head_follow>().return_black();
            }



            //遍历队长们的子物体,并且显示圆环
            foreach(GameObject gi in selectList)
            {
                print("他的孩子");
                print(gi.transform.GetChild(2));
                yuanHuanlist.Add(gi.transform.GetChild(2).gameObject);
                gi.transform.GetChild(2).gameObject.SetActive(true);

                //切换图片
                gi.transform.GetChild(3).GetChild(0).GetComponent<head_follow>().changeimg();

                imgsprit_list.Add(gi.transform.GetChild(3).GetChild(0).gameObject);

            }
            

        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            

            //判断点击的是什么
            RaycastHit hit_targt;
            Ray ray_target = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray_target, out hit_targt, 100f))        //最大长度设置为100f
            {
                if (hit_targt.collider.tag == "enemy")
                {
                    print("点击的敌人");
                    foreach(GameObject gi in selectList)
                    {
                       //遍历队长们

                        team_Manager te = gi.GetComponent<team_Manager>();
                        te.takemember_attack(hit_targt.collider.gameObject);

                        //停止导航并且转向目标
                        //gi.GetComponent<NavMeshAgent>().isStopped = true;
                        gi.GetComponent<NavMeshAgent>().enabled = false;
                        gi.transform.LookAt(hit_targt.collider.gameObject.transform.position);
                        //播放动画
                        gi.GetComponent<Sodier_Manager>().playGun(hit_targt.collider.gameObject);

                    }


                }else if (hit_targt.collider.tag == "ground")
                {
                    foreach (GameObject gi in selectList)
                    {
                        //移动到目标位置
                        NavMeshAgent agent = gi.GetComponent<NavMeshAgent>();
                        //将屏幕上的点转换为世界坐标，我用涉县吧
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, 100f))        //最大长度设置为100f
                        {
                            try
                            {
                                agent.SetDestination(hit.point);
                            }
                            catch
                            {
                                print("此时处理问题，正在重新制定新路线");
                            }
                            
                            //调用team——manager函数
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

    public List<GameObject> gameObjectList; //声明为public，Unity引擎会自动实例化，以便于在监视面板使用，懂了

    private List<GameObject> selectList = new List<GameObject>(); //必须实例化才可以使用

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