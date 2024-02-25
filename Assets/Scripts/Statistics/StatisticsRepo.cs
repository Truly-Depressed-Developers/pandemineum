using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Statistics {
  public class StatisticsRepo : MonoSingleton<StatisticsRepo> {
    public float PlayerHealthMaxMul { get; set; } = 1;
    public float PlayerArmorAdd { get; set; } = 0;
    public float PlayerDamageAdd { get; set; } = 0;
    public float PlayerSpeedMul { get; set; } = 1;
    public float PlayerReloadSpeedMul { get; set; } = 1;
    public float PlayerShotRangeMul { get; set; } = 1;
    public float PlayerSightRangeMul { get; set; } = 1;
    public float PlayerLuckMul { get; set; } = 1;
    public float PlayerCobaltPickRateMul { get; set; } = 1;

    //Enemies 
    public float EnemyHealthMaxMul { get; set; } = 1;
    public float EnemyArmorAdd { get; set; } = 0;
    public float EnemyDamageAdd { get; set; } = 0;
    public float EnemySpeedMul { get; set; } = 1;
    public float EnemyDropRateMul { get; set; } = 1;
    public float EnemyAttackRangeMul { get; set; } = 1;
  }
}
