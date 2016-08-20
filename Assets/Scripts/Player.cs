using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement movement;

	// Use this for initialization
	void Start()
    {
        movement = GetComponent<Movement>();
	}

    // Update is called once per frame
    void Update()
    {
        movement.Move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();
        }
    }
}
