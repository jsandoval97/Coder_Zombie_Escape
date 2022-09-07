
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerData playerData;


    // Start is called before the first frame update
    private void Start()
    {
        playerData = GetComponent<PlayerData>();
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

        if (other.gameObject.CompareTag("Points"))
        {
            Destroy(other.gameObject);
            playerData.Punctuation(other.gameObject.GetComponent<Points>().urnPoints);

            GameManager.score++;
            Debug.Log(GameManager.score);
        }

        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            playerData.Progress(other.gameObject.GetComponent<CheckPoint>().checkPoint);
            HUDManager.SetProgressBar(playerData.Position);
        }
    }

}
