using UnityEngine;
using System.Collections;
using Spine.Unity;

public class Player : MonoBehaviour
{
    public Transform WeaponSlot;
    public Transform RightHand;
    public Transform LeftHand;

    private Movement movement;
    private SkeletonAnimation spine;
    private HealthSystem health;
    private bool hasDied = false;

    private float parryCooldown;
    private IWeapon weapon;

    private float attackStart;
    private float releaseDelayTime = 0;
    private float lastAttack=0;

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
        AnimationsAndMovement();
        HandleWeapons();
    }

    void AnimationsAndMovement()
    {
        if (!hasDied && !health.IsDead())
        {

            Vector2 playerPos = this.gameObject.transform.position;
            playerPos.y = playerPos.y + 1f;

            Vector3 currentPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
            currentPos = Camera.main.ScreenToWorldPoint(currentPos);

            Vector3 difference = currentPos - (Vector3)playerPos;
            float distance = difference.magnitude;
            Vector3 direction = difference / distance;

            if (direction.x <0)
            {
                spine.skeleton.FlipX = true;
            }
            else
            {
                spine.skeleton.FlipX = false;
            }

            movement.Move(Input.GetAxis("Horizontal"));

            if (Input.GetKey(KeyCode.D))
            {
                spine.AnimationName = "Move";
            }
            else if (Input.GetKey(KeyCode.A))
            {
                spine.AnimationName = "Move";
            }
            else
            {
                spine.AnimationName = "Idle";
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                movement.Jump();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (parryCooldown < Time.time) spine.state.SetAnimation(1, "Parried", false);
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

    void HandleWeapons()
    {
        var currentWeapon = WeaponSlot.GetChild(0);
        weapon = currentWeapon.GetComponent(typeof(IWeapon)) as IWeapon;

        Vector2 playerPos = this.gameObject.transform.position;
        playerPos.y = playerPos.y + 1f;

        Vector3 currentPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z));
        currentPos = Camera.main.ScreenToWorldPoint(currentPos);

        if (Time.time > lastAttack + 1f)
        {

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                attackStart = Time.time;
            }

            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                Vector3 difference = currentPos - (Vector3)playerPos;
                float distance = difference.magnitude;
                Vector3 direction = difference / distance;
                if (spine.skeleton.FlipX == true)
                {
                    direction.x = -direction.x;
                    direction.y = -direction.y;
                }
                RightHand.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Override;
                LeftHand.GetComponent<SkeletonUtilityBone>().mode = SkeletonUtilityBone.Mode.Override;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                RightHand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                LeftHand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                WeaponUpdate(Input.GetMouseButton(0), Input.GetMouseButton(1), Time.time - attackStart, playerPos, currentPos, spine);
            }

            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                lastAttack = Time.time;
                StartCoroutine(DelayedExecution());

                WeaponAttack(Input.GetMouseButtonUp(0), Input.GetMouseButtonUp(1), Time.time - attackStart, playerPos, currentPos, spine);
            }
        }
    }

    IEnumerator DelayedExecution()
    {
        yield return new WaitForSeconds(0.5f);
        var rightHand = RightHand.GetComponent<SkeletonUtilityBone>();
        var leftHand = LeftHand.GetComponent<SkeletonUtilityBone>();
        rightHand.bone.SetToSetupPose();
        leftHand.bone.SetToSetupPose();
        rightHand.mode = SkeletonUtilityBone.Mode.Follow;
        leftHand.mode = SkeletonUtilityBone.Mode.Follow;
    }

    void WeaponAttack(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd, SkeletonAnimation animator)
    {
        weapon.Attack(mouse1, mouse2, holdTime, origin, clickEnd, animator);
    }

    void WeaponUpdate(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd, SkeletonAnimation animator)
    {
        weapon.PreAttackUpdate(mouse1, mouse2, holdTime, origin, clickEnd, animator);
    }
}