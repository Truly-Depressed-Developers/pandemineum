using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class StatisticsRepo : MonoSingleton<MouseLocation> {

  public int playerHealth { get; set; } = 150;
  public int playerHealthMax { get; set; } = 200;
  public int playerArmor { get; set; } = 30;
  public int playerDamage { get; set; } = 50;
  public float playerSpeed { get; set; } = 2f; // moved
  public int playerReloadSpeed { get; set; } = 2000;
  public int playerShotRange { get; set; } = 200;
  public int playerSightRange { get; set; } = 300;
  public int playerLuck { get; set; } = 1;

  //Enemies 
  public int enemyHealth { get; set; } = 150;
  public int enemyArmor { get; set; } = 20;
  public int enemyDamage { get; set; } = 30;
  public int enemySpeed { get; set; } = 40;
  public int enemyDropRate { get; set; } = 20;
  public int enemyShotRange { get; set; } = 150;

  // Singleton stuff
  private static StatisticsRepo instance;
  public static StatisticsRepo Instance { get { return instance; } }
}
