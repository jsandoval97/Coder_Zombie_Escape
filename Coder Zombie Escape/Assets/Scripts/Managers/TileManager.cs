
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
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tilesNumber; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(1, tilePrefabs.Length));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnTile(int tileIndex)
    {
        Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLength;
    }
}
