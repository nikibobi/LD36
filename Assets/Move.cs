using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    [Range(10, 100)]
    public float Speed;

    private Rigidbody2D body;
	// Use this for initialization
	void Start() {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update() {
        var speed = new Vector2(Speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime, 0);
        body.AddForce(speed, ForceMode2D.Impulse);
	}
}
