
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float cameraAxisX = 0f;
    public float rotationSensibility = 0.5f;
    public float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        MoveAxis();
    }

    private void MoveAxis()
    {
        float ejeHorizontal = Input.GetAxisRaw("Horizontal");
        float ejeVertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(ejeHorizontal, 0, ejeVertical);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void RotatePlayer()
    {
        cameraAxisX += Input.GetAxis("Mouse X");
        //metodo 1 de rotación
        //transform.rotation = Quaternion.Euler(0,cameraAxisX * rotationSensibility,0);
        //metodo 2 de rotación
        Quaternion newRotation = Quaternion.Euler(0,cameraAxisX,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }

}
