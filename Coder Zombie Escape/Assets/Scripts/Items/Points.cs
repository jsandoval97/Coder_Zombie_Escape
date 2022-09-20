using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private int points = 1;
    public int Point { get { return points; } }
}
