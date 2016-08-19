using UnityEngine;

public class Player : MonoBehaviour
{
    private Moving mover;

	// Use this for initialization
	void Start()
    {
        mover = GetComponent<Moving>();
	}

    // Update is called once per frame
    void Update()
    {
        mover.Move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mover.Jump();
        }
    }
}
