using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsRepo : MonoBehaviour
{

  private int playerHealth { get; set; }
  private int playerArmor { get; set; }
  private int playerDamage { get; set; }
  private int playerSpeed { get; set; }
  private int playerReloadSpeed { get; set; }
  private int playerShotRange { get; set; }
  private int playerSightRange { get; set; }
  private int playerLuck { get; set; }
  //Enemies 
  private int enemyHealth { get; set; }
  private int enemyArmor { get; set; }
  private int enemyDamage { get; set; }
  private int enemySpeed { get; set; }
  private int enemyDropRate { get; set; }
  private int enemyShotRange { get; set; }


  private static StatisticsRepo instance;

  public static StatisticsRepo Instance { get { return instance; } }
  private void constr() 
  {
    this.playerHealth = 200;
    this.playerArmor = 30;
    this.playerDamage = 50;
    this.playerSpeed = 50;
    this.playerReloadSpeed = 2000;
    this.playerShotRange = 200;
    this.playerSightRange = 300;
    this.playerLuck = 1;
    this.enemyHealth = 150;
    this.enemyArmor = 20;
    this.enemyDamage = 30;
    this.enemySpeed = 40;
    this.enemyShotRange = 150;
    this.enemyDropRate = 20;
  }

  private void Awake() {
    if (instance != null && instance != this) {
      Destroy(this.gameObject);
    } else {
      constr();
      instance = this;
    }
  }
}
