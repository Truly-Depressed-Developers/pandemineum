using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using Player;
using UnityEngine;

[DefaultExecutionOrder(-999)]
public class SetPlayerStats : MonoBehaviour
{
  [SerializeField] private Receiver receiver;
  [SerializeField] private Shotgun shotgun;
  [SerializeField] private Movement movement;

  private void Start() {
    receiver.baseMaxHp = receiver.baseMaxHp * StatisticsRepo.I.playerHealthMaxMul;
    receiver.baseArmor = receiver.baseArmor + StatisticsRepo.I.playerArmorAdd;
    shotgun.damage = shotgun.damage + StatisticsRepo.I.playerDamageAdd;
    movement.moveSpeed = movement.moveSpeed * StatisticsRepo.I.playerSpeedMul;
    shotgun.range = shotgun.range * StatisticsRepo.I.playerShotRangeMul;
    shotgun.reloadTime = shotgun.reloadTime * StatisticsRepo.I.playerReloadSpeedMul;

  }
}
