using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float liveTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyDelay", liveTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyDelay(){
        Destroy(gameObject);
    }
}
