﻿using System;
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    [Range(0, 20)]
    public float Speed = 10;
    [Range(0, 20)]
    public float MaxVelocityX = 10;
    [Range(1, 30)]
    public float JumpPower = 15;

    public bool FloatyMovement = false;
    public bool AirStrafing = true;

    public float parryTime = 0.5f;
    private float lastParry = 0f;
    public bool inParry { get; private set; }

    public bool IsGrounded { get { return isGrounded; } }

    private Rigidbody2D body;
    private Action<float> move;
    private float lastJumpTime = 0;
    private bool isGrounded = false;
    private bool isOnMovingPlatform = false;
    private Rigidbody2D collidingBody;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        if (AirStrafing)
        {
            move = FloatyMovement ? FloatyMove : (Action<float>)SnappyMove;
        }
        else
        {
            move = FloatyMovement ? FloatyMoveNoAirStrafing : (Action<float>)SnappyMoveNoAirStrafing;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        isGrounded = true;

        if (collision.transform.tag == "Moving Platform")
        {
            collidingBody = collision.GetComponent<Rigidbody2D>();
            isOnMovingPlatform = true;
        }
        else
        {
            isOnMovingPlatform = false;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }

    public void Move(float direction)
    {
        move(direction);
    }

    public void Jump()
    {
        if (Time.time > (lastJumpTime + 0.2) && isGrounded)
        {
            isGrounded = false;
            body.velocity = new Vector2(body.velocity.x, JumpPower);
            lastJumpTime = Time.time;
        }
    }

    public void Parry(out float nextAvailableParry)
    {
        if (Time.time > lastParry + 2)
        {
            inParry = false;
        }
            
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time > (lastParry + 2))
            {
                inParry = true;
                lastParry = Time.time;
            }
        }
        nextAvailableParry = lastParry + 2;
    }

    private void FloatyMove(float direction)
    {
        //This is a Flaoty movement system with accelration and deceleration:
        body.AddForce(Vector2.right * direction * (Speed / 10), ForceMode2D.Impulse);
        body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -MaxVelocityX, MaxVelocityX), body.velocity.y);
    }

    private void FloatyMoveNoAirStrafing(float direction)
    {
        //This is a Flaoty movement system with accelration and deceleration:
        if (isGrounded)
        {
            body.AddForce(Vector2.right * direction * (Speed / 10), ForceMode2D.Impulse);
            body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -MaxVelocityX, MaxVelocityX), body.velocity.y);
        }
    }

    private void SnappyMove(float direction)
    {
        //This is a Snappy movement system with almost pixel perfect movement:
        Vector2 currentVel = body.velocity;
        currentVel.x = direction * Speed;

        if (isGrounded)
        {
            if (isOnMovingPlatform)
            {
                currentVel.x += collidingBody.velocity.x;
                currentVel.y = collidingBody.velocity.y;
            }
            else
            {
                currentVel.y /= 2;
            }
        }
        body.velocity = currentVel;
    }

    private void SnappyMoveNoAirStrafing(float direction)
    {
        //This is a Snappy movement system with almost pixel perfect movement:
        if (isGrounded)
        {
            Vector2 currentVel = body.velocity;
            currentVel.x = direction * Speed;
            
            if (isOnMovingPlatform)
            {
                currentVel.x += collidingBody.velocity.x;
                currentVel.y = collidingBody.velocity.y;
            }
            else
            {
                currentVel.y /= 2;
            }

            body.velocity = currentVel;
        }
    }
}
