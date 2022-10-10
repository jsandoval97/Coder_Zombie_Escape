
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    //creo el array de los bloques
    public GameObject[] tilePrefabs;
    public float zSpawn = 0f;
    public float tileLength = 40;
    public int tilesNumber = 3;
    public float delaySpawn = 0.1f;
    public float intervalSpawn = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
                InvokeRepeating("SpawnTile", delaySpawn, intervalSpawn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnTile()
    {
        int tileIndex = Random.Range(1, tilePrefabs.Length);
        Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLength;
    }
}
