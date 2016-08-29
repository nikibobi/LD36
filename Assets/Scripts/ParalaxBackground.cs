using UnityEngine;
using System.Collections;

public class ParalaxBackground : MonoBehaviour {

    public Transform CenterObject;

    [Range(1, 2)]
    public float ParallaxRate;

    private Vector2 centerObjectStart;
    private Vector2 imageStart;

    void Start()
    {
        centerObjectStart = CenterObject.position;
        imageStart = this.gameObject.transform.position;
    }

    void Update()
    {
        Vector2 offset = (centerObjectStart - (Vector2)CenterObject.position);
        this.gameObject.transform.position = imageStart - (offset / ParallaxRate);
    }
}
