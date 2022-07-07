using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Sirenix.OdinInspector;

public class Lot_Eyes : MonoBehaviour
{
    //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
    //￥￥￥￥￥￥￥￥￥￥手机脚本
    [BoxGroup("Camera")]
    //摄像机啊
    public GameObject Camearmain;
    [BoxGroup("Camera")]
    //摄像机距离
    public float distance = 10.0f;
    //缩放系数
    [BoxGroup("Camera")]
    public float scaleFactor = 1f;
    [BoxGroup("Camera")]
    public float maxDistance = 14f;
    [BoxGroup("Camera")]
    public float minDistance = 2f;


    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
    private Vector2 oldPosition1;
    private Vector2 oldPosition2;


    private Vector2 lastSingleTouchPosition;

    private Vector3 m_CameraOffset;
    private Camera m_Camera;


    //定义摄像机可以活动的范围
    [BoxGroup("Camera_Range")]
    public float xMin = -300;
    [BoxGroup("Camera_Range")]
    public float xMax = 300;
    [BoxGroup("Camera_Range")]
    public float zMin = -300;
    [BoxGroup("Camera_Range")]
    public float zMax = 300;

    //这个变量用来记录单指双指的变换
    private bool m_IsSingleFinger;

    //public RectTransform selecting_img;
    //catch_point
    //public GameObject catch_point;

    [BoxGroup("status"),ReadOnly]
    //判断是否为框选状态
    public bool isSelecting = false;
    [BoxGroup("status"),ReadOnly]
    //屏幕位置是拖动还是点击
    public bool isClick = true;

    //是否点击在Ui上
    //public bool isInUi = false;


    //##############################
    //############################\
    [BoxGroup("list"),ReadOnly]
    //被选中的物体
    public List<GameObject> Selected_Obj = new List<GameObject>();
    [BoxGroup("list")]
    public List<GameObject> register_List = new List<GameObject>();
    //所有的物体生成在game'units下，其子物体进行注册，用来判断其是否在框选范围内

    //弹出的面板
    //public List<GameObject> gameobj_info_panel = new List<GameObject>();

    [BoxGroup("UEsystem")]
    //箭头
    public GameObject click_arrow;
    [BoxGroup("UEsystem")]
    //攻击箭头
    public GameObject attack_arrow;


    [BoxGroup("GL")]
    //GL***********************************
    public Material rectMat = null;//这里使用Sprite下的defaultshader的材质即可
    [BoxGroup("GL")]
    public Color rectColor = Color.green;
    private bool drawRectangle = false;//是否开始画线标志


    void Start()
    {

        //*********************************************************************
        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

        m_Camera = Camearmain.GetComponent<Camera>();
        m_CameraOffset = m_Camera.transform.position;

        //**********************GL
        rectMat.hideFlags = HideFlags.HideAndDontSave;

        rectMat.shader.hideFlags = HideFlags.HideAndDontSave;//不显示在hierarchy面板中的组合，不保存到场景并且卸载Resources.UnloadUnusedAssets不卸载的对象。



    }

 

    //画图函数
    void OnPostRender()
    {//画线这种操作推荐在OnPostRender（）里进行 而不是直接放在Update，所以需要标志来开启

        if (drawRectangle==true)
        {

            Vector3 end = Input.mousePosition;//鼠标当前位置

            GL.PushMatrix();//保存摄像机变换矩阵,把投影视图矩阵和模型视图矩阵压入堆栈保存

            if (!rectMat)

                return;

            rectMat.SetPass(0);//为渲染激活给定的pass。

            GL.LoadPixelMatrix();//设置用屏幕坐标绘图

            GL.Begin(GL.QUADS);//开始绘制矩形

            GL.Color(new Color(rectColor.r, rectColor.g, rectColor.b, 0.1f));//设置颜色和透明度，方框内部透明

            //绘制顶点
            GL.Vertex3(lastSingleTouchPosition.x, lastSingleTouchPosition.y, 0);

            GL.Vertex3(Input.GetTouch(0).position.x, lastSingleTouchPosition.y, 0);

            GL.Vertex3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);

            GL.Vertex3(lastSingleTouchPosition.x, Input.GetTouch(0).position.y, 0);

            GL.End();


            GL.Begin(GL.LINES);//开始绘制线

            GL.Color(rectColor);//设置方框的边框颜色 边框不透明

            GL.Vertex3(lastSingleTouchPosition.x, lastSingleTouchPosition.y, 0);

            GL.Vertex3(Input.GetTouch(0).position.x, lastSingleTouchPosition.y, 0);

            GL.Vertex3(Input.GetTouch(0).position.x, lastSingleTouchPosition.y, 0);

            GL.Vertex3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);

            GL.Vertex3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0);

            GL.Vertex3(lastSingleTouchPosition.x, Input.GetTouch(0).position.y, 0);

            GL.Vertex3(lastSingleTouchPosition.x, Input.GetTouch(0).position.y, 0);

            GL.Vertex3(lastSingleTouchPosition.x, lastSingleTouchPosition.y, 0);

            GL.End();

            GL.PopMatrix();//恢复摄像机投影矩阵

        }

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
                    //判断是否点击到Ui以上，双端支持
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {
                        //TODO：点击在了UI上                     
                    }
                    else
                    {
                        MoveCamera(Input.GetTouch(0).position);
                    }

                    isClick = false;
                    //print("拖动中,设置不可点击" +isClick);

                    



                }
                m_IsSingleFinger = true;
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    //判断是否点击到Ui以上，双端支持
                    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    {
                        //TODO：点击在了UI上                     
                    }
                    else
                    {
                        if (isClick == true)
                        {

                            //发出射线，然后判断点击的什么
                            //判断点击的地面或者是其他

                            var layerMaks = 9 << 10;
                            RaycastHit hit_target;

                            Ray ray_target = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                            if (Physics.Raycast(ray_target, out hit_target, 100f))
                            {
                                //点击的是地面还是物体
                                if (hit_target.collider.gameObject.tag == "ground")
                                {


                                }


                            }

                        }
                        isClick = true;

                    }









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
        else if (isSelecting == true)
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
                    //开始绘制
                    drawRectangle = true;

                }
                m_IsSingleFinger = true;

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {

                    drawRectangle = false;
                    Endid_Selecting();

                    //先让之前选中的列表清空一遍,之前的红框物体也取消选中状态
                    foreach (GameObject y in Selected_Obj)
                    {

                        y.GetComponent<Lot_SodierBase>().YuanHuan_Hiden();

                    }
                    Selected_Obj.Clear();
                    FrameSelect();
                    //显示圆环
                    foreach (var i in Selected_Obj)
                    {
                        i.GetComponent<Lot_SodierBase>().YuanHuan_Show();

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
            try
            {
                Vector3 location = Camera.main.WorldToScreenPoint(item.transform.position);
                if (location.x < p1.x || location.x > p2.x || location.y < p1.y || location.y > p2.y
                    || location.z < Camera.main.nearClipPlane || location.z > Camera.main.farClipPlane)
                {
                    ;
                }
                else
                {
                    print("ADDDD+" + location);
                    Selected_Obj.Add(item);


                }

            }
            catch
            {

            }


        }


    }

    public void Endid_Selecting()
    {
        //结束框选状态


        isSelecting = false;



    }
 

    //这里为按钮入口函数
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
