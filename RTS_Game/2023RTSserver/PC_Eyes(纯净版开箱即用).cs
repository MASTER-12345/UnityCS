using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;





public class PC_Eyes : MonoBehaviour
{
    [ReadOnly,BoxGroup("CameraPara")]
    public float HorSpeed = 30f;//移动速度
    [ReadOnly, BoxGroup("CameraPara")]
    public float VerSpeed = 30f;//垂直移速
    [ReadOnly, BoxGroup("CameraPara")]
    public int scrollspeed;
    private Transform m_transform;
    private Vector2 lastSingleTouchPosition;
    [ReadOnly, BoxGroup("CameraPara")]
    public bool lock_camera;
    [ReadOnly,BoxGroup("CameraPara")]
    public float max_height;
    [ReadOnly,BoxGroup("CameraPara")]
    public float min_height;
    [ReadOnly,BoxGroup("CameraPara")]
    public bool building_mode;

    [ReadOnly,BoxGroup("UnitManager")]
    public List<GameObject> register_list;
    [ReadOnly,BoxGroup("UnitManager")]
    public List<GameObject> selected_list;



    [BoxGroup("GL"),ReadOnly]
    public Color rectColor = Color.green;
    [BoxGroup("GL"), ReadOnly]
    public Vector3 start = Vector3.zero;//记下鼠标按下位置
    [BoxGroup("GL")]
    public Material rectMat = null;//这里使用Sprite下的defaultshader的材质即可
    [BoxGroup("GL"), ReadOnly]
    public bool drawRectangle = false;//是否开始画线标志




    void Move_Eyes()
    {
        Vector3 v1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 dir_forward = m_transform.forward;
        Vector3 dir_right = m_transform.right;

        //消除高度变化
        dir_right.y = 0;
        dir_forward.y = 0;
        dir_forward = dir_forward.normalized;
        dir_right = dir_right.normalized;

        if (v1.x < 0.05f)
        {
            m_transform.Translate(-dir_right * HorSpeed * Time.deltaTime, Space.World);
        }
        if (v1.x > 1 - 0.05f)
        {
            m_transform.Translate(dir_right * HorSpeed * Time.deltaTime, Space.World);
        }

        if (v1.y < 0.05f)
        {
            m_transform.Translate(-dir_forward * VerSpeed * Time.deltaTime, Space.World);
        }
        if (v1.y > 1 - 0.05f)
        {
            m_transform.Translate(dir_forward * VerSpeed * Time.deltaTime, Space.World);
        }


        //Horizontal、Vertical
        if (Input.GetAxis("Vertical") != 0)
        {
            m_transform.Translate(dir_forward * VerSpeed * Input.GetAxis("Vertical") * Time.deltaTime, Space.World);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            m_transform.Translate(dir_right * HorSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, Space.World);
        }


    }
    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (transform.position.y >= min_height)
            {
                transform.position = transform.position - new Vector3(0, scrollspeed, 0);
            }


        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (transform.position.y <= max_height)
            {
                transform.position = transform.position + new Vector3(0, scrollspeed, 0);
            }


        }


    }
    void Start()
    {
        Init();
        Init_GL();
    }

    private void Init()
    {
        m_transform = Camera.main.transform;

        scrollspeed = 7;
        HorSpeed = 30f;
        VerSpeed = 30f;
        lock_camera = false;

        max_height = 150;
        min_height = 100;
    }

    private void Init_GL()
    {
        rectMat.hideFlags = HideFlags.HideAndDontSave;
        rectMat.shader.hideFlags = HideFlags.HideAndDontSave;//不显示在hierarchy面板中的组合，不保存到场景并且卸载Resources.UnloadUnusedAssets不卸载的对象。

    }

    void Update()
    {
        OnPostRender();
       
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (lock_camera == false)
            {
                lock_camera = true;
            }
            else
            {
                lock_camera = false;
            }
        }
        if (lock_camera == false)
        {
            Zoom();
            Move_Eyes();
        }


        if (building_mode == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //在开始触摸或者从两字手指放开回来的时候记录一下触摸的位置
                lastSingleTouchPosition = Input.mousePosition;

                drawRectangle = true;//如果鼠标左键按下 设置开始画线标志
                start = Input.mousePosition;//记录按下位置

            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {

                drawRectangle = false;//如果鼠标左键放开 结束画线
                FrameSelect();

            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //DrawSelectFrame();
            }
            //#############################

            if (Input.GetKeyDown(KeyCode.Mouse1) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                try
                {
                    RaycastHit hit_target;

                    Ray ray_target = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray_target, out hit_target, 10000f))
                    {

                        if (hit_target.collider.gameObject.tag == "ground")
                        {
                        

                        }
                        else
                        {
                    
                        }


                    }
                }
                catch
                {

                }




            }

        }
        else
        {
            //进入放置模式
            //左键放置物体
            if (Input.GetKeyDown(KeyCode.Mouse0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                //射线
                RaycastHit hit_target;
                Ray ray_target = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray_target, out hit_target, 10000f))
                {
                 

                }

            }


        }

    }

    void OnPostRender()
    {//画线这种操作推荐在OnPostRender（）里进行 而不是直接放在Update，所以需要标志来开启

        if (drawRectangle)
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
            GL.Vertex3(start.x, start.y, 0);

            GL.Vertex3(end.x, start.y, 0);

            GL.Vertex3(end.x, end.y, 0);

            GL.Vertex3(start.x, end.y, 0);

            GL.End();


            GL.Begin(GL.LINES);//开始绘制线

            GL.Color(rectColor);//设置方框的边框颜色 边框不透明

            GL.Vertex3(start.x, start.y, 0);

            GL.Vertex3(end.x, start.y, 0);

            GL.Vertex3(end.x, start.y, 0);

            GL.Vertex3(end.x, end.y, 0);

            GL.Vertex3(end.x, end.y, 0);

            GL.Vertex3(start.x, end.y, 0);

            GL.Vertex3(start.x, end.y, 0);

            GL.Vertex3(start.x, start.y, 0);

            GL.End();

            GL.PopMatrix();//恢复摄像机投影矩阵

        }

    }


    void FrameSelect()
    {
        try
        {
            Vector3 start = lastSingleTouchPosition;
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
            foreach (GameObject item in register_list)
            {
                //print(item);
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
         
                        selected_list.Add(item);

                    }

                }
                catch
                {

                }


            }

        }
        catch
        {

        }




    }

   
}
