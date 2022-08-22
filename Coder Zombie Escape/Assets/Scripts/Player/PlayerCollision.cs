using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerMoveForce playerMove;

        private void Start()
    {
        playerMove = GetComponent<PlayerMoveForce>();
    }
    
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Zombie aparece");
        }
        else
        {
            Debug.Log("Entrando en colisión con -> " + other.gameObject.name);
        }

    }
}
