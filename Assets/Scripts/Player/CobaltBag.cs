using Statistics;
using UnityEngine.Events;
using Utils;

namespace Player {
  public class CobaltBag : MonoSingleton<CobaltBag> {
    public int CobaltCount { get; private set; }
    public UnityEvent<int> cobaltCountChanged;
    
    public void OnCobaltCollected(int collected) {
      CobaltCount += (int) (collected * StatisticsRepo.I.PlayerCobaltPickRateMul);
      
      cobaltCountChanged.Invoke(CobaltCount);
    }
  }
}
