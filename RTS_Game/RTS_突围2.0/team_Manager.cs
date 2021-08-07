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


    }


    public void takemember_attack(GameObject enemy)
    {

        print("攻击的目标" + enemy.name);

        foreach(GameObject gi in member)
        {

            //延时零散攻击
            StartCoroutine(takemenmber_attack_2(gi,enemy));



        }


    }
    IEnumerator takemenmber_attack_2(GameObject gi,GameObject enemy)
    {
        var wai = Random.RandomRange(0.1f, 0.9f);
        yield return new WaitForSeconds(wai);  //暂停协程，2秒后执行之后的操作

        //停止导航并且转向目标
        //gi.GetComponent<NavMeshAgent>().isStopped = true;(错误的)
        //应该禁止脚本再打开
        gi.GetComponent<NavMeshAgent>().enabled = false;


        gi.transform.LookAt(enemy.transform.position);
        
        //播放动画
        gi.GetComponent<Sodier_Manager>().playGun(enemy);
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
