using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//这是C#脚本

public class Django_EyesControler : MonoBehaviour //Move_Scal是脚本的名字，要改成一致的
{
    //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    //￥￥￥￥￥￥￥￥￥￥手机脚本

    //摄像机啊
    public GameObject Camearmain;

    //摄像机距离
    public float distance = 10.0f;
    //缩放系数
    public float scaleFactor = 1f;
    public float maxDistance = 100f;
    public float minDistance = 2f;


    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;


    private Vector2 lastSingleTouchPosition;

    private Vector3 m_CameraOffset;
    private Camera m_Camera;

    public bool useMouse = true;

    //定义摄像机可以活动的范围
    public float xMin = -300;
    public float xMax = 300;
    public float zMin = -300;
    public float zMax = 300;

    //这个变量用来记录单指双指的变换
    private bool m_IsSingleFinger;
    
    public RectTransform selecting_img;



    //catch_point
    public GameObject catch_point;
    //判断是否为框选状态
    public bool isSelecting=false;

    //屏幕位置是拖动还是点击
    public bool isClick = true;
    
    
    //##############################
    //############################
    //被选中的物体
    public List<GameObject> Selected_Obj=new List<GameObject>();

    public List<GameObject> register_List=new List<GameObject>();
    //所有的物体生成在game'units下，其子物体进行注册，用来判断其是否在框选范围内
    


    void Start()
    {
      
        //*********************************************************************
        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

        m_Camera = Camearmain.GetComponent<Camera>();
        m_CameraOffset = m_Camera.transform.position;
        
        


    }

  

    void Update()
    {
  
          if (isSelecting == false)
            {
                if (Input.touchCount == 1)
                {
                   
                    if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
                    {
                        //在开始触摸或者从两字手指放开回来的时候记录一下触摸的位置
                        lastSingleTouchPosition = Input.GetTouch(0).position;
                      
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        //判断这里是否被触发过，如果是，就为拖动，不是则为点击
                       
                        MoveCamera(Input.GetTouch(0).position);
                        isClick = false;
                        //print("拖动中,设置不可点击" +isClick);

                    }
                    m_IsSingleFinger = true;
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {
                        print("判断是否为点击" + isClick);
                        if (isClick == true)
                        {
                            
                            //发出射线，然后判断点击的什么
                            //判断点击的地面或者是其他
                            RaycastHit hit_target;

                            Ray ray_target = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                            if (Physics.Raycast(ray_target, out hit_target, 100f))
                            {
                                
                                //先判断是不是地面,只有地面和非地面
                                bool isgrounded = hit_target.collider.gameObject.GetComponent<Django_UseType>().agent
                                    .isGround;

                                if (isgrounded == true)
                                {
                                  
                                    foreach (GameObject i in Selected_Obj)
                                    {
                                    //判断是否是代理
                                   
                                        bool isagent = i.GetComponent<Django_UseType>().agent.agent;
                                  
                                

                                    if (isagent == false)
                                    {
                                        i.SendMessage("Takemember_Go",hit_target.point);
                                    }
                                    else
                                    {
                                        i.transform.GetChild(0).gameObject.SendMessage("Takemember_Go",hit_target.point);

                                    }

                                        
                                    }
                                    
                                    
                                }


                            }

                        }
                        isClick = true;


                    }
                  
                    

                }
                else if (Input.touchCount > 1)
                {
                    //当从单指触摸进入多指触摸的时候,记录一下触摸的位置
                    //保证计算缩放都是从两指手指触碰开始的
                    if (m_IsSingleFinger)
                    {
                        oldPosition1 = Input.GetTouch(0).position;
                        oldPosition2 = Input.GetTouch(1).position;
                    }

                    if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                    {
                        ScaleCamera();
                        isClick = false;

                    }
                    isClick = false;

                    m_IsSingleFinger = false;
                }

            }
          else if(isSelecting==true)
            {
                //此时为框选模式，选择士兵
                if (Input.touchCount == 1)
                {
                         
                   

                    if (Input.GetTouch(0).phase == TouchPhase.Began || !m_IsSingleFinger)
                    {
                        //在开始触摸或者从两字手指放开回来的时候记录一下触摸的位置
                        lastSingleTouchPosition = Input.GetTouch(0).position;
                        
                        
                        
                      
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                      
                        DrawSelectFrame();
                        selecting_img.gameObject.SetActive(true);

                    }
                    m_IsSingleFinger = true;
                
                    if (Input.GetTouch(0).phase == TouchPhase.Ended)
                    {

                              selecting_img.gameObject.SetActive(false);
                              Endid_Selecting();
                             
                              
                             

                           
                              //先让之前选中的列表清空一遍,之前的红框物体也取消选中状态
                              foreach (GameObject y in Selected_Obj)
                              {
                                  bool isagent = y.GetComponent<Django_UseType>().agent.isAgent;
                                  if (isagent == false)
                                  {
                                      print("不是代理，直接取消");
                                      y.transform.GetChild(0).gameObject.SetActive(false);
                                  }
                                  else
                                  {
                                      print("是代理，取消字迹");
                                      y.GetComponent<Django_UseType>().agent.agent.transform.GetChild(0).gameObject.SetActive(false);
                                  }
                                  
                                  
                              }
                              Selected_Obj.Clear();
                              FrameSelect();

                              foreach (var i in Selected_Obj)
                              {
                                  print("选中的物体有"+i);
                                  bool isagent = i.GetComponent<Django_UseType>().agent.isAgent;
                                  if (isagent == false)
                                  {
                                      //如果不是代理，则是自己，如果是代理，寻找代理目标
                                      i.transform.GetChild(0).gameObject.SetActive(true);
                                  }
                                  else
                                  {
                                      print("目标圆环"+i.GetComponent<Django_UseType>().agent.agent.transform.GetChild(0).gameObject.name);
                                      i.GetComponent<Django_UseType>().agent.agent.transform.GetChild(0).gameObject.SetActive(true);
                                  }
                                  
                              }
                              
                      
                      





                    }
                    
                    

                }
                
                
            }
           
        
            
            
            
    }
    
    
    void FrameSelect()
    {

        Vector3 start = lastSingleTouchPosition;
        Vector3 end = Input.GetTouch(0).position;
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
        foreach (GameObject item in register_List)
        {
            
            
            Vector3 location = Camera.main.WorldToScreenPoint(item.transform.position);
            if (location.x < p1.x || location.x > p2.x || location.y < p1.y || location.y > p2.y
                || location.z < Camera.main.nearClipPlane || location.z > Camera.main.farClipPlane)
            {
                ;
            }
            else
            {
               print("ADDDD+"+location);
                Selected_Obj.Add(item);


            }
        }


    }

