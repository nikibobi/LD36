using UnityEngine;
using Spine.Unity;

public class Player : MonoBehaviour
{
    [Range(0, 100)]
    public int hp;
    [Range(0, 100)]
    public int armor;
    bool dead = false;

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
        if (hp <= 0 && !dead)
        {
            Destroy(gameObject.GetComponent<Player>());
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;    
            dead = true;
            //string animation = Random.Range(0, 2) == 0 ? "DeathBackward" : "DeathForward";
            spine.state.SetAnimation(0, "DeathBackward", false);
            return;
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
    }

    public void LoseHp(int damageTaken)
    {
        //League's formula for damage reduction.
        hp -= Mathf.Abs(100 / (100 + armor) - damageTaken);
    }
    public void GainHp(int healAmount)
    {
        hp += healAmount;
    }
}
