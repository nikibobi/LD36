using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    private Movement movement;
    private SkeletonAnimation spine;

	// Use this for initialization
	void Start()
    {
        movement = GetComponent<Movement>();
        spine = GetComponent<SkeletonAnimation>();
        spine.skeleton.SetAttachment("Face", "faces/seensomeshitsad");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            spine.skeleton.FlipX = false;
            spine.AnimationName = "Move";
        }
        else if (Input.GetKey(KeyCode.A))
        {
            spine.skeleton.FlipX = true;
            spine.AnimationName = "Move";
        }
        else
        {
            spine.AnimationName = "Idle";
        }
        movement.Move(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spine.state.SetAnimation(1, "Attack", false);
            movement.Jump();
        }
    }
}
