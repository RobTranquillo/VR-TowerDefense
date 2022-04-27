using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int count;
    public float spawnDelay = 1f;

    void Start()
    {
        float delay = 0;
        for (int i = 0; i < count; i++)
        {
            Invoke("Spawn", delay);
            delay += spawnDelay;
        }
    }

    private void Spawn()
    {
        GameObject foo = Instantiate(enemyPrefab, transform);
        foo.name += "_" + count--;
    }
}
