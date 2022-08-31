
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int score; 

    public static GameManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            score = 0;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
