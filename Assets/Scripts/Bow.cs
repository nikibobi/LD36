using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {

    private Vector2 ClickPos, CurrnetPos;
    private bool clicked = false;
    private float power;
    private Vector2 direction;

    public GameObject ArrowType;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            ClickPos = Input.mousePosition;
            clicked = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
            if (power > 10)
            {
                GameObject arrow = (GameObject)Instantiate(ArrowType, (Vector2)transform.position + (direction * 1.25f), transform.rotation);
                Rigidbody2D arrowBody = arrow.GetComponent<Rigidbody2D>();
                arrowBody.velocity = direction * power;
            }
        }

        if (Input.GetMouseButton(0))
        {
            CurrnetPos = Input.mousePosition;

            Vector2 difference = ClickPos - CurrnetPos;
            float distance = difference.magnitude;
            direction = difference / distance;
            power = distance/10;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            this.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }

    }
}
