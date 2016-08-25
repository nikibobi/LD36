using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MovingPlatform : MonoBehaviour {

    public Transform Platform;
    [Range(1, 10)]
    public float Speed;
    public Transform[] Path;

    private IEnumerator<Transform> points;
    private Rigidbody2D body;

    void Start()
    {
        points = GetInfinate().GetEnumerator();
        points.MoveNext();
        body = Platform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Platform.position = Vector2.MoveTowards(Platform.position, points.Current.position, Speed * Time.deltaTime)

        Vector2 destination = (Vector2)points.Current.position - (Vector2)body.position;
        float distance = destination.magnitude;
        Vector2 direction = destination / distance;

        body.velocity = direction * Speed;

        if (distance < Speed * Time.fixedDeltaTime)
        {
            points.MoveNext();
        }
    }

    private IEnumerable<Transform> GetInfinate()
    {
        while(true)
        {
            foreach (var point in Path)
            {
                yield return point;
            }
            foreach (var point in Path.Reverse())
            {
                yield return point;
            }
        }
    }
}
