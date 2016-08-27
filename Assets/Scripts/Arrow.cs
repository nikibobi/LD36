using UnityEngine;

public class Arrow : MonoBehaviour {

    public float damage;

    private Rigidbody2D body;

	void Start () {
        body = GetComponent<Rigidbody2D>();

    }

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

        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        print(healthSystem);
        if (healthSystem != null)
        {
            healthSystem.DoDamange(damage);
        }

        transform.parent = collision.transform;
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(body);
    }

}
