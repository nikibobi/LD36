using UnityEngine;

public class Move : MonoBehaviour {

    [Range(10, 100)]
    public float Speed;
    [Range(100, 1000)]
    public float JumpPower;

    private Rigidbody2D body;
	// Use this for initialization
	void Start() {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update() {
        var movementSpeed = new Vector2(Speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime, 0);
        var jumpHeight = new Vector2(0, JumpPower * Input.GetAxis("Vertical"));// * Time.fixedDeltaTime);
        body.AddForce(movementSpeed, ForceMode2D.Impulse);
        body.AddForce(jumpHeight, ForceMode2D.Force);
    }
}
