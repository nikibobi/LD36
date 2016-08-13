using UnityEngine;

public class Move : MonoBehaviour {

    [Range(1, 10)]
    public float Speed, JumpPower , MaxVelocityX;
    public float LineCastLength;
    public LayerMask playerMask;

    //public Transform RayCastStart, RayCastEnd;

    private Rigidbody2D body;


	// Use this for initialization
	void Start() {
        body = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {

        HandleMovement(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }

    }

    public bool IsGrounded()
    {
        var linecastEnd = body.position + (Vector2.down * LineCastLength);
        return Physics2D.Linecast(body.position, linecastEnd, playerMask);
    }

    public void HandleMovement(float direction)
    {
        body.AddForce(Vector2.right * direction, ForceMode2D.Impulse);

        Vector2 currentVelocity = body.velocity;
        if (currentVelocity.x > 0 && currentVelocity.x > MaxVelocityX)
        {
            currentVelocity.x = MaxVelocityX;
        }
        if (currentVelocity.x < 0 && currentVelocity.x < -MaxVelocityX)
        {
            currentVelocity.x = -MaxVelocityX;
        }

        body.velocity = currentVelocity;
    }

    public void HandleJump()
    {
        if (IsGrounded())
        { 
            body.velocity += JumpPower * Vector2.up;
        }
    }

}
