 public GameObject targetPos;

    private float _lineHeight;//抛物线高度
    public LineRenderer lineRender;



    private void Start()
    {
        float Dis = Vector3.Distance(transform.position, targetPos.transform.position);

        _lineHeight = Dis / 2;//随距离变化曲率

        /*绘制抛物线*/
        var bezierControlPoint = (transform.position + targetPos.transform.position) * 0.5f + (Vector3.up * _lineHeight);

        int resolution = 50;//曲线上的路径点数量，值越大，取得的路径点越多，曲线越平滑

        var _path = new Vector3[resolution];
        for (int i = 0; i < resolution; i++)
        {
            var t = (i + 1) / (float)resolution;//归化到0~1范围
            _path[i] = GetBezierPoint(t, transform.position, bezierControlPoint, targetPos.transform.position);//使用贝塞尔曲线的公式取得t时的路径点
        }
        lineRender.positionCount = _path.Length;
        lineRender.SetPositions(_path);


        transform.position = transform.position;
        transform.DOPath(_path, 6f).SetEase(Ease.OutSine).OnComplete(MoveCallback);
      
    }


    /// <param name="t">0到1的值，0获取曲线的起点，1获得曲线的终点</param>
    /// <param name="start">曲线的起始位置</param>
    /// <param name="center">决定曲线形状的控制点</param>
    /// <param name="end">曲线的终点</param>
    public static Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    }
    void MoveCallback(){
    //结束后的事件
    }
