using UnityEngine;
using System.Collections;
using Spine.Unity;

public class Spear : MonoBehaviour, IWeapon {


    private bool animationStarted = false;

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
    }

    public void PreAttackUpdate(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd, SkeletonAnimation animator)
    {
        if (!animationStarted)
        {
            animationStarted = true;
            animator.state.SetAnimation(1, "Jab", false);
        }
    }
}
