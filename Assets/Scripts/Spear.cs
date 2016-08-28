using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour, IWeapon {
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd)
    {
        if (mouse1 && holdTime >= 1)
        {
            Vector3 differnece = origin - clickEnd;
            float distance = differnece.magnitude;
            Vector3 direction = differnece / distance;
            print("fittchick");
        }
    }

    public void PreAttackUpdate(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd)
    {
        print("fittchick");
    }
}
