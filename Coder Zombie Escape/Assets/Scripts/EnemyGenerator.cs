using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float delaySpawn = 2f;
    public float intervalSpawn = 2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemySpawn", delaySpawn, intervalSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemySpawn()
    {
        int indexEnemy = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[indexEnemy], transform);
    }
}
