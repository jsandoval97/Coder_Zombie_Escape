
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV3 : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    public float forwardSpeed = 5f;

    private int desiredLane = 1; //0 es izquierda, 1 es medio, 2 es derecha
    public float laneDistance = 2; //distancia entre líneas

    public float jumpForce;
    public float Gravity = -20;

    [SerializeField] Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move.z = forwardSpeed; 

        move.y += Gravity * Time.deltaTime;

        if(controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {

            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        //transform.position = targetPosition;
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(move * Time.deltaTime);
    }

    /*private void FixedUpdate() 
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }*/

    private void Jump()
    {
        move.y = jumpForce;
    }
}
