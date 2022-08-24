using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private int Coins = 0;

    public int Points { get { return Coins; } }

    public void Punctuation(int value)
    {
        Coins += value;
    }
}

