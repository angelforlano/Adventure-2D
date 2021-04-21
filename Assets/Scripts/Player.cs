using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public float speed = 4;
    public float jumpForce = 4;
    public Vector2 offset;
    public float damageRadius;
    
    public Animator animator;
    public SpriteRenderer renderer;
    public Rigidbody2D rigidbody;

    private Enemy enemy;
    private int jumps;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;

        transform.Translate(horizontal * Time.deltaTime, 0, 0);

        if (horizontal != 0)
            renderer.flipX = horizontal > 0;
        
        animator.SetBool("isMoving", horizontal != 0);

        if (Input.GetKeyDown(KeyCode.Space) && jumps < 2)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Attack();
        }
    }

    void Jump()
    {
        jumps++;
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("jump");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var position = new Vector2(transform.position.x + (renderer.flipX? offset.x:-offset.x), transform.position.y + offset.y);
        Gizmos.DrawWireSphere(position, damageRadius);
    }

    void Attack()
    {
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

        animator.SetTrigger("attack");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumps=0;
            animator.SetTrigger("idle");
        }
    }
}
