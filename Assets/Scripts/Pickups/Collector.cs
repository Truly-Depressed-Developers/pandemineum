using Cobalt;
using UnityEngine;
using UnityEngine.Events;

namespace Pickups {
  public class Collector : MonoBehaviour {
    [SerializeField] private float collectionRadius = 0.25f;
    public UnityEvent<int> OnCobaltCollected;
    private void OnTriggerStay2D(Collider2D other) {
      if (!other.gameObject.TryGetComponent(out Chunk c)) return;
      if ((other.transform.position - transform.position).sqrMagnitude > Mathf.Pow(collectionRadius, 2)) return;
      
      OnCobaltCollected.Invoke(c.Richness);
      Destroy(other.gameObject);
    }
  }
}
