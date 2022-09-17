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
    [SerializeField] private float speedMove;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float gravity = 20f;
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
        controllerRadius = characterController.radius;
        controllerHeight = characterController.height;
        controllerPositionY = characterController.center.y;
    }

    // Update is called once per frame
    void Update()
    {
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

            if (!isSliding && !isJumping)
            {
                playerAnimation.showAnimationRun();
            }

            if(directionInput == DirectionInput.Up)
            {
                verticalPosition = jumpForce;
                isJumping = true;
                playerAnimation.showAnimationJump();
                if(coroutineSlide != null)
                {
                    StopCoroutine(coroutineSlide);
                    isSliding = false;
                    ModifyColliderSlide(false);
                }
            }
            if (directionInput == DirectionInput.Down)
            {
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
        switch (lane)
        {
            case -1:
                LeftLaneLogic();
                break;
            case 0:
                MidLaneLogic();
                break;
            case 1:
                RightLaneLogic();
                break;

        }
    }

    private void MidLaneLogic()
    {
        if(transform.position.x > 0.1f)
        {
            HorizontalMove(0f, Vector3.left);
        }
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
        float horizontalPosition = Mathf.Abs(transform.position.x - posX);
        if(horizontalPosition > 0.1f)
        {
            desiredDirection = Vector3.Lerp(desiredDirection, directionMove * 20f, Time.deltaTime * 500f);
        }
        else
        {
            desiredDirection = Vector3.zero;
            transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
        }
    }

    private void Slide()
    {
        coroutineSlide = StartCoroutine(COSlide());
    }

    private IEnumerator COSlide()
    {
        isSliding = true;
        playerAnimation.showAnimationSlide();
        ModifyColliderSlide(true);
        yield return new WaitForSeconds(2f);
        isSliding = false; 
        ModifyColliderSlide(false);
    }

    private void ModifyColliderSlide (bool modify)
    {
        if(modify)
        {
            characterController.radius = 0.3f;
            characterController.height = 0.6f;
            characterController.center = new Vector3 (0f, 0f, 0f);
        }
        else
        {
            characterController.radius = controllerRadius;
            characterController.height = controllerHeight;
            characterController.center = new Vector3 (0f, controllerPositionY, 0f);
        }
    }

    private void DetectInput()
    {
        directionInput =DirectionInput.Null;
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            directionInput = DirectionInput.Left;
            lane--;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            directionInput = DirectionInput.Right;
            lane++;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            directionInput = DirectionInput.Down;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            directionInput = DirectionInput.Up;
        }

        lane = Mathf.Clamp(lane,-1,1);
    }

    
}
