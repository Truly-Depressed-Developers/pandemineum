using System;
using UnityEngine;

[CreateAssetMenu]
public class CardStatistics : ScriptableObject {
  // Chosen statistic
  public EntityType entityType;
  public PlayerStatistics playerStatistic;
  public EnemyStatistics enemyStatistic;
  public BuffType buffType;
  public int value;
  //player Tresholds

  private int playerHealthThreshhold = 5;
  private int playerArmorThreshhold = 5;
  private int playerDamageThreshhold = 5;
  private int playerSpeedThreshhold = 5;
  private int playerReloadSpeedThreshhold = 5;
  private int playerShotRangeThreshhold = 5;
  private int playerSightRangeThreshhold = 5;
  private int playerLuckThreshhold = 5;
  private int playerCobaltPickRateThreshhold = 5;
  //Enemies Tresholds
  private int enemyHealthThreshhold = 5;
  private int enemyArmorThreshhold = 5;
  private int enemyDamageThreshhold = 5;
  private int enemySpeedThreshhold = 5;
  private int enemyDropRateThreshhold = 5;
  private int enemyShotRangeThreshhold = 5;
  // Operational variables
  private float probability = 0.5f;
  // Images
  public Sprite sprite = null;

  void OnEnable() {
    this.drawStatistics();
  }

  private void loadBackupImage() {
    this.sprite = (Sprite)Resources.Load("Cards/good_card", typeof(Sprite));
  }

  private Sprite loadSprite(string name) {
    return (Sprite)Resources.Load("Cards/" + name, typeof(Sprite));
  }

