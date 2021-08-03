using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building_Script : MonoBehaviour
{
    public GameObject Buiding1;

    public GameObject moniBuiding1;

    private bool take = false;
    
    //注意给layer层建筑设置ignore ray，不然建筑会向上飘

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (take == true)
        {
            tas();

        }
    }
    public void instanceBuiding()
    {
        print("出现建筑！");

        take = true;
        moniBuiding1.SetActive(true);
       
    }
    public void tas()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))        //最大长度设置为100f
        {
            moniBuiding1.transform.position = hit.point;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(Buiding1, hit.point, Quaternion.identity);
                take = false;
            }
        }

        

    }
}
