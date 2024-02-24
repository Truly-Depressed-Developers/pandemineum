using System;
using UnityEngine;

[CreateAssetMenu]
public class CardStatistics : ScriptableObject
{
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
    //Enemies Tresholds
    private int enemyHealthThreshhold = 5;
    private int enemyArmorThreshhold = 5;
    private int enemyDamageThreshhold = 5;
    private int enemySpeedThreshhold = 5;
    private int enemyDropRateThreshhold = 5;
    private int enemyShotRangeThreshhold = 5;
    // Operational variables
    private float probability = 0.5f;

    void OnEnable()
    {
        this.drawStatistics();
    }

    private void drawStatistics()
    {
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
        if (this.entityType == EntityType.Player)
        {
            if (this.buffType == BuffType.Buff)
            {
                switch (this.playerStatistic)
                {
                    case PlayerStatistics.Health:
                        {
                            this.value = UnityEngine.Random.Range(1, this.playerHealthThreshhold);
                            break;
                        };
                    case PlayerStatistics.Armor:
                        {
                            this.value = UnityEngine.Random.Range(1, this.playerArmorThreshhold);
                            break;
                        };
                    case PlayerStatistics.Damage:
                        {
                            this.value = UnityEngine.Random.Range(1, this.playerDamageThreshhold);
                            break;
                        };
                    case PlayerStatistics.Speed:
                        {
                            this.value = UnityEngine.Random.Range(1, this.playerSpeedThreshhold);
                            break;
                        };
                    case PlayerStatistics.ReloadSpeed:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.playerReloadSpeedThreshhold);
                            break;
                        };
                    case PlayerStatistics.ShotRange:
                        {
                            this.value = UnityEngine.Random.Range(1, this.playerShotRangeThreshhold);
                            break;
                        };
                    case PlayerStatistics.SightRange:
                        {
                            this.value = UnityEngine.Random.Range(1, this.playerSightRangeThreshhold);
                            break;
                        };
                    case PlayerStatistics.Luck:
                        {
                            this.value = UnityEngine.Random.Range(1, this.playerLuckThreshhold);
                            break;
                        };
                }
            }
            else
            {
                switch (this.playerStatistic)
                {
                    case PlayerStatistics.Health:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.playerHealthThreshhold);
                            break;
                        };
                    case PlayerStatistics.Armor:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.playerArmorThreshhold);
                            break;
                        };
                    case PlayerStatistics.Damage:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.playerDamageThreshhold);
                            break;
                        };
                    case PlayerStatistics.Speed:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.playerSpeedThreshhold);
                            break;
                        };
                    case PlayerStatistics.ReloadSpeed:
                        {
                            this.value = UnityEngine.Random.Range(1, this.playerReloadSpeedThreshhold);
                            break;
                        };
                    case PlayerStatistics.ShotRange:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.playerShotRangeThreshhold);
                            break;
                        };
                    case PlayerStatistics.SightRange:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.playerSightRangeThreshhold);
                            break;
                        };
                    case PlayerStatistics.Luck:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.playerLuckThreshhold);
                            break;
                        };
                }
            }
        }
        else
        {
            if (this.buffType == BuffType.Buff)
            {
                switch (this.enemyStatistic)
                {
                    case EnemyStatistics.Health:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.enemyHealthThreshhold);
                            break;
                        };
                    case EnemyStatistics.Armor:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.enemyArmorThreshhold);
                            break;
                        };
                    case EnemyStatistics.Damage:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.enemyDamageThreshhold);
                            break;
                        };
                    case EnemyStatistics.Speed:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.enemySpeedThreshhold);
                            break;
                        };
                    case EnemyStatistics.ShotRange:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.enemyShotRangeThreshhold);
                            break;
                        };
                    case EnemyStatistics.DropRate:
                        {
                            this.value = UnityEngine.Random.Range(1, this.enemyDropRateThreshhold);
                            break;
                        };
                }
            }
            else
            {
                switch (this.enemyStatistic)
                {
                    case EnemyStatistics.Health:
                        {
                            this.value = UnityEngine.Random.Range(1, this.enemyHealthThreshhold);
                            break;
                        };
                    case EnemyStatistics.Armor:
                        {
                            this.value = UnityEngine.Random.Range(1, this.enemyArmorThreshhold);
                            break;
                        };
                    case EnemyStatistics.Damage:
                        {
                            this.value = UnityEngine.Random.Range(1, this.enemyDamageThreshhold);
                            break;
                        };
                    case EnemyStatistics.Speed:
                        {
                            this.value = UnityEngine.Random.Range(1, this.enemySpeedThreshhold);
                            break;
                        };
                    case EnemyStatistics.ShotRange:
                        {
                            this.value = UnityEngine.Random.Range(1, this.enemyShotRangeThreshhold);
                            break;
                        };
                    case EnemyStatistics.DropRate:
                        {
                            this.value = -UnityEngine.Random.Range(1, this.enemyDropRateThreshhold);
                            break;
                        };
                }
            }
        }
    }
}
