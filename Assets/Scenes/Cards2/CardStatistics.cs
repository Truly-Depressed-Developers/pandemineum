using System;
using UnityEditor;
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
        this.entityType = this.drawEntityType();
        this.buffType = this.drawBuffType();
        this.value = this.drawValue(this.buffType);

        this.displayStat = this.drawStatistic().ToString();
        this.displayVal = this.value.ToString();
    }
    private Statistics drawStatistic()
    {
        Statistics[] allValues = (Statistics[])Enum.GetValues(typeof(Statistics));
        return allValues[UnityEngine.Random.Range(0, allValues.Length)];
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
        return type == BuffType.Buff ? UnityEngine.Random.Range(0, this.threshhold): UnityEngine.Random.Range(0, this.threshhold);
    }
}
