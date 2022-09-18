
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class PlayerCollision : MonoBehaviour
{

    private PlayerData playerData;
    private PlayerAnimation playerAnimation; 
    public static event Action OnDead;


    void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        playerData = GetComponent<PlayerData>();
    }



    private void OnCollisionEnter(Collision other)
    {
        


    }

    private void OnControllerColliderHit(ControllerColliderHit hit) 
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            Debug.Log("GameOver");
            playerAnimation.showAnimationHit();
            HUDManager.Instance.RestarVida();
            PlayerCollision.OnDead?.Invoke();
            
        }

        else
        {
            //Debug.Log("Entrando en colisión con -> " + hit.gameObject.name);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Points"))
        {
            Destroy(other.gameObject);
            playerData.Punctuation(other.gameObject.GetComponent<Points>().urnPoints);

            GameManager.score++;
            Debug.Log(GameManager.score);
            HUDManager.Instance.AddPoint();
        }
    }
    
}
