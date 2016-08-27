using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody2D body;
    public int damage = 50;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        if (body != null)
        {
            Vector2 velocity = body.velocity;
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            this.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    { 
        transform.parent = collision.transform;
        GetComponent<CircleCollider2D>().isTrigger = true;
        Destroy(body);
    }
}
