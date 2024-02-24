using System;
using DamageSystem.Health;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DamageSystem {
  public class Receiver: MonoBehaviour {
    [SerializeField] private float maxHp;
    [FormerlySerializedAs("hb")] [SerializeField] private Healthbar healthbar;
    [SerializeField] private LayerMask damageSources;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent<float> onDamageReceived;
    
    private float currentHp;
    
    private void Start() {
      currentHp = maxHp;
      
      if (healthbar) healthbar.SetMaxHealth(maxHp);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
      Debug.Log("Collision");
      if (damageSources != (damageSources | 1 << other.gameObject.layer)) return;
      other.gameObject.TryGetComponent(out CollisionDamageDealer damageDealer);
      if (!damageDealer) return;
      TakeDamage(damageDealer.GetDamage());
    }

    private void OnTriggerEnter2D(Collider2D other) {
      Debug.Log("Trigger");
      Debug.Log(other.gameObject.layer);
      Debug.Log(other.gameObject.name);
      if (damageSources != (damageSources | 1 << other.gameObject.layer)) return;
      IDamageDealer damageDealer = other.gameObject.GetComponentInParent<IDamageDealer>();
      if (damageDealer == null) return;
      TakeDamage(damageDealer.GetDamage());
    }
    
    public void TakeDamage(float amount) {
      onDamageReceived.Invoke(amount);

      currentHp -= amount;
      currentHp = Mathf.Clamp(currentHp, 0, maxHp);
      
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
