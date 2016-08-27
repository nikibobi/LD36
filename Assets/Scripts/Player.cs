using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    public bool dead = false;   

    private Movement movement;
    private SkeletonAnimation spine;
    private HealthSystem health;

	// Use this for initialization
	void Start()
    {
        movement = GetComponent<Movement>();
        spine = GetComponent<SkeletonAnimation>();
        health = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) { 
            if (health.CheckHealthPoints())
            {
                dead = true;
                death();
            }

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

                if (Input.GetMouseButtonDown(0))
                {
                    spine.state.SetAnimation(0, "Shoot", false);
                }
        }
    }

    void death()
    {
        print("player died");
        movement = null;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        dead = true;
        string animation = Random.Range(0, 2) == 0 ? "DeathBackward" : "DeathForward";
        spine.state.SetAnimation(0, animation, false);
        return;
    }
}