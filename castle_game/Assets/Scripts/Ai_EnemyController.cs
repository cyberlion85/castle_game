using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Ai_EnemyController : MonoBehaviour
{
    //------------------------
    public float maxHP = 100;
    public float curHP = 100;

    //смещение полоски здоровья
    public Vector3 offset;
    //------------------------


    public float DistanceToFire;
    public float DistanceToWalk;
    public float DistanceToBite;
    public float DelayFire;
    public float DelayBite;
    public float DelayBetweenAnim;
    public GameObject player;

    NavMeshAgent _agent;
    public Animator anim;
    public float curDistance;


    public bool isFire;
    public bool isBite;

    public GameObject enemyHP;
    public EnemyHP HPslider;

    public GameObject fireBall;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player1");

        //создаем полоску жизни и делаем ее дочерней канваса
        GameObject hp = Instantiate(enemyHP, Vector3.zero, Quaternion.identity) as GameObject;
        hp.transform.SetParent(GameObject.Find("Canvas").transform);
        //первый в списке
        hp.transform.SetAsFirstSibling();
        hp.GetComponent<EnemyHP>().maxHP = maxHP;
        hp.GetComponent<EnemyHP>().curHP = curHP;
        hp.GetComponent<EnemyHP>().offset = offset;
        hp.GetComponent<EnemyHP>().Enemy = gameObject;

        HPslider = hp.GetComponent<EnemyHP>();

    }

    void Update()
    {
        if (HPslider.curHP <= 0)
        {
            //GetComponent<Rigidbody>().AddForce(Vector3.right * 4000);

            _agent.enabled = false;
            anim.enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            player.GetComponent<PlayerStats>().curExp += 250;
            Destroy(HPslider.gameObject);
            this.enabled = false;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);
        curDistance = distance;
            if (distance > DistanceToFire)
        {
            return;
        }
        if (distance < DistanceToFire && distance > DistanceToWalk)
        {
            // shoot
            transform.LookAt(player.transform);
            //anim.SetBool("isFire", true);
            if (!isFire)
            {
                StartCoroutine(Fire());
            }
        }

        if (distance < DistanceToWalk && distance > DistanceToBite)
        {
            // follow
            _agent.SetDestination(player.transform.position);
            anim.SetBool("isBite", false);
            anim.SetBool("isFire", false);
        }
        if (distance < DistanceToWalk && distance < DistanceToBite)
        {
            // bite and follow
            _agent.SetDestination(player.transform.position);            
            transform.LookAt(player.transform);
            if (!isBite)
            {
                StartCoroutine(Bite());
            }
        }

        void FireBallR()
        {
            GameObject fB = Instantiate(fireBall, transform.position,transform.rotation) as GameObject;
        }

        IEnumerator Fire()
        {
            isFire = true;
            anim.SetBool("isFire", true);
            yield return new WaitForSeconds(DelayFire/2);

            FireBallR();

            yield return new WaitForSeconds(DelayFire/2);
            anim.SetBool("isFire", false);
            yield return new WaitForSeconds(DelayBetweenAnim);
            isFire = false;

        }

        IEnumerator Bite()
        {
            isBite = true;
            anim.SetBool("isBite", true);

            player.GetComponent<PlayerStats>().curLive -= 5;

            yield return new WaitForSeconds(DelayBite);
            anim.SetBool("isBite", false);
            yield return new WaitForSeconds(DelayBetweenAnim);
            isBite = false;
        }    


    }
/*    void OnMouseDown()
    {
        //если нажали на цель, передаем в скрипт плеера ее и присваиваем переменной 
        if (Input.GetMouseButtonDown(0))
        {
            player.GetComponent<PlayerNavController>().currentTarget = gameObject.transform;
        }
    }*/

    public void Damage(int dmg)
    {
        HPslider.curHP = HPslider.curHP- dmg;

    }
}
  