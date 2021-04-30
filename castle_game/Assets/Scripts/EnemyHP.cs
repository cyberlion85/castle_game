using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    public float maxHP=100;
    public float curHP=100;

    public GameObject Enemy;

    //�������� ������� ��������
    public Vector3 offset;
    void Start()
    {
        
    }

    void Update()
    {
        //������� ��������� ������ � ������� � �������� �������� �� ������� �����
        GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(Enemy.transform.position+offset);

        GetComponent<Slider>().value = curHP / maxHP;


    }
}
