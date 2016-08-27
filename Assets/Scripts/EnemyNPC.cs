using UnityEngine;
using System.Collections;
using System;

public class EnemyNPC : MonoBehaviour {

    public GameObject Target;
    public float DetectionRange = 10;
    public float MinimumRange = 2;
    public float JumpTriggerHeight = 2;
    
    public float attackSpeed = 0f;
    private float lastAttackTime = 0;

    private Movement movement;
    private Rigidbody2D body;
    private HealthSystem health;
    private bool hasDied = false;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<Movement>();
        body = GetComponent<Rigidbody2D>();
        health = gameObject.GetComponent<HealthSystem>();
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
                if (Mathf.Round(distance) > MinimumRange)
                {
                    movement.Move(direction.x);
                }
                else
                {
                    movement.Move((direction.x / MinimumRange) * (Mathf.Round(distance) - 1));
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
        Destroy(this.gameObject);
    }
}
