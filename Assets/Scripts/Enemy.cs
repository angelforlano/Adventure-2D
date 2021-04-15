using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public Image hpBarImage;

    int startHp;

    // Start is called before the first frame update
    void Start()
    {
        startHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpBarImage.fillAmount = (float) hp/startHp;
    }
}