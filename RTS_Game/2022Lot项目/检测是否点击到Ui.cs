
//第一种方法，最简单实用
if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
{
    //TODO：点击在了UI上
}

//第二种方法,通过ui发送射线检测

public static bool IsPointerOverUIObject() 
{
    PointerEventData eventData = new PointerEventData(EventSystem.current);
    eventData.position = Input.mousePosition;

    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventData, results);
    return results.Count > 0;
}
public bool IsPointerOverUIObject(Vector2 screenPosition)
{
    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    return results.Count > 0;
}


//第三种方法通过画布上的GraphicRaycaster组件发射射线进行检测

public bool IsPointerOverUIObject(Canvas canvas, Vector2 screenPosition) 
{
    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    eventDataCurrentPosition.position = screenPosition;
    GraphicRaycaster uiRaycaster = canvas.gameObject.GetComponent<GraphicRaycaster>();
    
    List<RaycastResult> results = new List<RaycastResult>();
    uiRaycaster.Raycast(eventDataCurrentPosition, results);
    return results.Count > 0;
}
-----------------------------------
©著作权归作者所有：来自51CTO博客作者mb60e5417d375b8的原创作品，请联系作者获取转载授权，否则将追究法律责任
Unity中解决IsPointerOverGameObject防UI穿透在移动端检测失败
https://blog.51cto.com/u_15296378/3017773

