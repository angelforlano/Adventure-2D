using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;
    public float speed = 4;
    public float jumpForce = 4;
    public SpriteRenderer renderer;
    public Rigidbody2D rigidbody;
    
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
    }

    void Jump()
    {
        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