  private void drawStatistics() {
    // Entity type
    this.entityType = UnityEngine.Random.value <= this.probability ? EntityType.Player : EntityType.Enemy;

    // Statistic
    PlayerStatistics[] allValues = (PlayerStatistics[])Enum.GetValues(typeof(PlayerStatistics));
    this.playerStatistic = allValues[UnityEngine.Random.Range(0, allValues.Length)];

    EnemyStatistics[] allEnemyValues = (EnemyStatistics[])Enum.GetValues(typeof(EnemyStatistics));
    this.enemyStatistic = allEnemyValues[UnityEngine.Random.Range(0, allEnemyValues.Length)];

    // Buff or debuff
    this.buffType = UnityEngine.Random.value <= this.probability ? BuffType.Buff : BuffType.Debuff;

    // Value of the buff or debuff
    if (this.entityType == EntityType.Player) {
      if (this.buffType == BuffType.Buff) {
        switch (this.playerStatistic) {
          case PlayerStatistics.Health: {
              this.value = UnityEngine.Random.Range(1, this.playerHealthThreshhold);
              this.sprite = this.loadSprite("player_health");
              break;
            };
          case PlayerStatistics.Armor: {
              this.value = UnityEngine.Random.Range(1, this.playerArmorThreshhold);
              this.sprite = this.loadSprite("player_armor");
              break;
            };
          case PlayerStatistics.Damage: {
              this.value = UnityEngine.Random.Range(1, this.playerDamageThreshhold);
              this.sprite = this.loadSprite("player_damage");
              break;
            };
          case PlayerStatistics.Speed: {
              this.value = UnityEngine.Random.Range(1, this.playerSpeedThreshhold);
              this.sprite = this.loadSprite("player_speed");
              break;
            };
          case PlayerStatistics.ReloadSpeed: {
              this.value = -UnityEngine.Random.Range(1, this.playerReloadSpeedThreshhold);
              this.sprite = this.loadSprite("player_reloasd_speed");
              break;
            };
          case PlayerStatistics.ShotRange: {
              this.value = UnityEngine.Random.Range(1, this.playerShotRangeThreshhold);
              this.sprite = this.loadSprite("player_shot_range");
              break;
            };
          case PlayerStatistics.SightRange: {
              this.value = UnityEngine.Random.Range(1, this.playerSightRangeThreshhold);
              this.sprite = this.loadSprite("player_sight_range");
              break;
            };
          case PlayerStatistics.Luck: {
              this.value = UnityEngine.Random.Range(1, this.playerLuckThreshhold);
              this.sprite = this.loadSprite("player_card");
              break;
            };
          case PlayerStatistics.CobaltPickRate: {
              this.value = UnityEngine.Random.Range(1, this.playerCobaltPickRateThreshhold);
              this.sprite = this.loadSprite("player_cobalt_pick_rate");
              break;
            };
        }
      } else {
        switch (this.playerStatistic) {
          case PlayerStatistics.Health: {
              this.value = -UnityEngine.Random.Range(1, this.playerHealthThreshhold);
              this.sprite = this.loadSprite("player_health");
              break;
            };
          case PlayerStatistics.Armor: {
              this.value = -UnityEngine.Random.Range(1, this.playerArmorThreshhold);
              this.sprite = this.loadSprite("player_armor");
              break;
            };
          case PlayerStatistics.Damage: {
              this.value = -UnityEngine.Random.Range(1, this.playerDamageThreshhold);
              this.sprite = this.loadSprite("player_damage");
              break;
            };
          case PlayerStatistics.Speed: {
              this.value = -UnityEngine.Random.Range(1, this.playerSpeedThreshhold);
              this.sprite = this.loadSprite("player_speed");
              break;
            };
          case PlayerStatistics.ReloadSpeed: {
              this.value = UnityEngine.Random.Range(1, this.playerReloadSpeedThreshhold);
              this.sprite = this.loadSprite("player_reload_speed");
              break;
            };
          case PlayerStatistics.ShotRange: {
              this.value = -UnityEngine.Random.Range(1, this.playerShotRangeThreshhold);
              this.sprite = this.loadSprite("player_shot_range");
              break;
            };
          case PlayerStatistics.SightRange: {
              this.value = -UnityEngine.Random.Range(1, this.playerSightRangeThreshhold);
              this.sprite = this.loadSprite("player_sight_range");
              break;
            };
          case PlayerStatistics.Luck: {
              this.value = -UnityEngine.Random.Range(1, this.playerLuckThreshhold);
              this.sprite = this.loadSprite("player_card");
              break;
            };
        }
      }
    } else {
      if (this.buffType == BuffType.Buff) {
        switch (this.enemyStatistic) {
          case EnemyStatistics.Health: {
              this.value = -UnityEngine.Random.Range(1, this.enemyHealthThreshhold);
              this.sprite = this.loadSprite("enemy_health");
              break;
            };
          case EnemyStatistics.Armor: {
              this.value = -UnityEngine.Random.Range(1, this.enemyArmorThreshhold);
              this.sprite = this.loadSprite("enemy_armor");
              break;
            };
          case EnemyStatistics.Damage: {
              this.value = -UnityEngine.Random.Range(1, this.enemyDamageThreshhold);
              this.sprite = this.loadSprite("enemy_damage");
              break;
            };
          case EnemyStatistics.Speed: {
              this.value = -UnityEngine.Random.Range(1, this.enemySpeedThreshhold);
              this.sprite = this.loadSprite("enemy_speed");
              break;
            };
          case EnemyStatistics.ShotRange: {
              this.value = -UnityEngine.Random.Range(1, this.enemyShotRangeThreshhold);
              this.sprite = this.loadSprite("enemy_card");
              break;
            };
          case EnemyStatistics.DropRate: {
              this.value = UnityEngine.Random.Range(1, this.enemyDropRateThreshhold);
              this.sprite = this.loadSprite("enemy_droprate");
              break;
            };
        }
      } else {
        switch (this.enemyStatistic) {
          case EnemyStatistics.Health: {
              this.value = UnityEngine.Random.Range(1, this.enemyHealthThreshhold);
              this.sprite = this.loadSprite("enemy_health");
              break;
            };
          case EnemyStatistics.Armor: {
              this.value = UnityEngine.Random.Range(1, this.enemyArmorThreshhold);
              this.sprite = this.loadSprite("enemy_armor");
              break;
            };
          case EnemyStatistics.Damage: {
              this.value = UnityEngine.Random.Range(1, this.enemyDamageThreshhold);
              this.sprite = this.loadSprite("enemy_damage");
              break;
            };
          case EnemyStatistics.Speed: {
              this.value = UnityEngine.Random.Range(1, this.enemySpeedThreshhold);
              this.sprite = this.loadSprite("enemy_speed");
              break;
            };
          case EnemyStatistics.ShotRange: {
              this.value = UnityEngine.Random.Range(1, this.enemyShotRangeThreshhold);
              this.sprite = this.loadSprite("enemy_card");
              break;
            };
          case EnemyStatistics.DropRate: {
              this.value = -UnityEngine.Random.Range(1, this.enemyDropRateThreshhold);
              this.sprite = this.loadSprite("enemy_droprate");
              break;
            };
        }
      }
    }
    Debug.Log(this.sprite);
  }
}
