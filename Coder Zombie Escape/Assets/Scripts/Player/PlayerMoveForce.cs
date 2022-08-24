using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveForce : MonoBehaviour
{

    [SerializeField]
    private float jumpHeight = 4f;

    //[SerializeField]
    //private bool isGrounded;

    [SerializeField]
    Rigidbody MyRigidBody;

    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody>();
       // isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
    }

    private void jump()
    {
        MyRigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

}
