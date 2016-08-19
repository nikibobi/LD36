using System;
using UnityEngine;

public class Moving : MonoBehaviour
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

    public void Move(float direction)
    {
        move(direction);
    }

    public void Jump()
    {
        if (IsGrounded()) body.velocity += JumpPower * Vector2.up;
    }

    private bool IsGrounded()
    {
        return Physics2D.Linecast(body.position, body.position + (Vector2.down * LineCastLength), PlayerMask);
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
}
