using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseType : MonoBehaviour
{

    public enum Can_attack
    {
        yes,no
    }
    public Can_attack canbe_attack = Can_attack.yes;

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
        selfs,enemy,partner,other,none
    }
    public Group_type group = Group_type.selfs;

    //�жϽ�������
    public enum Buiding_kind
    {
        produce,
        defence,
        spy,
        none
    }
    public Buiding_kind buildkind = Buiding_kind.produce;
    //�жϱ���
    public enum sodier_kind
    {
        comsodier,none

    }
    public sodier_kind sodierkind = sodier_kind.comsodier;



    

   
}
