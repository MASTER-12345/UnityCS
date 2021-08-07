using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseType : MonoBehaviour
{

    //判断士兵还是建筑,还是载具
    public enum object_type
    {
        buildings, sodier,vehicle
    }
    public object_type obj_type=object_type.buildings;



    //判断是队长，或者小兵，或者无;
    public enum sodier_type
    {
        teamer,comsodier,none
    }
    public sodier_type sodiertype = sodier_type.teamer;



    //判断是敌人，队友，自己，还是中立生物体
    public enum Group_type
    {
        selfs,enemy,partner,other
    }
    public Group_type group = Group_type.selfs;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
