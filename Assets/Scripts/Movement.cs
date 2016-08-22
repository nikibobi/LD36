using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Range(0, 20)]
    public float Speed = 10;
    [Range(0, 20)]
    public float MaxVelocityX = 10;
    [Range(1, 20)]
    public float JumpPower = 10;
    public bool FloatyMovement = false;
    public float LineCastLength = 0.51f;
    public LayerMask PlayerMask;

    private Rigidbody2D body;
    private Action<float> move;
    private float lastJumpTime = 0;
    private bool IsGrounded = false;

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

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var point in collision.contacts)
        {
            Vector2 vector;
            vector.x = body.position.x - point.point.x;
            vector.y = body.position.y - point.point.y;
            float angle = (float)Math.Atan2(vector.x, vector.y);

            if (angle<0.81 && angle > -0.81)
            {
                IsGrounded = true;
            }
        }
            
        if (collision.transform.tag == "Moving Platform" && IsGrounded)
        {
            if (Math.Abs(body.velocity.x) < 0.025 && Math.Abs(body.velocity.y) < 0.025)
            {
                transform.parent = collision.transform;
            }
            else
            {
                transform.parent = null;
            }
        }
        else
        {
            transform.parent = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("In contact with " + collision.transform.name);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        print("No longer in contact with " + collision.transform.name);
        IsGrounded = false;
        transform.parent = null;
    }

    public void Move(float direction)
    {
        move(direction);
    }

    public void Jump()
    {
        if (Time.time > (lastJumpTime + 0.2))
        {
            if (IsGrounded) body.velocity += JumpPower * Vector2.up;
            lastJumpTime = Time.time;
        }
    }

    //private bool IsGrounded()
    //{
    //    return Physics2D.Linecast(body.position, body.position + (Vector2.down * LineCastLength), PlayerMask);
    //}

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
}
