using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public float speed = 4;
    public float jumpForce = 4;
    public int stars;
    public Vector2 offset;
    public float damageRadius;
    
    public Animator animator;
    public SpriteRenderer renderer;
    public Rigidbody2D rigidbody;
    
    private int startHp;
    private bool isAttacking;
    private int jumps;

    public float HpPercent
    {
        get { return (float) hp / startHp;}
    }

    private void Start()
    {
        startHp = hp;
    }
    
    void Update()
    {
        if (hp <= 0) return;

        float horizontal = Input.GetAxis("Horizontal") * speed;

        if(!isAttacking)
            transform.Translate(horizontal * Time.deltaTime, 0, 0);

        if (horizontal != 0)
            renderer.flipX = horizontal > 0;
        
        animator.SetBool("isMoving", horizontal != 0);

        if (Input.GetKeyDown(KeyCode.Space) && jumps < 2)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Return) && !isAttacking)
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            var position = new Vector2(transform.position.x + (renderer.flipX? offset.x:-offset.x), transform.position.y + offset.y);
            var all = Physics2D.OverlapCircleAll(position, damageRadius);
            
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].gameObject.CompareTag("Chest"))
                {
                    all[i].gameObject.GetComponent<Chest>().Open(this);
                    break;
                }
            }
        }
    }

    private void Jump()
    {
        jumps++;
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("jump");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var position = new Vector2(transform.position.x + (renderer.flipX? offset.x:-offset.x), transform.position.y + offset.y);
        Gizmos.DrawWireSphere(position, damageRadius);
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("attack");
        
        yield return new WaitForSeconds(0.5f);
        
        var position = new Vector2(transform.position.x + (renderer.flipX? offset.x:-offset.x), transform.position.y + offset.y);
        var all = Physics2D.OverlapCircleAll(position, damageRadius);
        
        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].gameObject.CompareTag("Enemies"))
            {
                all[i].gameObject.GetComponent<Enemy>().TakeDamage(1);
                break;
            }
        }

        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumps=0;
            animator.SetTrigger("idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            stars++;
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;

        hp -= damage;
        animator.SetTrigger("hurt");
        Debug.Log("Player Take " + damage + " damage");

        if(hp <= 0)
        {
            Debug.Log("Player Die");
            animator.SetTrigger("die");
        }
    }
}
