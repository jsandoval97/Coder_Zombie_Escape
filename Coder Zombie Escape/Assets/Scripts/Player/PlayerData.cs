using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private int score = 0;

    public int Score { get { return score; } }

    private int position = 0;
    public int Position { get { return position; } }

    /*public void Punctuation(int value)
    {
        Coins += value;
    }*/
    
    public void AddPoint(int value)
    {
        score += value;
    }

    public void Progress(int value)
    {
        position += value;
    }

    

    

}

