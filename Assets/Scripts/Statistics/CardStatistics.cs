using System;
using UnityEngine;

namespace Statistics {
  public class CardStatistics : ScriptableObject {
    public PlayerStatistics playerStatistic;
    public EnemyStatistics enemyStatistic;
    public EntityType entityType;
    public BuffType buffType;
    public float value;
    public Sprite sprite;

    // Structure: (buffMin, buffMax, debuffMin, debuffMax)
    // Player 
    private readonly Vector4 playerHealthBounds = new(15, 25, 20, 40);
    private readonly Vector4 playerArmorBounds = new(5, 15, 5, 15); // base 0
    private readonly Vector4 playerDamageBounds = new(2, 5, 3, 6); // base 10
    private readonly Vector4 playerSpeedBounds = new(10, 20, 15, 30);
    private readonly Vector4 playerReloadSpeedBounds = new(15, 25, 20, 35);
    private readonly Vector4 playerShotRangeBounds = new(10, 25, 15, 30);
    private readonly Vector4 playerSightRangeBounds = new(10, 20, 15, 30);
    private readonly Vector4 playerLuckBounds = new(10, 20, 10, 20);
    private readonly Vector4 playerCobaltPickRateBounds = new(10, 20, 15, 30);

    // Enemy
    private readonly Vector4 enemyHealthBounds = new(15, 25, 25, 40);
    private readonly Vector4 enemyArmorBounds = new(2, 4, 15, 35); // base 0
    private readonly Vector4 enemyDamageBounds = new(10, 20, 20, 40); // base 15
    private readonly Vector4 enemySpeedBounds = new(5, 15, 15, 25);
    private readonly Vector4 enemyDropRateBounds = new(10, 25, 20, 45);
    private readonly Vector4 enemyShotRangeBounds = new(10, 25, 20, 40);

    private Sprite LoadSprite(string path) {
      return (Sprite)Resources.Load("Cards/" + path, typeof(Sprite));
    }

