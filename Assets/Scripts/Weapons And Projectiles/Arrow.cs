using UnityEngine;
using System.Collections;
public class Arrow : MonoBehaviour {

    public float damage;

    private Rigidbody2D body;

	void Start () {
        body = GetComponent<Rigidbody2D>();
        GetComponent<AudioSource>().Play();
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
            healthSystem.DoDamange(damage, collision.gameObject.GetComponent<Movement>().inParry);
        }

        transform.parent = collision.transform;
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(body);
        StartCoroutine(DelayedExecution(1f, () => {
            Destroy(GetComponent<AudioSource>());
        }));
    }

    IEnumerator DelayedExecution(float time, System.Action function)
    {
        yield return new WaitForSeconds(time);
        function();
    }

}
