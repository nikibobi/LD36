using System;
using UnityEngine;

public class TriggerCollider : MonoBehaviour {

    public Transform prefab;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!GameObject.Find(prefab.name))
        Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        Destroy(GameObject.Find(prefab.name + "(Clone)"));
    }
}
