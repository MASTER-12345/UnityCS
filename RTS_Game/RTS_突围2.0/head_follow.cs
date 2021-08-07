using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class head_follow : MonoBehaviour
{
    public GameObject target;

    public Sprite grennlight;

    public Sprite green_black;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position+new Vector3(0,3,0));
    }

    public void changeimg()
    {
        print("ÇÐ»¨Í¼Æ¬");
        transform.GetComponent<Image>().sprite = grennlight;

    }
    public void return_black()
    {
        print("ÇÐ»¨Í¼Æ¬");
        transform.GetComponent<Image>().sprite = green_black;

    }
}

