using UnityEngine;

public class TriggerCollider : MonoBehaviour {

    public GameObject prefab;
    //bool spawned = false;
    public Vector2 offset;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
            Instantiate(prefab, (Vector2)transform.position + offset, gameObject.transform.rotation);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        GameObject clone = GameObject.Find(prefab.name + "(Clone)");
        if (clone != null)
        {
            Destroy(clone);
        }
    }
}
