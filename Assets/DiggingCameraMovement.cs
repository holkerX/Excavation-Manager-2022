using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingCameraMovement : MonoBehaviour
{
    //Camera Movement
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;

    void Update()
    {
        if (movement.y == 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }

        if (movement.x == 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position +
            movement * moveSpeed * Time.fixedDeltaTime);
    }
}
