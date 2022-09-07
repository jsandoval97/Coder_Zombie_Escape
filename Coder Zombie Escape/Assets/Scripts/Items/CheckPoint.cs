using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    private int point = 10;
    public int checkPoint { get { return point; } }
}
