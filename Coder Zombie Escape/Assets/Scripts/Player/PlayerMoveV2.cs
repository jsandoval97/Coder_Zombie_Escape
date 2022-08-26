
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveV2 : MonoBehaviour
{
    public float Horizontal;
    public float MaxLeft, MaxRight;
    public AnimationCurve jumpCurve;

    private bool Jumping = false;
    public float JumpScale = 5f;
    public float JumpDuration = 1f;
    public float Speed;

    float yOriginal;
    float yOffset;

    internal Transform tr; 
    // Start is called before the first frame update
    void Start()
    {
        tr = transform; 
        yOriginal = tr.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal += Input.GetAxis("Horizontal") * Speed * Time.deltaTime;

        if (!Jumping && Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(fly());
        }
        Horizontal = Mathf.Clamp(Horizontal, MaxLeft, MaxRight);
        tr.position = new Vector3(Horizontal, yOriginal, 0);
    }

    public IEnumerator fly ()
    {
        Jumping = true;
        float d = 0;
        while (d < JumpDuration)
        {
            d += Time.deltaTime;
            yOffset = jumpCurve.Evaluate(d / JumpDuration) * JumpScale;
            yield return null;
        }

        Jumping = false;
    }
}
