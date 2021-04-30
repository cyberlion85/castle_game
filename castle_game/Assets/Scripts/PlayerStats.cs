using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float maxLife = 100f;
    public float curLive = 100f;
    public int level = 1;
    public float nextLevelExp = 1000;
    public float curExp = 0;

    public Text Level_Text;
    public Slider EXP_Slider;
    public Slider HP_Slider;


    void Start()
    {
    }

    void Update()
    {
        HP_Slider.value = (curLive/maxLife);
        EXP_Slider.value = (curExp / nextLevelExp);
        Level_Text.text = level.ToString();

        if(curExp>=nextLevelExp)
        {
            level++;
            curExp = 0;
            nextLevelExp += 500;
        }

    }
}
