using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider Col)
    {
        Debug.Log(Col.gameObject.name);

        if (Col.gameObject.name.Contains("Enemy"))
        {
            Debug.Log("punch");
            Col.GetComponent<Ai_EnemyController>().Damage(10);
           // Destroy(Col.gameObject);

        }
    }
}
