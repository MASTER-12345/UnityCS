   
  //给Ui穿上指定tag，Ui射线(非物理射线)检测其中有没有带tag的ui，有就不能穿透，否则就穿透,返回bool值
   
   
   
   public bool GetFirstPickGameObject(Vector2 position)
    {
        int raycastUi_num = 0;

        EventSystem evetSystem = EventSystem.current;
        PointerEventData pointEventData = new PointerEventData(evetSystem);
        pointEventData.position = position;
        //射线检测Ui
        List<RaycastResult> uiRaycastResultCache = new List<RaycastResult>();
        evetSystem.RaycastAll(pointEventData, uiRaycastResultCache);
        
        if (uiRaycastResultCache.Count >= 1)
        {
           foreach(var i in uiRaycastResultCache)
            {
                if (i.gameObject.tag == "RaycastUi")
                {
                    //不能穿透
                    raycastUi_num = raycastUi_num + 1;

                }

            }

        }
        if (raycastUi_num > 0)
        {
            return false;

        }
        else
        {
            return true;
        }


    }
   
