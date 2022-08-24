using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField]
    [Range(1f, 10f)]
    private float speed = 2f;
    enum ZombieTypes { Crawler, Stalker, Rioter };
    [SerializeField] ZombieTypes zombieType;
    [SerializeField] Transform playerTransform;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (zombieType)
        {
            case ZombieTypes.Crawler:
                MoveForward();
                break;
            case ZombieTypes.Stalker:
                ChasePlayer();
                break;
            case ZombieTypes.Rioter:
                RotateAroundPlayer();
                break;
        }
    }

 
    private void RotateAroundPlayer()
    {
        LookPlayer();
        transform.RotateAround(playerTransform.position, Vector3.up, 2f * Time.deltaTime);
    }

    private void ChasePlayer()
    {
        LookPlayer();
        Vector3 direction = (playerTransform.position - transform.position);
        if (direction.magnitude > 1f)
        {
           transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    private void LookPlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1.5f * Time.deltaTime);
    }
}
