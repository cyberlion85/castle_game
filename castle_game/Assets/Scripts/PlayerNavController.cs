using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavController : MonoBehaviour
{
    public int dmg = 10;
    public Vector3 targetPos;
    public GameObject Ground;
    public bool isPunch = false;
    NavMeshAgent _agent;
    Animator _anim;
    void Start()
    {
        targetPos = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
       if(Input.GetMouseButtonDown(1) && !isPunch)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Ground.GetComponent<Collider>().Raycast(ray,out hit, Mathf.Infinity))
            {
                targetPos = hit.point;
                //currentTarget = null;
                if (isPunch)
                {
                    transform.LookAt(hit.transform);
                }
            }

        }
        _agent.SetDestination(targetPos);

        if (!isPunch)
        {

            if (_agent.velocity == Vector3.zero)
            {
                GetComponent<Animator>().SetBool("stay", true);
            }
            if (_agent.velocity != Vector3.zero)
            {
                GetComponent<Animator>().SetBool("stay", false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Ground.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                transform.LookAt(hit.point);               
            }

            isPunch = true;
            targetPos = transform.position;
            _agent.velocity = Vector3.zero;
            _anim.SetBool("punch", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPunch = false;
            _anim.SetBool("punch", false);
        }
    }


}
