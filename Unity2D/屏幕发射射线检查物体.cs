

  if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
 
            RaycastHit2D hit2D = Physics2D.Raycast(screenPos, Vector2.zero);
 
            if (hit2D)
            {
                print(hit2D.collider.name);
            }
            else
            {
                print("no");
            }
        }

————————————————
版权声明：本文为CSDN博主「煮粥侠_99」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/yjy99yjy999/article/details/121044194
