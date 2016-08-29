using System.Collections;
using UnityEngine;

class Spawner : MonoBehaviour
{
    public GameObject Entity;
    public Transform[] SpawnPoints;
    public float SpawnInterval = 0.5f;

    private int wave;

    void Start()
    {
    }

    void Update()
    {
        if (GameObject.FindWithTag("Enemy NPC") == null)
        {
            StartCoroutine(NextWave());
        }
    }

    private IEnumerator NextWave()
    {
        wave += 1;
        GetComponent<AudioSource>().Play();

        for (int i = 0; i < wave; i++)
        {
            Instantiate(Entity, SpawnPoints[i % SpawnPoints.Length].position, Quaternion.identity);
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
}
