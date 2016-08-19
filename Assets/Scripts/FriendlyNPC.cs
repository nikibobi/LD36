using UnityEngine;

public class FriendlyNPC : MonoBehaviour
{
    public GameObject Target;
    public float DetectionRange = 10;
    public float MinimumRange = 2;
    public float JumpTriggerHeight = 2;

    private Moving mover;
    private Rigidbody2D body;

    // Use this for initialization
    void Start()
    {
        mover = GetComponent<Moving>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 destination = (Vector2)Target.transform.position - body.position;
        float distance = destination.magnitude;
        Vector2 direction = destination / distance;
        bool inMovementRange = distance < DetectionRange && distance > MinimumRange;
        float heightDifference = Target.transform.position.y - body.position.y;

        if (inMovementRange)
        {
            mover.Move(direction.x);

            if (heightDifference > JumpTriggerHeight)
            {
                mover.Jump();
            }
        }
    }
}