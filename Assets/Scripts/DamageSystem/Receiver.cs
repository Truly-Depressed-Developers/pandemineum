using System;
using DamageSystem.Health;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DamageSystem {
  public class Receiver: MonoBehaviour {
    [SerializeField] public float baseMaxHp;
    [SerializeField] public float baseArmor;
    [FormerlySerializedAs("hb")] [SerializeField] private Healthbar healthbar;
    [SerializeField] private LayerMask damageSources;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent<float> onDamageReceived;
    
    private float currentHp;
    
    private void Start() {
      currentHp = baseMaxHp;
      
      if (healthbar) healthbar.SetMaxHealth(baseMaxHp);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
      if (damageSources != (damageSources | 1 << other.gameObject.layer)) return;
      other.gameObject.TryGetComponent(out CollisionDamageDealer damageDealer);
      if (!damageDealer) return;
      TakeDamage(damageDealer.GetDamage());
    }

    private void OnTriggerEnter2D(Collider2D other) {
      if (damageSources != (damageSources | 1 << other.gameObject.layer)) return;
      IDamageDealer damageDealer = other.gameObject.GetComponentInParent<IDamageDealer>();
      if (damageDealer == null) return;
      TakeDamage(damageDealer.GetDamage());
    }
    
    public void TakeDamage(float amount) {
      onDamageReceived.Invoke(amount);

      currentHp -= amount *(1-(baseArmor/(baseArmor +50)));
      currentHp = Mathf.Clamp(currentHp, 0, baseMaxHp);
      
      if(healthbar) healthbar.SetHealth(currentHp);
      
      if (currentHp == 0) {
        Die();
      }

      return;
      
      void Die() {
        onDeath.Invoke();
        Destroy(gameObject);
      }
    }
  }
}
