using System;
using UnityEngine;

public class TriggerCollider : MonoBehaviour {

    public Transform prefab;
    bool instatiatedText = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            Vector3 possition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
            if (!GameObject.Find(prefab.name))
                Instantiate(prefab, possition, gameObject.transform.rotation);
            instatiatedText = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if(instatiatedText) Destroy(GameObject.Find(prefab.name + ("(Clone)")));
    }
}
