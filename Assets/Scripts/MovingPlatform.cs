using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public GameObject Platform;

    [Range(1, 10)]
    public float Speed;

    public Transform[] Path;

    Transform CurrentNode;
    int NodeSelector,Direction=1,NodeCounter;
    
    void Start () {
        NodeCounter = Path.Length - 1;
    }

    void Update () {

        CurrentNode = Path[NodeSelector];

        Platform.transform.position = Vector2.MoveTowards(Platform.transform.position, CurrentNode.position, Speed * Time.deltaTime);

        if (Platform.transform.position == CurrentNode.position)
        {
            NodeSelector += Direction;
        }

        if (NodeSelector> NodeCounter)
        {
            NodeSelector = NodeCounter - 1;
            Direction = -1;
        }
        else if (NodeSelector<0)
        {
            NodeSelector = 1;
            Direction = 1;
        }
    }
}
