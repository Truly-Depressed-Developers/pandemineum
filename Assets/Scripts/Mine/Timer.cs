using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Mine {
  public class Timer : MonoBehaviour {
    [SerializeField] private int allowedTime;
    [SerializeField] private TMP_Text counter;
    public UnityEvent onTimeRunOut;

    private float startTime;

    public float TimeRemaining {
      get {
        return Mathf.Clamp(startTime + allowedTime - Time.time, 0, Mathf.Infinity);
      }
    }

    private void Start() {
      startTime = Time.time;
    }

    private void Update() {
      counter.SetText(FormatTimeRemaining());

      if (TimeRemaining < Mathf.Epsilon) {
        onTimeRunOut.Invoke();
      }
    }

    private string FormatTimeRemaining() {
      int mins = (int) (TimeRemaining / 60);
      int secs = (int) ((TimeRemaining % 60));

      return $"{mins.ToString().PadLeft(2, '0')}:{secs.ToString().PadLeft(2, '0')}";
    }
  }
}
