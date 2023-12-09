using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    //Movement
    [SerializeField] private float playerSpeed = 1f;
    private Vector2 playerMovement = new Vector2();
    private Rigidbody2D playerRB;
    public bool spaceShooterMovement = true;
    
    private Vector2 leftPoint = new Vector2();
    private Vector2 rightPoint = new Vector2();
    
    
    

    private void Start()
    {
        leftPoint = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.75f));
        rightPoint = Camera.main.ViewportToWorldPoint(new Vector2(1, 0f));
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (spaceShooterMovement)
        {
            playerMovement.x = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            playerMovement.x = Input.GetAxisRaw("Horizontal");
            playerMovement.y = Input.GetAxisRaw("Vertical");
        }
        
    }
    
    private void FixedUpdate()
    {
        if (spaceShooterMovement)
        {
            playerRB.velocity = new Vector2(50f * playerMovement.x * playerSpeed * Time.deltaTime, 50f * playerMovement.y * playerSpeed * Time.deltaTime);
            Vector2 clampedPos = new Vector2();
            clampedPos.x = Mathf.Clamp(transform.position.x, leftPoint.x, rightPoint.x);
            clampedPos.y = -2.5f;
            transform.position = clampedPos;
        }
        else
        {
            playerRB.velocity = new Vector2(50f * playerMovement.x * playerSpeed * Time.deltaTime, 50f * playerMovement.y * playerSpeed * Time.deltaTime);
            Vector2 clampedPos = new Vector2();
            clampedPos.y = Mathf.Clamp(transform.position.y, rightPoint.y, leftPoint.y);
            clampedPos.x = Mathf.Clamp(transform.position.x, leftPoint.x, rightPoint.x);
            transform.position = clampedPos;
        }
        
    }
}
