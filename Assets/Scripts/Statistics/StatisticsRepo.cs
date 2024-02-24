using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class StatisticsRepo : MonoSingleton<StatisticsRepo> {


  public int playerHealthMaxMul { get; set; } = 1;
  public int playerArmorAdd { get; set; } = 0;
  public int playerDamageAdd { get; set; } = 0;
  public float playerSpeedMul { get; set; } = 1; // moved
  public int playerReloadSpeedMul { get; set; } = 1;
  public int playerShotRangeMul { get; set; } = 1;
  public int playerSightRangeMul { get; set; } = 1;
  public int playerLuckMul { get; set; } = 1;
  public int playerCobaltPickRateMul { get; set; } = 1;

  //Enemies 
  public int enemyHealthMaxMul { get; set; } = 1;
  public int enemyArmorAdd { get; set; } = 0;
  public int enemyDamageAdd { get; set; } = 0;
  public int enemySpeedMul { get; set; } = 1;
  public int enemyDropRateMul { get; set; } = 1;
  public int enemyAttackRangeMul { get; set; } = 1;
}
