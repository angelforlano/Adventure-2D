using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int hp = 3;
    public int damage = 1;
    public float moveVelocity = 2f;
    public float moveTime = 1.5f;
    public SpriteRenderer renderer;
    public Image hpBarImage;
    
    public GameObject deathVfx;
    public GameObject deathSfx;

    int startHp;
    bool direction;

    void Start()
    {
        var factor = Random.Range(0, 100);
        var sppedFactor = Random.Range(0f, 0.25f);
        
        moveVelocity += sppedFactor;
        
        if (factor > 80)
            hp++;

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
            var vfx = Instantiate(deathVfx, transform.position, Quaternion.identity);
            var sfx = Instantiate(deathSfx, transform.position, Quaternion.identity);
            
            Destroy(vfx, 0.5f);
            Destroy(sfx, 1);
            
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}