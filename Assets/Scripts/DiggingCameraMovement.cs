using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingCameraMovement : MonoBehaviour
{
    //Camera Movement
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    DiggingInitializeScene initScene;

    void Start(){
        initScene = GameObject.Find("InitializeSzene").GetComponent<DiggingInitializeScene>();
    }

    void Update()
    {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if(rb.position.x <= initScene.cameraPosXMin && movement.x < 0) {
            return;
        }
        if(rb.position.y <= initScene.cameraPosYMin && movement.y < 0) {
            return;
        }
        if(rb.position.x >= initScene.cameraPosXMax && movement.x > 0) {
            return;
        }
        if(rb.position.y >= initScene.cameraPosYMax && movement.y > 0) {
            return;
        }
        rb.MovePosition(rb.position +
            movement * moveSpeed * Time.fixedDeltaTime);
    }
}
