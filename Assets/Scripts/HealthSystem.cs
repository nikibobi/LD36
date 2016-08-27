using UnityEngine;

public class HealthSystem : MonoBehaviour {

    [Range(0, 100)]
    public float healthPoints = 100;
    [Range(0, 100)]
    public float armor;
    
    void Update()
    {

    }

    public void LoseHp(float damage)
    {
        //League's formula for damage reduction.
        healthPoints -= 100 / (100 + armor) * damage;
        print(healthPoints);
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
