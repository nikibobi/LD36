using UnityEngine;
using System.Collections;
using System;

public class HealthSystem : MonoBehaviour {

    [Range(0, 100)]
    public float healthPoints = 0;
    [Range(0, 100)]
    public float armor;
    public bool dead = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (checkDeath()) { }
	}

    private bool checkDeath()
    {
        return healthPoints <= 0;
    }

    public void LoseHp(GameObject target, float damage)
    {
        var tar = target.GetComponent<Player>();
        //League's formula for damage reduction.
        healthPoints -= 100 / (100 + armor) * damage;
    }
    public void GainHp(GameObject target, float healAmount)
    {
        //healthPoints += healAmount;
    }
}
