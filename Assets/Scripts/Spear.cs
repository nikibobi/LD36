using UnityEngine;
using System.Collections;
using Spine.Unity;

public class Spear : MonoBehaviour, IWeapon {


    private bool animationStarted = false;
    private Quaternion rotation;

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
        animationStarted = false;
        animator.state.SetAnimation(1, "JabAfter", false);
        StartCoroutine(DelayedExecution());
    }

    public void PreAttackUpdate(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd, SkeletonAnimation animator)
    {
        if (!animationStarted)
        {
            rotation = gameObject.transform.rotation;
            animationStarted = true;
            animator.state.SetAnimation(1, "JabBefore", false);
        }
        Vector2 difference = origin - clickEnd;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 100;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }

    IEnumerator DelayedExecution()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.transform.rotation = rotation;
    }
}
