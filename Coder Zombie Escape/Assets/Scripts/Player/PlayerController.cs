using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirectionInput
{
    Null,
    Up,
    Left,
    Right,
    Down
}

public class PlayerController : MonoBehaviour
{
    [Header ("Configuracion player")]
    [SerializeField] private float speedMove;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = 20f;

    [Header ("Configuracion carril")]
    [SerializeField] private float leftLanePos = -2f;
    [SerializeField] private float rightLanePos = 2f;

    public bool isJumping {get; private set;}
    public bool isSliding {get; private set;}

    private DirectionInput directionInput;
    private Coroutine coroutineSlide;
    private CharacterController characterController;
    private PlayerAnimation playerAnimation; 
    private float verticalPosition;
    private int lane; 
    private Vector3 desiredDirection;

    //Para el deslizamiento voy a modificar el Collider del Character Controller
    //para eso necesito crear las variables de radio, posicion y centro y.
    private float controllerRadius;
    private float controllerHeight;
    private float controllerPositionY;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Para el deslizamiento, defino los valores del Collider cuando NO me estoy deslizando 
        //esto me servirá para reestablecer el Collider 
        controllerRadius = characterController.radius;
        controllerHeight = characterController.height;
        controllerPositionY = characterController.center.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.actualState == GameStates.Starting || GameManager.instance.actualState == GameStates.GameOver)
        {
            return;
        }
        
        DetectInput();
        LaneControl();
        CalculateVerticalMovement();
        CharacterMove();
    }

    private void CharacterMove()
    {
        Vector3 newPos = new Vector3(desiredDirection.x,verticalPosition,speedMove);
        characterController.Move(newPos * Time.deltaTime);
    }

    private void CalculateVerticalMovement ()
    {
        if(characterController.isGrounded)
        {
            isJumping = false;
            verticalPosition = 0f;

            //Acá le indico que si no estoy saltando y no me estoy deslizando muestre la animación de correr
            if (!isSliding && !isJumping)
            {
                playerAnimation.showAnimationRun();
            }

            //Si detecto que cambia la dirección del input a Up cambio el valor por el de jumpForce
            if(directionInput == DirectionInput.Up)
            {
                verticalPosition = jumpForce;
                isJumping = true;
                playerAnimation.showAnimationJump();
                //reestablezco el collider para el salto
                if(coroutineSlide != null)
                {
                    StopCoroutine(coroutineSlide);
                    isSliding = false;
                    ModifyColliderSlide(false);
                }
            }

            //Si cambia la dirección del input a Down me delizo
            if (directionInput == DirectionInput.Down)
            {
                //verifico que no me este deslizando antes
                if(isSliding)
                {
                    return;
                }

                if(coroutineSlide != null)
                {
                    StopCoroutine(coroutineSlide);
                }

                Slide();
            }
            else
            {
                //si estoy en el aire e intento deslizarme "caigo" y realizo el deslizamiento
                if(directionInput == DirectionInput.Down)
                {
                    verticalPosition -= jumpForce;
                    Slide();
                }
            }
        }

        verticalPosition -= gravity * Time.deltaTime;
    }

    private void LaneControl()
    {
        //utilizo un switch para controlar en que carril me encuentro
        switch (lane)
        {
            case -1:
                //Mover izquierda
                LeftLaneLogic();
                break;
            case 0:
                MidLaneLogic();
                break;
            case 1:
                //Mover derecha
                RightLaneLogic();
                break;

        }
    }

    private void MidLaneLogic()
    {
        //primero tengo que saber desde donde vengo
        //me fijo si estoy en el carril derecho para moverme hacia el centro
        if(transform.position.x > 0.1f)
        {
            HorizontalMove(0f, Vector3.left);
        }
        //lo mismo para el otro lado
        else if(transform.position.x < -0.1f)
        {
            HorizontalMove(0f, Vector3.right);
        }
        else
        {
            desiredDirection = Vector3.zero;
        }
    }

    private void LeftLaneLogic()
    {
        HorizontalMove(leftLanePos, Vector3.left);
    }

    private void RightLaneLogic()
    {
        HorizontalMove(rightLanePos, Vector3.right);
    }

    private void HorizontalMove(float posX, Vector3 directionMove)
    {
        //utilizo valor absoluto porque trabajo con numeros negativos
        float horizontalPosition = Mathf.Abs(transform.position.x - posX);
        if(horizontalPosition > 0.1f)
        {
            desiredDirection = Vector3.Lerp(desiredDirection, directionMove * 20f, Time.deltaTime * 500f);
        }
        else
        {
            //detengo el movimiento una vez que llegue a donde quería
            desiredDirection = Vector3.zero;
            //reseteo la posicion
            transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
        }
    }

    //Creo un método para el deslizamiento que inicia la corrutina
    private void Slide()
    {
        coroutineSlide = StartCoroutine(COSlide());
    }

    //Creo una corrutina para el deslizamiento
    private IEnumerator COSlide()
    {
        isSliding = true;
        playerAnimation.showAnimationSlide();
        ModifyColliderSlide(true);
        yield return new WaitForSeconds(2f);
        isSliding = false; 
        ModifyColliderSlide(false);
    }

    //Creo un método para modificar el Collider en el deslizamiento
    private void ModifyColliderSlide (bool modify)
    {
        if(modify)
        {
            //modifico el collider
            characterController.radius = 0.3f;
            characterController.height = 0.6f;
            characterController.center = new Vector3 (0f, 0f, 0f);
        }
        else
        {
            //reestablezco los valores
            characterController.radius = controllerRadius;
            characterController.height = controllerHeight;
            characterController.center = new Vector3 (0f, controllerPositionY, 0f);
        }
    }


    //Creo un método para verificar si se está presionando alguna tecla
    private void DetectInput()
    {
        //En cada frame reseteo el valor
        directionInput =DirectionInput.Null;
        //Si presiono A o flecha derecha le resto un valor al carril actual e indico que la dirección es Left
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            directionInput = DirectionInput.Left;
            lane--;
        }
        //Si presiono D o flecha derecha le sumo un valor al carril actual e indico que la dirección es Right
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            directionInput = DirectionInput.Right;
            lane++;
        }
        //Si presiono S o flecha abajo indico que la dirección es Down
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            directionInput = DirectionInput.Down;
        }
        //Si presiono W o flecha arriba indico que la dirección es Up
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            directionInput = DirectionInput.Up;
        }
    //utilizo Mathf para evitar que el valor de carril actual sea superior a 1 o menor a -1
        lane = Mathf.Clamp(lane,-1,1);
    }

    
}
