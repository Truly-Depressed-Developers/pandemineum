using System.Collections;
using System.Collections.Generic;
using DamageSystem;
using Player;
using Statistics;
using UnityEngine;

namespace Player {
  [DefaultExecutionOrder(-999)]
  public class SetPlayerStats : MonoBehaviour {
    [SerializeField] private Receiver receiver;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private Movement movement;

    private void Start() {
      receiver.baseMaxHp *= StatisticsRepo.I.PlayerHealthMaxMul;
      receiver.baseArmor += StatisticsRepo.I.PlayerArmorAdd;
      shotgun.damage += StatisticsRepo.I.PlayerDamageAdd;
      movement.moveSpeed *= StatisticsRepo.I.PlayerSpeedMul;
      shotgun.range *= StatisticsRepo.I.PlayerShotRangeMul;
      shotgun.reloadTime *= StatisticsRepo.I.PlayerReloadSpeedMul;
    }
  }
}
