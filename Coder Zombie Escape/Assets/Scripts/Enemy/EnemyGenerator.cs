
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private Transform appearPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] 
    [Range(1f,20f)]
    private float rayDistance;

    [SerializeField] private GameObject Enemy;

    [SerializeField]
    private int delaySpawn = 2;
    


    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyGeneratorRaycast();
    }

    void SpawnEnemy()
    {
        Instantiate (Enemy, transform);
    }

    private void EnemyGeneratorRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(appearPoint.position, endPoint.transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            if(hit.transform.CompareTag("Player"))
            {
                Debug.Log("Colisión con player");
                Invoke("SpawnEnemy", delaySpawn);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Vector3 direction = appearPoint.transform.TransformDirection(Vector3.forward) * rayDistance;
        Gizmos.DrawLine(appearPoint.position, endPoint.position);
    }
}
