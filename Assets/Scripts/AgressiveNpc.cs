using UnityEngine;
using System;

public class AgressiveNpc : MonoBehaviour {

    /* void OnTriggerEnter2D(Collider2D collider)
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
         if (instatiatedText) Destroy(GameObject.Find(prefab.name + ("(Clone)")));
     }*/
    [Range(0, 10)]
    public float Speed;
    [Range(0, 10)]
    public float MaxVelocityX;

    public bool isAwake;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {

        if (collider.name == "Player" && isAwake)
        {
            SnappyMove(playerDirection(collider.transform.position));
        }
    }

    private void SnappyMove(float direction)
    {
        //This is a Snappy movement system with almost pixel perfect movement:
        Vector2 currentVel = body.velocity;
        currentVel.x = direction * Speed;
        body.velocity = currentVel;
    }

    int playerDirection(Vector3 playerPossition)
    {
        if (playerPossition.x - transform.position.x < 0) return -1;
        else return 1;
    }
}