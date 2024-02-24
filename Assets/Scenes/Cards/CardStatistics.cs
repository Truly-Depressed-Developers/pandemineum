using System;
using UnityEngine;

[CreateAssetMenu]
public class CardStatistics : ScriptableObject
{
    public Statistics statistics;
    public EntityType entityType;
    public BuffType buffType;
    public int value;
    private int threshhold = 5;
    public string displayVal;
    public string displayStat;


    void OnEnable()
    {
        Console.WriteLine("Drawing a card");
        this.entityType = this.drawEntityType();
        this.buffType = this.drawBuffType();
        this.value = this.drawValue(this.buffType);
        this.displayStat = this.statistics.ToString();
        this.displayVal = this.value.ToString();

    }

    private EntityType drawEntityType()
    {
        return UnityEngine.Random.value <= 0.5f ? EntityType.Player : EntityType.Enemy;
    }

    private BuffType drawBuffType()
    {
        return UnityEngine.Random.value <= 0.5f ? BuffType.Buff : BuffType.Debuff;
    }

    private int drawValue(BuffType type)
    {
        if (type == BuffType.Buff) {
            return UnityEngine.Random.Range(0, this.threshhold);
        } else
        {
            return UnityEngine.Random.Range(0, this.threshhold);
        }
    }
}
