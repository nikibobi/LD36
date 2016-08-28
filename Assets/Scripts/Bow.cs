using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour, IWeapon
{

    public GameObject ArrowType;

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
        Vector3 differnece = clickEnd - origin;
        float distance = differnece.magnitude;
        Vector3 direction = differnece / distance;

        var power = holdTime * 50;

        print("Power = " + power + " Direction = " + direction);
        if (power > 10)
        {
            power = Mathf.Clamp(power, 10, 100);
            GameObject arrow = (GameObject)Instantiate(ArrowType, (Vector3)transform.position + (direction * 2f), transform.rotation);
            Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
            arrowBody.velocity = direction * power;
        }
    }

    public void PreAttackUpdate(bool mouse1, bool mouse2, float holdTime, Vector2 origin, Vector2 clickEnd)
    {
        //Vector3 difference = clickEnd - origin;
        //float distance = difference.magnitude;
        //Vector3 direction = difference / distance;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //this.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
