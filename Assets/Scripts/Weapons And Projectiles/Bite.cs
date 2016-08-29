using UnityEngine;
using System.Collections;
using Spine.Unity;

public class Bite : MonoBehaviour, IWeapon
{

    public float damage;
    private bool damageNow = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd, SkeletonAnimation animator)
    {
        damageNow = true;
        animator.state.SetAnimation(0, "Attack", false);
        StartCoroutine(DelayedExecution(0.5f, () => { damageNow = false; }));
    }

    public void PreAttackUpdate(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd, SkeletonAnimation animator)
    {

    }

    IEnumerator DelayedExecution(float time, System.Action function)
    {
        yield return new WaitForSeconds(time);
        function();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        //print(healthSystem);
        if (healthSystem != null && damageNow)
        {
            healthSystem.DoDamange(damage, collision.gameObject.GetComponent<Movement>().inParry);
            damageNow = false;
        }
    }
}
