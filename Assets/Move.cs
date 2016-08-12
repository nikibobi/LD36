using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    [Range(10, 100)]
    public float Speed;
    [Range(10, 100)]
    public float JumpPower;
    // Use this for initialization
    void Start() {
	}
	
	// Update is called once per frame
	void Update() {
        float MovementSpeed = Speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float JumpHeight = JumpPower * Input.GetAxis("Vertical") * Time.deltaTime;
        this.transform.Translate(MovementSpeed, 0, 0, Space.World);
        this.transform.Translate(0, JumpHeight, 0, Space.World);
    }
}
