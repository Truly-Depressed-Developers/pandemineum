using System;
using UnityEngine;

[CreateAssetMenu]
public class CardStatistics : ScriptableObject
{
    // All statistics
    public Statistics statistics;
    // Chosen statistic
    public EntityType entityType;
    public Statistics chosenStatistic;
    public BuffType buffType;
    public int value;
    // Operational variables
    private int threshhold = 5;
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
        Statistics[] allValues = (Statistics[])Enum.GetValues(typeof(Statistics));
        this.chosenStatistic = allValues[UnityEngine.Random.Range(0, allValues.Length)];

        // Buff or debuff
        this.buffType = UnityEngine.Random.value <= this.probability ? BuffType.Buff : BuffType.Debuff;
    
        // Value of the buff or debuff
        this.value = this.buffType == BuffType.Buff ? UnityEngine.Random.Range(1, this.threshhold) : UnityEngine.Random.Range(1, this.threshhold);
    }
}
