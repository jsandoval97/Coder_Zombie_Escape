
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameras;
    int activeCamera;
    // Start is called before the first frame update
    void Start()
    {
         ChangeCamera(cameras.Length-1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            ChangeCamera(activeCamera);
        }
        
    }

    void ChangeCamera(int camera)
    {
        activeCamera = (camera +1) % cameras.Length;

        for(int i = 0; i< cameras.Length; i++)
        {
            if (i == activeCamera)
            {
                cameras[i].SetActive(true);
            }
            else
            {
                cameras[i].SetActive(false);
            }
        }
    }
}
