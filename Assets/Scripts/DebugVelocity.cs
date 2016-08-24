using UnityEngine;

public class DebugVelocity : MonoBehaviour
{
    public Color LineColor = Color.green;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.DrawLine(body.position, body.position + body.velocity, LineColor);
    }
}
