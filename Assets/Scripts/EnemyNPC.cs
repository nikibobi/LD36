using UnityEngine;
using System.Collections;
using System;

public class EnemyNPC : MonoBehaviour {

    public GameObject Target;
    public float DetectionRange = 10;
    public float MinimumRange = 2;
    public float JumpTriggerHeight = 2;
    public float attackDamage = 0;
    public float attackSpeed = 0f;
    private float lastAttackTime = 0;
    private bool dead = false;

    private Movement movement;
    private Rigidbody2D body;
    private HealthSystem health;

    

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<Movement>();
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (health.CheckHealthPoints())
            {
                dead = true;
                death();
            }

            Vector2 destination = (Vector2)Target.transform.position - body.position;
            float distance = destination.magnitude;
            Vector2 direction = destination / distance;
            float heightDifference = Target.transform.position.y - body.position.y;

            if (distance < DetectionRange)
            {
                if (Mathf.Round(distance) > MinimumRange)
                {
                    movement.Move(direction.x);
                }
                else
                {
                    movement.Move((direction.x / MinimumRange) * (Mathf.Round(distance) - 1));
                    //Attack timing.
                    if (Time.time > (lastAttackTime + attackSpeed) && !Target.GetComponent<Player>().dead)
                    {
                        Target.GetComponent<HealthSystem>().LoseHp(attackDamage);
                        lastAttackTime = Time.time;
                        print(this.name + " attacked!");
                    }
                }

                if (heightDifference > JumpTriggerHeight)
                {
                    movement.Jump();
                }
            }
        }
    }

    private void death()
    {
        print(name + " died!");
        Destroy(movement);
    }
}
