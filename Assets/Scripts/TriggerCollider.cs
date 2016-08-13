using UnityEngine;

public class TriggerCollider : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("its happening");
    }
}
