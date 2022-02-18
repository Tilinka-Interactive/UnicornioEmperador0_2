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

        if (Input.GetAxis("Horizontal") == 0)
        {
            dirX = Input.GetAxis("Horizontal");
        }
        else 
        {
            dirX = Input.GetAxis("Horizontal") - 0.2f;
        }

        if (Input.GetAxis("Vertical") == 0)
        {
            dirY = Input.GetAxis("Vertical");
        }
        else
        {
            dirY = Input.GetAxis("Vertical") - 0.2f;
        }


        

        direction.Set(dirX, dirY);
        direction.Normalize();
    }
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = direction * speed;
    }
}
