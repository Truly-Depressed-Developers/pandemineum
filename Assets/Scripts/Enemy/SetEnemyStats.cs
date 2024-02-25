using DamageSystem;
using Statistics;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class SetEnemyStats : MonoBehaviour {
  [SerializeField] private Receiver receiver;
  [SerializeField] private Enemy kobold;
  [SerializeField] private CollisionDamageDealer collisionDamageDealer;

  private void Start() {
    receiver.baseMaxHp *= StatisticsRepo.I.EnemyHealthMaxMul;
    receiver.baseArmor += StatisticsRepo.I.EnemyArmorAdd;
    kobold.minJumpDistance *= StatisticsRepo.I.EnemyAttackRangeMul;
    kobold.movementSpeed *= StatisticsRepo.I.EnemySpeedMul;
    collisionDamageDealer.damage += StatisticsRepo.I.EnemyDamageAdd;
  }
}
