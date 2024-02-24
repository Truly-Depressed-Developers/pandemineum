using UnityEngine;

namespace Utils {
  [DefaultExecutionOrder(-999)]
  public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {
    public static T I { get; private set; }

    protected virtual void Awake() {
      if (I == null) {
        I = this as T;
      } else {
        Destroy(gameObject);
        Debug.LogError($"Trying to create two singleton instances of {typeof(T).Name}!");
      }
    }

    protected virtual void OnDestroy() {
      if (I == this)
        I = null;
    }
  }
}
