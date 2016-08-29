using UnityEngine;

public class FriendlyNPC : MonoBehaviour
{
    public GameObject Target;
    public float DetectionRange = 10;
    public float MinimumRange = 2;
    public float JumpTriggerHeight = 2;

    private Movement movement;
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<Movement>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 destination = (Vector2)Target.transform.position - body.position;
        float distance = destination.magnitude;
        Vector2 direction = destination / distance;
        float heightDifference = Target.transform.position.y - body.position.y;

        if (distance < DetectionRange)
        {
            if (Mathf.Round(distance)>MinimumRange)
            {
                movement.Move(direction.x);
            }
            else
            {
                //This is so god damn beautiful!
                movement.Move((direction.x / MinimumRange)*(Mathf.Round(distance)-1));
            }

            if (heightDifference > JumpTriggerHeight)
            {
                movement.Jump();
            }
        }
    }
}