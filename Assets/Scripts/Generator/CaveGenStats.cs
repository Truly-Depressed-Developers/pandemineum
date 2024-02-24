using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generator {
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

    [Header("Enemies")]
    [Space(5)]
    public float troop_collision = 10f;
    [Min(6)] public int enemy_troop_amount = 15;
    public int min_enemy_per_troop = 3;
    public int max_enemy_per_troop = 8;
    public float troop_boundry_box_size = 9f;

    [Space(10)]
    [Header("Other Stats")]
    [Space(5)]
    public float min_kobalt_percentage = 0.12f;
    public float max_kobalt_percentage = 0.18f;
  }
}
