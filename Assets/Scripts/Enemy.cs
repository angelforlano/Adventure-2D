using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public float moveVelocity = 2f;
    public float moveTime = 1.5f;
    public SpriteRenderer renderer;
    public Image hpBarImage;

    int startHp;
    bool direction;

    void Start()
    {
        startHp = hp;
        StartCoroutine(Move());
    }

    void Update()
    {
        if (hp <= 0) return;

        if(direction)
        {
            transform.Translate(moveVelocity * Time.deltaTime, 0, 0);
        } else {
            transform.Translate(-moveVelocity * Time.deltaTime, 0, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpBarImage.fillAmount = (float) hp/startHp;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Move()
    {
        while (hp > 0)
        {
            yield return new WaitForSeconds(moveTime);
            direction = !direction;
            renderer.flipX = !renderer.flipX;
        }
    }
}