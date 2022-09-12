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
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float leftLanePos = -2f;
    [SerializeField] private float rightLanePos = 2f;

    private DirectionInput directionInput;
    private CharacterController characterController;
    private float verticalPosition;
    private int lane; 
    private Vector3 desiredDirection;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
            verticalPosition = 0f;
        }

        if(directionInput == DirectionInput.Up)
        {
            //salto
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
