using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1f;
    private Vector2 playerMovement = new Vector2();
    private Rigidbody2D playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerMovement.x = Input.GetAxisRaw("Horizontal");
        playerMovement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(50f * playerMovement.x * playerSpeed * Time.deltaTime, 50f * playerMovement.y * playerSpeed * Time.deltaTime);
    }
}
