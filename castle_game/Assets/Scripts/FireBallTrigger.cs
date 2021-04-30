using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTrigger : MonoBehaviour
{
    public float damage;
    public float speed;
    private void OnTriggerEnter(Collider Col)
    {

        if (Col.gameObject.CompareTag("Player1"))
        {
            //Debug.Log("Col.CompareTag(Player1)");
            Col.gameObject.GetComponent<PlayerStats>().curLive -= damage;
            Destroy(gameObject);

        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Start()
    {
        //уничтожить через 10 сек
        Destroy(gameObject, 10);

    }

}