using UnityEngine;
using Spine.Unity;
interface IWeapon
{
    void Attack(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd, SkeletonAnimation animator);
    void PreAttackUpdate(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd, SkeletonAnimation animator);
}