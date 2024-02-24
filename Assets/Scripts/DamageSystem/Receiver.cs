using UnityEngine;
using UnityEngine.Events;

namespace DamageSystem {
  public class Receiver: MonoBehaviour {
    [SerializeField] private float maxHp;
    [SerializeField] private LayerMask damageSources;
    [SerializeField] private UnityEvent onDeath;
    [SerializeField] private UnityEvent<float> onDamageReceived;
    private float currentHp;
    
    private void Start() {
      currentHp = maxHp;
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

      currentHp -= amount;
      currentHp = Mathf.Clamp(currentHp, 0, maxHp);
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