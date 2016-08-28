using UnityEngine;
interface IWeapon
{
    void Attack(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd);
    void PreAttackUpdate(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd);
}