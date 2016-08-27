using UnityEngine;

public class HealthSystem : MonoBehaviour {

    [Range(0, 100)]
    public float healthPoints = 0;
    [Range(0, 100)]
    public float armor;

    public void LoseHp(float damage)
    {
        //League's formula for damage reduction.
        healthPoints -= 100 / (100 + armor) * damage;
    }
    public void GainHp(float healAmount)
    {
        healthPoints += healAmount;
    }
    public bool CheckHealthPoints()
    {
        return healthPoints <= 0;
    }
}
