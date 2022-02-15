using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmperorMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Vector2 direction;
    [SerializeField] private float speed;
    
    void Start()
    {
        Rigidbody2D = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update()
    {
        float dirX;
        float dirY;

        dirX = Input.GetAxis("Horizontal"); 
        dirY = Input.GetAxis("Vertical");

        
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = direction * speed;
    }
}
