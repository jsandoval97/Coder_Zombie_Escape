using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveForce : MonoBehaviour
{

    [SerializeField]
    private int jumpForce = 40;

    [SerializeField]
    [Range(1f, 20f)]
    private int delayNextJump = 1;

    [SerializeField]
    private Animator playerAnimator;


    public bool CanJump { get => canJump; set => canJump = value; }
    public Rigidbody MyRigidbody { get => myRigidbody; set => myRigidbody = value; }


    private bool canJump = true;
    private bool inDelayJump = false;
    private Vector3 playerDirection;
    private Rigidbody myRigidbody;

    void Start()
    {
        MyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            MyRigidbody.AddForce((Vector3.up + Vector3.forward), ForceMode.VelocityChange);
            canJump = false;
        }
    }

    private void FixedUpdate()
    {

        if (!canJump && !inDelayJump)
        {

            MyRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            inDelayJump = true;
            Invoke("DelayNextJump", delayNextJump);
        }

    }

    private void DelayNextJump()
    {
        inDelayJump = false;
        canJump = true;
    }

}