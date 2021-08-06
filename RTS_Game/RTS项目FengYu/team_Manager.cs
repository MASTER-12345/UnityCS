using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class team_Manager : MonoBehaviour
{

    public List<GameObject> member;
    //���ص��Ƿ��ظ�
    public List<Vector3> targetPosList;

    //��������������֮������ƶ�
    public bool CanMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<NavMeshAgent>().isStopped = false;


        foreach (GameObject gi in member)
        {
            //���¿��ƶ�
            print("��������");
            gi.GetComponent<NavMeshAgent>().isStopped = false;

        }








    }


    public void takemember_attack(GameObject enemy)
    {

        print("������Ŀ��" + enemy.name);

        foreach(GameObject gi in member)
        {
            //ֹͣ��������ת��Ŀ��
            gi.GetComponent<NavMeshAgent>().isStopped = true;
            gi.transform.LookAt(enemy.transform.position);
            //���Ŷ���
            gi.GetComponent<Sodier_Manager>().playGun(enemy);

        }


    }

    public void attackoff()
    {
        //�ӳ���������
        GetComponent<NavMeshAgent>().isStopped = false;


        foreach(GameObject gi in member)
        {
            //���¿��ƶ�
            print("��������");
            gi.GetComponent<NavMeshAgent>().isStopped = false;

        }
    }


    public void takemember(Vector3 pos)
    {
        targetPosList.Clear();

        



        //�ö�Աǰ��ָ��Ŀ��
       
        foreach(GameObject gi in member)
        {
            if (CanMove == true)
            {
                //���¿�ʼ����
                gi.GetComponent<NavMeshAgent>().isStopped = false;

            }
            


            var randx = Random.RandomRange(-3, 3);
            var randy = Random.RandomRange(-3, 3);

            var targePos = pos + new Vector3(randx, 0, randy);

            bool ifIn = targetPosList.Contains(targePos);
            if (ifIn == true)
            {

                print("�ظ���");

                randx = Random.RandomRange(-3, 3);
                randy = Random.RandomRange(-3, 3);
                targePos = pos + new Vector3(randx, 0, randy);
                gi.GetComponent<NavMeshAgent>().SetDestination(targePos);

            }
            else
            {
                gi.GetComponent<NavMeshAgent>().SetDestination(targePos);
            }



        }

    }
}
