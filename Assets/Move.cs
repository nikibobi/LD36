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
        var movementSpeed = Speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
        body.AddForce(Vector2.right * movementSpeed, ForceMode2D.Impulse);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * JumpPower, ForceMode2D.Force);
        }
    }
}
