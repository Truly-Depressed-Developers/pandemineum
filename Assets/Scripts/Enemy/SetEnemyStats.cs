using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using UnityEngine;

public class SetEnemyStats : MonoBehaviour
{
  [SerializeField] private Receiver receiver;
  [SerializeField] private Enemy enemy;
  [SerializeField] private CollisionDamageDealer collisionDamageDealer;

  void Start()
    {
    receiver.baseMaxHp = receiver.baseMaxHp * StatisticsRepo.I.enemyHealthMaxMul;
    receiver.baseArmor = receiver.baseArmor + StatisticsRepo.I.enemyArmorAdd;
    enemy.minJumpDistance = enemy.minJumpDistance * StatisticsRepo.I.enemyAttackRangeMul;
    enemy.movementSpeed = enemy.movementSpeed * StatisticsRepo.I.enemySpeedMul;
    collisionDamageDealer.damage = collisionDamageDealer.damage + StatisticsRepo.I.enemyDamageAdd;
  }
}
