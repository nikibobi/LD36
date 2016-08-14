using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MovingPlatform : MonoBehaviour {

    public Transform Platform;
    [Range(1, 10)]
    public float Speed;
    public Transform[] Path;

    private IEnumerator<Transform> points;
    
    void Start()
    {
        points = GetInfinate().GetEnumerator();
        points.MoveNext();
    }

    void Update()
    {
        Platform.position = Vector2.MoveTowards(Platform.position, points.Current.position, Speed * Time.deltaTime);

        if(Platform.position == points.Current.position)
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
