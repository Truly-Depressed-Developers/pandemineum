using UnityEngine;

namespace DamageSystem {
  public class CollisionDamageDealer : MonoBehaviour, IStaticDamageDealer {
    [SerializeField] public float damage = 20f;

    public float GetDamage() {
      return damage;
    }
  }
}