    public void Randomize(BuffType flavour) {
      // Entity type
      entityType = UnityEngine.Random.value <= 0.5f ? EntityType.Player : EntityType.Enemy;

      // Statistic
      PlayerStatistics[] allValues = (PlayerStatistics[])Enum.GetValues(typeof(PlayerStatistics));
      playerStatistic = allValues[UnityEngine.Random.Range(0, allValues.Length)];

      EnemyStatistics[] allEnemyValues = (EnemyStatistics[])Enum.GetValues(typeof(EnemyStatistics));
      enemyStatistic = allEnemyValues[UnityEngine.Random.Range(0, allEnemyValues.Length)];

      buffType = flavour;

      // Value of the buff or debuff
      if (entityType == EntityType.Player) {
        if (buffType == BuffType.Buff) {
          switch (playerStatistic) {
            case PlayerStatistics.Health: {
              value = UnityEngine.Random.Range(playerHealthBounds.x, playerHealthBounds.y);
              sprite = LoadSprite("player_health");
              break;
            }
            case PlayerStatistics.Armor: {
              value = UnityEngine.Random.Range(playerArmorBounds.x, playerArmorBounds.y);
              sprite = LoadSprite("player_armor");
              break;
            }
            case PlayerStatistics.Damage: {
              value = UnityEngine.Random.Range(playerDamageBounds.x, playerDamageBounds.y);
              sprite = LoadSprite("player_damage");
              break;
            }
            case PlayerStatistics.Speed: {
              value = UnityEngine.Random.Range(playerSpeedBounds.x, playerSpeedBounds.y);
              sprite = LoadSprite("player_speed");
              break;
            }
            case PlayerStatistics.ReloadSpeed: {
              value = -UnityEngine.Random.Range(playerReloadSpeedBounds.x, playerReloadSpeedBounds.y);
              sprite = LoadSprite("player_reload_speed");
              break;
            }
            case PlayerStatistics.ShotRange: {
              value = UnityEngine.Random.Range(playerShotRangeBounds.x, playerShotRangeBounds.y);
              sprite = LoadSprite("player_shot_range");
              break;
            }
            case PlayerStatistics.SightRange: {
              value = UnityEngine.Random.Range(playerSightRangeBounds.x, playerSightRangeBounds.y);
              sprite = LoadSprite("player_sight_range");
              break;
            }
            case PlayerStatistics.Luck: {
              value = UnityEngine.Random.Range(playerLuckBounds.x, playerLuckBounds.y);
              sprite = LoadSprite("player_card");
              break;
            }
            case PlayerStatistics.CobaltPickRate: {
              value = UnityEngine.Random.Range(playerCobaltPickRateBounds.x, playerCobaltPickRateBounds.y);
              sprite = LoadSprite("player_cobalt_pick_rate");
              break;
            }
          }
        } else {
          switch (playerStatistic) {
            case PlayerStatistics.Health: {
             value = -UnityEngine.Random.Range(playerHealthBounds.z, playerHealthBounds.w);
             sprite = LoadSprite("player_health");
              break;
            }
            case PlayerStatistics.Armor: {
              value = -UnityEngine.Random.Range(playerArmorBounds.z, playerArmorBounds.w);
              sprite = LoadSprite("player_armor");
              break;
            }
            case PlayerStatistics.Damage: {
              value = -UnityEngine.Random.Range(playerDamageBounds.z, playerDamageBounds.w);
              sprite = LoadSprite("player_damage");
              break;
            }
            case PlayerStatistics.Speed: {
              value = -UnityEngine.Random.Range(playerSpeedBounds.z, playerSpeedBounds.w);
              sprite = LoadSprite("player_speed");
              break;
            }
            case PlayerStatistics.ReloadSpeed: {
              value = UnityEngine.Random.Range(playerReloadSpeedBounds.z, playerReloadSpeedBounds.w);
              sprite = LoadSprite("player_reload_speed");
              break;
            }
            case PlayerStatistics.ShotRange: {
              value = -UnityEngine.Random.Range(playerShotRangeBounds.z, playerShotRangeBounds.w);
              sprite = LoadSprite("player_shot_range");
              break;
            }
            case PlayerStatistics.SightRange: {
              value = -UnityEngine.Random.Range(playerSightRangeBounds.z, playerSightRangeBounds.w);
              sprite = LoadSprite("player_sight_range");
              break;
            }
            case PlayerStatistics.Luck: {
              value = -UnityEngine.Random.Range(playerLuckBounds.z, playerLuckBounds.w);
              sprite = LoadSprite("player_card");
              break;
            }
            case PlayerStatistics.CobaltPickRate: {
              value = -UnityEngine.Random.Range(playerCobaltPickRateBounds.z, playerCobaltPickRateBounds.w);
              sprite = LoadSprite("player_cobalt_pick_rate");
              break;
            }
          }
        }
      } else {
        if (buffType == BuffType.Buff) {
          switch (enemyStatistic) {
            case EnemyStatistics.Health: {
              value = -UnityEngine.Random.Range(enemyHealthBounds.x, enemyHealthBounds.y);
              sprite = LoadSprite("enemy_health");
              break;
            }
            case EnemyStatistics.Armor: {
              value = -UnityEngine.Random.Range(enemyArmorBounds.x, enemyArmorBounds.y);
              sprite = LoadSprite("enemy_armor");
              break;
            }
            case EnemyStatistics.Damage: {
              value = -UnityEngine.Random.Range(enemyDamageBounds.x, enemyDamageBounds.y);
              sprite = LoadSprite("enemy_damage");
              break;
            }
            case EnemyStatistics.Speed: {
              value = -UnityEngine.Random.Range(enemySpeedBounds.x, enemySpeedBounds.y);
              sprite = LoadSprite("enemy_speed");
              break;
            }
            case EnemyStatistics.ShotRange: {
              value = -UnityEngine.Random.Range(enemyShotRangeBounds.x, enemyShotRangeBounds.y);
              sprite = LoadSprite("enemy_card");
              break;
            }
            case EnemyStatistics.DropRate: {
              value = UnityEngine.Random.Range(enemyDropRateBounds.x, enemyDropRateBounds.y);
              sprite = LoadSprite("enemy_droprate");
              break;
            }
          }
        } else {
          switch (enemyStatistic) {
            case EnemyStatistics.Health: {
              value = UnityEngine.Random.Range(enemyHealthBounds.z, enemyHealthBounds.w);
              sprite = LoadSprite("enemy_health");
              break;
            }
            case EnemyStatistics.Armor: {
              value = UnityEngine.Random.Range(enemyArmorBounds.z, enemyArmorBounds.w);
              sprite = LoadSprite("enemy_armor");
              break;
            }
            case EnemyStatistics.Damage: {
              value = UnityEngine.Random.Range(enemyDamageBounds.z, enemyDamageBounds.w);
              sprite = LoadSprite("enemy_damage");
              break;
            }
            case EnemyStatistics.Speed: {
              value = UnityEngine.Random.Range(enemySpeedBounds.z, enemySpeedBounds.w);
              sprite = LoadSprite("enemy_speed");
              break;
            }
            case EnemyStatistics.ShotRange: {
              value = UnityEngine.Random.Range(enemyShotRangeBounds.z, enemyShotRangeBounds.w);
              sprite = LoadSprite("enemy_card");
              break;
            }
            case EnemyStatistics.DropRate: {
              value = -UnityEngine.Random.Range(enemyDropRateBounds.z, enemyDropRateBounds.w);
              sprite = LoadSprite("enemy_droprate");
              break;
            }
          }
        }
      }
    }
  }
}
