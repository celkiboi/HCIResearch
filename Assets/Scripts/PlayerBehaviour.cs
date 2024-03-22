using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Rigidbody2D RigidBody;
    public static IPlayerController PlayerController;
    [SerializeField]
    float duckingGravityScale = 1.0f;

    [SerializeField]
    Sprite normalSprite;
    [SerializeField]
    Sprite duckingSprite;
    [SerializeField]
    Sprite jumpingSprite;
    SpriteRenderer SpriteRenderer;

    [SerializeField]
    float jumpForce = 1.0f;
    Vector2 jumpVector;
    [SerializeField]
    bool isGrounded;
    bool isDucking = false;

    public static bool CanCollide { get; set; } = true;

    void Start()
    {
        jumpVector = new(0, jumpForce);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        RigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PlayerController.WantsToJump() && isGrounded)
            Jump();
        if (PlayerController.WantsToDuck() && !isDucking)
        {
            Duck();
            isDucking = true;
        }   
        if (PlayerController.WantsToStopDucking() && isDucking)
        {
            UnDuck();
            isDucking = false;
        }
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
            isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isDucking)
        {
            SpriteRenderer.sprite = normalSprite;
            GetComponent<BoxCollider2D>().size = normalSprite.bounds.size;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanCollide) return;

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            IPowerUp powerUp = collision.gameObject.GetComponent<IPowerUp>();
            powerUp.Activate();
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.IsFinished = true;
        }
    }

    private void Duck()
    {
        SpriteRenderer.sprite = duckingSprite;
        GetComponent<BoxCollider2D>().size = duckingSprite.bounds.size;
        RigidBody.gravityScale = duckingGravityScale;
    }

    private void UnDuck()
    {
        SpriteRenderer.sprite = normalSprite;
        GetComponent<BoxCollider2D>().size = normalSprite.bounds.size;
        RigidBody.gravityScale = 1;
    }

    private void Jump()
    {
        RigidBody.AddForce(jumpVector, ForceMode2D.Impulse);
        SpriteRenderer.sprite = jumpingSprite;
        GetComponent<BoxCollider2D>().size = jumpingSprite.bounds.size;
    }

}
