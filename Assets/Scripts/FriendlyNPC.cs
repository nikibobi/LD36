using System;
using UnityEngine;
using System.Collections;

public class FriendlyNPC : MonoBehaviour
{

    [Range(0, 20)]
    public float Speed = 10;
    [Range(0, 20)]
    public float MaxVelocityX = 10;
    [Range(1, 20)]
    public float JumpPower = 10;
    public bool FloatyMovement = false;
    public float LineCastLength = 0.6f;
    public LayerMask PlayerMask;
    public GameObject Target;
    public float DetectionRange = 10;
    public float MinimumRange = 2;

    private Rigidbody2D body;
    private Action<float> movement;


    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if (FloatyMovement)
        {
            movement = FloatyMove;
        }
        else
        {
            movement = SnappyMove;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveAI();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Moving Platform" && IsGrounded())
        {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D()
    {
        transform.parent = null;
    }

    private bool IsGrounded()
    {
        return Physics2D.Linecast(body.position, body.position + (Vector2.down * LineCastLength), PlayerMask);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            Debug.Log("NPC IS JUMPING");
            body.velocity += JumpPower * Vector2.up;
        }
    }

    private void FloatyMove(float direction)
    {
        //This is a Flaoty movement system with accelration and deceleration:
        body.AddForce(Vector2.right * direction * (Speed / 10), ForceMode2D.Impulse);
        body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -MaxVelocityX, MaxVelocityX), body.velocity.y);
    }

    private void SnappyMove(float direction)
    {
        //This is a Snappy movement system with almost pixel perfect movement:
        Vector2 currentVel = body.velocity;
        currentVel.x = direction * Speed;
        body.velocity = currentVel;
    }

    private void MoveAI()
    {
        Vector2 TargetDirection = DetectTarget();

        movement(TargetDirection.x);

        if (TargetDirection.y > 0.1)
        {
            Jump();
        }
    }

    private Vector2 DetectTarget()
    {
        float distance = Vector2.Distance(body.position, Target.transform.position);

        if (distance < DetectionRange && distance > MinimumRange)
        {
            Vector2 destination = (Vector2)Target.transform.position - body.position;
            float range = destination.magnitude;
            Vector2 direction = destination / range;
            return direction;
        }

        return new Vector2(0, 0);
    }

}