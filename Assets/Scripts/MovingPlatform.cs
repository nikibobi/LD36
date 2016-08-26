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
    private Vector2 lastPointPosition;

    void Start()
    {
        points = GetInfinate().GetEnumerator();
        points.MoveNext();
        body = Platform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Platform.position = Vector2.MoveTowards(Platform.position, points.Current.position, Speed * Time.deltaTime)

        Vector2 difference = (Vector2)points.Current.position - (Vector2)body.position;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;

        Vector2 differenceToLast = lastPointPosition - (Vector2)body.position;
        float distanceToLast = differenceToLast.magnitude;

        Vector2 differenceBetween = lastPointPosition - (Vector2)points.Current.position;
        float distanceBetween = differenceBetween.magnitude;

        if (distance < distanceToLast)
        {
            body.velocity = direction * ((Speed * distance / distanceBetween) + 1f);
        }
        else if (distance > distanceToLast)
        {
            body.velocity = direction * ((Speed * distanceToLast / distanceBetween) + 1f);
        }

        if (distance < Speed * Time.fixedDeltaTime)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        lastPointPosition = points.Current.position;
        points.MoveNext();
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
