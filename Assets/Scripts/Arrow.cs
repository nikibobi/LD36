using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    private Rigidbody2D body;
    private bool collided = false;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!collided)
        {
            Vector2 velocity = body.velocity;
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            this.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (true)//collision.transform.tag == "Ground")
        {
            collided = true;
            //gameObject.transform.parent = collision.transform;
            body.isKinematic = true;
            //Destroy(body);
            //Destroy(GetComponent<CircleCollider2D>());
            //body.freezeRotation = true;
            //body.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
