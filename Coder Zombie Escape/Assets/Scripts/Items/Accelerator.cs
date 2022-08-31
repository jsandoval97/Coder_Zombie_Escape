using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{

    [SerializeField]
    private int SpeedUp = 2;

    [SerializeField]
    private bool MaxSpeed = true;


    private void OnTriggerEnter(Collider other)
    {

        if (MaxSpeed == true)
        {
            Run(other); 
        }

        else
        {
            NormalSpeed(other);
        }

    }



    private void NormalSpeed(Collider other)
    {
        other.GetComponent<PlayerMovement>().speed /= SpeedUp;
    }

    private void Run(Collider other)
    {
        other.GetComponent<PlayerMovement>().speed *= SpeedUp;
    }

    private void ResetSpeed()
    {
        MaxSpeed = true;
    }

}
