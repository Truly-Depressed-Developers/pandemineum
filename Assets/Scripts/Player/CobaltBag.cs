using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Player {
  public class CobaltBag : MonoSingleton<CobaltBag> {
    public int CobaltCount { get; private set; }
    public UnityEvent<int> CobaltCountChanged;
    
    public void OnCobaltCollected(int collected) {
      CobaltCount += collected;
      
      CobaltCountChanged.Invoke(CobaltCount);
    }
  }
}
