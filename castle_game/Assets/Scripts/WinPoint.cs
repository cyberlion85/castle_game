using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1"))
        {
            Debug.Log("WinPoint trigger");

            GameObject Enemies = GameObject.Find("Enemies");
            if (Enemies.transform.childCount <= 1)
            {
                SceneManager.LoadScene(2);
            }

        }
    }
}
