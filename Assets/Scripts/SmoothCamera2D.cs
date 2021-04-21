using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera2D : MonoBehaviour
{
    public Transform target;
    public float dampTime = 0.15f;
    public Vector2 offset;
    private Vector3 velocity = Vector3.zero;
    
    void Update() 
    {
        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f + offset.x, 0.5f + offset.y, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}