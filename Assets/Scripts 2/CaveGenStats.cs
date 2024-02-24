using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cave Type" , menuName = "new Cave")]
public class CaveGenStats : ScriptableObject
{
    [Header("Overall Size")]
    public int MinTunnels = 7;
    public int MaxTunnels = 13;
    public int Caverns = 6;

    [Header("Tunnels")]
    [Space(5)]
    public int MinSnakeLength = 10;
    public int MaxSnakeLength = 20;

    [Header("Caverns")]
    [Space(5)]
    public int MinCavernSize = 6;
    public int MaxCavernSize = 9;

    [Header("Average Size Test")]
    [Space(5)]
    public int MinAverage = 0;
    public int MaxAverage = 100000;
}
