public LayerMask mask;//序列化后选中的层才会被射线触发

Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
RaycastHit2D hit2D = Physics2D.Raycast(screenPos, Vector2.zero,1000,mask);
if (hit2D)
{
    print(hit2D.collider.name);
    if (hit2D.collider.gameObject.tag == "Counter")
    {
        mapm.GetComponent<GameinUiManager>().CounterDetailPanel.SetActive(true);
        Now_Counter = hit2D.collider.gameObject;
    }
}

