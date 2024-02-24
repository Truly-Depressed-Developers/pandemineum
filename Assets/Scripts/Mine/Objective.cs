using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Mine {
  public class Objective : MonoBehaviour {
    [SerializeField] private int cobaltRequirement;
    [SerializeField] private TMP_Text counter;
    public UnityEvent onThresholdCrossed;

    private void Start() {
      CobaltBag.I.CobaltCountChanged.AddListener((newCount) => {
        UpdateCounterText(newCount);
        
        if(newCount >= cobaltRequirement) onThresholdCrossed.Invoke();
      });
      
      UpdateCounterText(0);
    }

    private void UpdateCounterText(int newCount) {
      counter.SetText($"{newCount} / {cobaltRequirement}");
    }
  }
}
