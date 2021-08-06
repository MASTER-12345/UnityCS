using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class team_Manager : MonoBehaviour
{

    public List<GameObject> member;
    //检测地点是否重复
    public List<Vector3> targetPosList;

    //攻击动画播放完之后才能移动
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
            //重新可移动
            print("后腰结束");
            gi.GetComponent<NavMeshAgent>().isStopped = false;

        }








    }


    public void takemember_attack(GameObject enemy)
    {

        print("攻击的目标" + enemy.name);

        foreach(GameObject gi in member)
        {
            //停止导航并且转向目标
            gi.GetComponent<NavMeshAgent>().isStopped = true;
            gi.transform.LookAt(enemy.transform.position);
            //播放动画
            gi.GetComponent<Sodier_Manager>().playGun(enemy);

        }


    }

    public void attackoff()
    {
        //队长结束后腰
        GetComponent<NavMeshAgent>().isStopped = false;


        foreach(GameObject gi in member)
        {
            //重新可移动
            print("后腰结束");
            gi.GetComponent<NavMeshAgent>().isStopped = false;

        }
    }


    public void takemember(Vector3 pos)
    {
        targetPosList.Clear();

        



        //让队员前往指定目标
       
        foreach(GameObject gi in member)
        {
            if (CanMove == true)
            {
                //重新开始导航
                gi.GetComponent<NavMeshAgent>().isStopped = false;

            }
            


            var randx = Random.RandomRange(-3, 3);
            var randy = Random.RandomRange(-3, 3);

            var targePos = pos + new Vector3(randx, 0, randy);

            bool ifIn = targetPosList.Contains(targePos);
            if (ifIn == true)
            {

                print("重复了");

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
