using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    [Range(10, 100)]
    public float Speed;
	// Use this for initialization
	void Start() {
	}
	
	// Update is called once per frame
	void Update() {
        this.transform.Translate(Speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0, 0, Space.World);	
	}
}