    public void Endid_Selecting()
    {
        //结束框选状态
        
  
        isSelecting = false;
      


    }
    void DrawSelectFrame()
    {
        Vector2 boxStart = lastSingleTouchPosition;
        Vector2 boxEnd = Input.GetTouch(0).position;

        Vector2 boxCenter = (boxStart + boxEnd) / 2;
        selecting_img.position = boxCenter;
        selecting_img.sizeDelta = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
    }



    public void change_bool_IsSelectng()
    {
        StartCoroutine(change_bool_isselecting2());
    }

    IEnumerator change_bool_isselecting2()
    {
        yield return new WaitForSeconds(0.1f);
        isSelecting = true;

    }
    private void MoveCamera(Vector3 scenePos)
    {
        Vector3 lastTouchPostion = m_Camera.ScreenToWorldPoint(new Vector3(lastSingleTouchPosition.x, lastSingleTouchPosition.y, -1));
        Vector3 currentTouchPosition = m_Camera.ScreenToWorldPoint(new Vector3(scenePos.x, scenePos.y, -1));

        Vector3 v = currentTouchPosition - lastTouchPostion;
        m_CameraOffset += new Vector3(v.x, 0, v.z) * m_Camera.transform.position.y;

        //把摄像机的位置控制在范围内
        m_CameraOffset = new Vector3(Mathf.Clamp(m_CameraOffset.x, xMin, xMax), m_CameraOffset.y, Mathf.Clamp(m_CameraOffset.z, zMin, zMax));
        //Debug.Log(lastTouchPostion + "|" + currentTouchPosition + "|" + v);
        lastSingleTouchPosition = scenePos;
    }
    
    private void ScaleCamera()
    {
        //计算出当前两点触摸点的位置
        var tempPosition1 = Input.GetTouch(0).position;
        var tempPosition2 = Input.GetTouch(1).position;


        float currentTouchDistance = Vector3.Distance(tempPosition1, tempPosition2);
        float lastTouchDistance = Vector3.Distance(oldPosition1, oldPosition2);

        //计算上次和这次双指触摸之间的距离差距
        //然后去更改摄像机的距离
        distance -= (currentTouchDistance - lastTouchDistance) * scaleFactor * Time.deltaTime;


        //把距离限制住在min和max之间
        distance = Mathf.Clamp(distance, minDistance, maxDistance);


        //备份上一次触摸点的位置，用于对比
        oldPosition1 = tempPosition1;
        oldPosition2 = tempPosition2;
    }
    
    //Update方法一旦调用结束以后进入这里算出重置摄像机的位置
    private void LateUpdate()
    {
        var position = m_CameraOffset + m_Camera.transform.forward * -distance;
        m_Camera.transform.position = position;
    }
}

