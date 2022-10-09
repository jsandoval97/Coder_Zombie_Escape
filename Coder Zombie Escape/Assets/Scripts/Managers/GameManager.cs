
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Starting,
    Playing,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static int score;
    public GameStates actualState { get; set; }

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            score = 0;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(GameStates.Playing);
        }
    }

    public void ChangeState(GameStates newState)
    {
        if (actualState != newState)
        {
            actualState = newState;
        }
    }
}
