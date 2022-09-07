using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileExit : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    [SerializeField] 
    [Range(1f,20f)]
    private float rayDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TileExitRaycast();
    }

    private void TileExitRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(startPoint.position, endPoint.transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            if(hit.transform.CompareTag("Player"))
            {
                Debug.Log("Fin del bloque");
            }
        }

        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startPoint.position, endPoint.position);
    }
}
