using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    //defino los carriles 
    [SerializeField]
    public enum Side { Left, Mid, Right}
    //selecciono el carril de preferencia
    public Side pSide = Side.Mid;
    //creo una variable para cambiar la posición de x y para su valor
    float newXPosition = 0f;
    public float xValue;
    //creo las variables para ingresar el movimiento izquierda y derecha
    public bool GoLeft;
    public bool GoRight; 
    //creo un CharacterController para mover al player 
    private CharacterController playerController;
    //creo un AnimatorController para animar al player
    [SerializeField] Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //obtengo el componente CharacterController
        playerController = GetComponent<CharacterController>();
        //transform.position = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        //defino el movimiento a la izquierda
        GoLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        if(GoLeft)
        {
            if(pSide == Side.Mid)
            {
                newXPosition = -xValue;
                pSide = Side.Left;
            }   else if(pSide == Side.Right)
                {
                    newXPosition = 0;
                    pSide = Side.Mid;
                }
        }
        //defino el movimiento a la derecha
        GoRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        if(GoRight)
        {
            if(pSide == Side.Mid)
            {
                newXPosition = xValue;
                pSide = Side.Right;
                
            }   else if(pSide == Side.Left)
                {
                    newXPosition = 0;
                    pSide = Side.Mid;
                    
                }
        }
        playerController.Move((newXPosition-transform.position.x) * Vector3.right);
        Move();
        //falta el jump y el roll
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
