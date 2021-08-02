
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Control_System : MonoBehaviour
{
    Vector3 startPosition;
    public RectTransform image;






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



        }
        if (Input.GetMouseButton(0))
        {
            DrawSelectFrame();
        }
        if (Input.GetMouseButtonUp(0))
        {
            FrameSelect();
            image.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            foreach(GameObject gi in selectList)
            {
                //移动到目标位置
                NavMeshAgent agent = gi.GetComponent<NavMeshAgent>();
                //将屏幕上的点转换为世界坐标，我用涉县吧
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))        //最大长度设置为100f
                {
                    agent.SetDestination(hit.point);
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

    private List<GameObject> selectList=new List<GameObject>(); //必须实例化才可以使用

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
