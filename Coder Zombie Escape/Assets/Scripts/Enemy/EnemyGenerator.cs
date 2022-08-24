using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject Enemy;

    [SerializeField]
    private int delaySpawn = 2;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", delaySpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        Instantiate (Enemy, transform);
    }
}
