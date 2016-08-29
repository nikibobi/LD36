using UnityEngine;
using System.Collections;
using Spine.Unity;

public class EnemyNPC : MonoBehaviour {

    public GameObject Target;
    public float DetectionRange = 10;
    public float MinimumRange = 2;
    public float AttackRange = 0.75f;
    public float JumpTriggerHeight = 2;
    
    public float attackSpeed = 0f;
    private float lastAttack = 0;

    private Movement movement;
    private SkeletonAnimation spine;
    private Rigidbody2D body;
    private HealthSystem health;
    private IWeapon weapon;
    private bool hasDied = false;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<Movement>();
        spine = GetComponent<SkeletonAnimation>();
        body = GetComponent<Rigidbody2D>();
        health = gameObject.GetComponent<HealthSystem>();
        weapon = GetComponentInChildren(typeof(IWeapon)) as IWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDied && !health.IsDead())
        {
            Vector2 destination = (Vector2)Target.transform.position - body.position;
            float distance = destination.magnitude;
            Vector2 direction = destination / distance;
            float heightDifference = Target.transform.position.y - body.position.y;

            if (distance < DetectionRange)
            {
                if (distance > MinimumRange)
                {
                    movement.Move(direction.x);
                }
                else
                {
                    movement.Move((direction.x / MinimumRange) * (Mathf.Round(distance) - 1));

                    if (Time.time > lastAttack + 1f && distance <= AttackRange)
                    {
                        weapon.Attack(true, true, 0, Vector2.zero, Vector2.zero, spine);
                        lastAttack = Time.time;
                    }
                }

                if (heightDifference > JumpTriggerHeight)
                {
                    movement.Jump();
                }
            }
        }
        else if (!hasDied)
        {
            hasDied = true;
            Death();
        }
    }

    private void Death()
    {
        print(name + " died!");
        body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        spine.state.SetAnimation(0, "Die", false).Complete += Despawn;
    }

    private void Despawn(Spine.AnimationState state, int trackIndex, int loopCount)
    {
        StartCoroutine(DestroyAfter(3));
    }

    private IEnumerator DestroyAfter(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}
