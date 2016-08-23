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
            float deltaX = body.position.x - point.point.x;
            float deltaY = body.position.y - point.point.y;
            float distance = (point.point - body.position).magnitude;
            Vector2 direction = (point.point - body.position) / distance;
            //RaycastHit2D hit= Physics2D.Raycast(transform.position, direction, distance + 0.1f);
            

            float angle = (float)(Math.Atan2(deltaY, deltaX) * 180 / Math.PI);

            print(angle);
            Debug.DrawLine(body.position, point.point, Color.red, 0.5f);

            if (angle>44&&angle<136)
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

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
        transform.parent = null;
    }

    public void Move(float direction)
    {
        move(direction);
    }

    public void Jump()
    {
        if (Time.time > (lastJumpTime + 0.2) && IsGrounded)
        {
            body.velocity = new Vector2(body.velocity.x, (body.velocity.y / 2) + JumpPower);
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
