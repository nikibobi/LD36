using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    private Movement movement;
    private SkeletonAnimation spine;
    private HealthSystem health;
    private bool hasDied = false;

    private float parryCooldown;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<Movement>();
        spine = GetComponent<SkeletonAnimation>();
        health = gameObject.GetComponent<HealthSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if ( !hasDied && !health.IsDead() )
        {
            movement.Move(Input.GetAxis("Horizontal"));

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
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(movement.IsGrounded) spine.state.SetAnimation(1, "Attack", false);
                movement.Jump();
            }

            if (Input.GetMouseButtonDown(0))
            {
                spine.state.SetAnimation(0, "Shoot", false);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if(parryCooldown < Time.time) spine.state.SetAnimation(1, "Parried", false);
                movement.Parry(out parryCooldown);
            }
        }
        else if (!hasDied)
        {
            hasDied = true;
            Death();
        }
    }

    void Death()
    {
        movement = null;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        string animation = Random.Range(0, 2) == 0 ? "DeathBackward" : "DeathForward";
        spine.state.SetAnimation(0, animation, false);
        return;
    }
}