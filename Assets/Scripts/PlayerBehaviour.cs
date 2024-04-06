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
    [SerializeField]
    bool isDucking = false;

    PolygonCollider2D polygonCollider2D;
    Vector2[] fullSizeColliderPoints;
    Vector2[] smallSizeColliderPoints;

    public bool IsPowerUpActive { get; set; } = false;

    public bool CanCollide { get; set; } = true;

    void Start()
    {
        jumpVector = new(0, jumpForce);
        SpriteRenderer = GetComponent<SpriteRenderer>();
        RigidBody = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        fullSizeColliderPoints = polygonCollider2D.points;
        smallSizeColliderPoints = CalculateSmallColliderPoints();
    }

    void Update()
    {
        if (GameManager.IsFinished) return;
        if (PlayerController.WantsToJump() && isGrounded)
            Jump();

            
        if (PlayerController.WantsToDuck() && !isDucking)
        {
            Duck();
            isDucking = true;
        }
        if (!PlayerController.WantsToDuck() && isDucking)
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
            polygonCollider2D.points = fullSizeColliderPoints;
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
        polygonCollider2D.points = smallSizeColliderPoints;
        RigidBody.gravityScale = duckingGravityScale;
    }

    private void UnDuck()
    {
        SpriteRenderer.sprite = isGrounded ? normalSprite : jumpingSprite;
        polygonCollider2D.points = fullSizeColliderPoints;
        RigidBody.gravityScale = 1;
    }

    private void Jump()
    {
        // Note to future self
        // sometimes the game engine bugs and queues two jumps
        // this will prevent this stupid double jump
        if (RigidBody.velocity.y != 0) return;

        isGrounded = false;
        RigidBody.AddForce(jumpVector, ForceMode2D.Impulse);
        SpriteRenderer.sprite = jumpingSprite;
        polygonCollider2D.points = smallSizeColliderPoints;
    }

    private Vector2[] CalculateSmallColliderPoints()
    {
        Vector2[] smallColliderPoints = new Vector2[fullSizeColliderPoints.Length];
        float colliderSizeDifference = 0.35f;

        for (int i = 0; i < fullSizeColliderPoints.Length; i++)
        {
            Vector2 point = fullSizeColliderPoints[i];
            if (point.y < 0)
            {
                smallColliderPoints[i] = new(point.x, point.y + colliderSizeDifference);
            }
            else if (point.y > 0)
            {
                smallColliderPoints[i] = new(point.x, point.y - colliderSizeDifference);
            }
        }

        return smallColliderPoints;
    }
}