using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseType : MonoBehaviour
{

    //�ж�ʿ�����ǽ���,�����ؾ�
    public enum object_type
    {
        buildings, sodier,vehicle
    }
    public object_type obj_type=object_type.buildings;



    //�ж��Ƕӳ�������С����������;
    public enum sodier_type
    {
        teamer,comsodier,none
    }
    public sodier_type sodiertype = sodier_type.teamer;



    //�ж��ǵ��ˣ����ѣ��Լ�����������������
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
