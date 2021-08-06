using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head_follow : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position+new Vector3(0,3,0));
    }
}
