using UnityEngine;
using UnityEngine.UI;

namespace DamageSystem.Health {
  public class Healthbar : MonoBehaviour {
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    [SerializeField] private Image border;

    public void SetHealth(float health) {
      slider.value = health;

      bool isPlayer = transform.parent.parent.CompareTag("Player");

      if (isPlayer) {
        fill.enabled = true;
        border.enabled = true;
      } else {
        bool isHidden = Mathf.Abs(slider.value - slider.maxValue) < Mathf.Epsilon;
        fill.enabled = !isHidden;
        border.enabled = !isHidden;
      }
    }

    public void SetMaxHealth(float health) {
      slider.maxValue = health;
      
      SetHealth(health);
    }
  }
}
