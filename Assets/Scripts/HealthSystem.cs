using UnityEngine;

public class HealthSystem : MonoBehaviour {


    [Range(0, 100)]
    public float InitialHealht = 100, InitialArmor;

    public float HealthPoints { get; private set; }
    public float Armor { get; private set; }
    
    void Start()
    {
        HealthPoints = InitialHealht;
        Armor = InitialArmor;
    }

    public void AddHp(float healAmount)
    {
        HealthPoints += healAmount;
    }

    public void AddArmor(float amount)
    {
        Armor = Armor + amount;
    }

    public void DoDamange(float damage, bool invincible)
    {
        //League's formula for armor.
        if (!invincible)
        {
            HealthPoints -= 100 / (100 + Armor) * damage;
            print(HealthPoints);
        }
        else
        {
            print(name + " is invincible");
        }
    }

    public bool IsDead()
    {
        return HealthPoints <= 0;
    }


}
