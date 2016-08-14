using UnityEngine;

public class Player : MonoBehaviour {

    [Range(1, 10)]
    public float Speed;
    [Range(1, 10)]
    public float JumpPower;
    [Range(1, 10)]
    public float MaxVelocityX;
    public float LineCastLength;
    public LayerMask playerMask;

    private Rigidbody2D body;

	// Use this for initialization
	void Start()
    {
        body = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
        HandleMovement(Input.GetAxis("Horizontal"));

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
        var linecastEnd = body.position + (Vector2.down * LineCastLength);
        Debug.DrawLine(body.position, linecastEnd, Color.yellow);
        print("Player is grounded...");
        return Physics2D.Linecast(body.position, linecastEnd, playerMask);
    }

    private void HandleMovement(float direction)
    {
        body.AddForce(Vector2.right * direction, ForceMode2D.Impulse);
        body.velocity.Set(Mathf.Clamp(body.velocity.x, -MaxVelocityX, MaxVelocityX), body.velocity.y);
    }

    private void HandleJump()
    {
        if (IsGrounded())
        { 
            body.velocity += JumpPower * Vector2.up;
        }
    }
}
