using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private int Coins = 0;

    public int Points { get { return Coins; } }

    private int position = 0;
    public int Position { get { return position; } }

    public void Punctuation(int value)
    {
        Coins += value;
    }

    public void Progress(int value)
    {
        position += value;
    }

}

