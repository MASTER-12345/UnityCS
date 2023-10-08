public Material mat;

//只能在此函数中使用，统一绘制，只有一个drawcall
public void OnRenderObject()
{
    GameObject p = GameObject.Find("Player");
    //激活第一个着色器通过（在本例中，我们知道它是唯一的通过）
    mat.SetPass(0);
    //渲染入栈  在Push——Pop之间写GL代码
    GL.PushMatrix();
    GL.Color(Color.red);
    // 开始画线  在Begin——End之间写画线方式
    GL.Begin(GL.LINES);

    GL.Vertex3(p.transform.position.x, p.transform.position.y, p.transform.position.z);
    Vector3 newpos = p.transform.position + new Vector3(7, -1, 0);
    GL.Vertex3(newpos.x, newpos.y, newpos.z);

    GL.End();
    //渲染出栈
    GL.PopMatrix();
}
