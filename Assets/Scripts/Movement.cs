using System;
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [Range(0, 20)]
    public float Speed = 10;
    [Range(0, 20)]
    public float MaxVelocityX = 10;
    [Range(1, 30)]
    public float JumpPower = 15;
    public bool FloatyMovement = false;
    public float LineCastLength = 0.51f;
    public LayerMask PlayerMask;

    private Rigidbody2D body;
    private Action<float> move;
    private float lastJumpTime = 0;
    private bool isGrounded = false;
    private bool isOnMovingPlatform = false;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if (FloatyMovement)
        {
            move = FloatyMove;
        }
        else
        {
            move = SnappyMove;
        }
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + (Vector3)body.velocity, Color.green);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.transform.tag == "Moving Platform")
        //{
        //    transform.parent = collision.transform;
        //}
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.transform.tag == "Moving Platform" )
        //{
        //    transform.parent = collision.transform;
        //}
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //transform.parent = null;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;

        if (collision.transform.tag == "Moving Platform")
        {
            Rigidbody2D collidingBody = collision.GetComponent<Rigidbody2D>();
            Vector2 currentVel = body.velocity;
            body.velocity = collidingBody.velocity;
            isOnMovingPlatform = true;
        }
        else
        {
            isOnMovingPlatform = false;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    public void Move(float direction)
    {
        move(direction);
    }

    public void Jump()
    {
        if (Time.time > (lastJumpTime + 0.2) && isGrounded)
        {
            isGrounded = false;
            body.velocity = new Vector2(body.velocity.x, JumpPower);
            lastJumpTime = Time.time;
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
        if (isOnMovingPlatform == false && isGrounded)
        {
            currentVel.y /= 2;
        }
        body.velocity = currentVel;
    }
}
