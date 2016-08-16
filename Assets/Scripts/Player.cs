using System;
using UnityEngine;

public class Player : MonoBehaviour {

    [Range(0, 10)]
    public float Speed;
    [Range(0, 10)]
    public float MaxVelocityX;
    [Range(1, 10)]
    public float JumpPower;
    public bool FloatyMovement;
    public float LineCastLength;
    public LayerMask PlayerMask;

    private Rigidbody2D body;
    private Action<float> move;

	// Use this for initialization
	void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if(FloatyMovement)
        {
            move = FloatyMove;
        }
        else
        {
            move = SnappyMove;
        }
	}

    // Update is called once per frame
    void Update()
    {
        move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
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

    private bool IsGrounded()
    {
        return Physics2D.Linecast(body.position, body.position + (Vector2.down * LineCastLength), PlayerMask);
    }

    private void HandleJump()
    {
        if (IsGrounded())
        {
            Debug.Log("Is grounded");
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
}
