using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Statistics {
  public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler,
    IDeselectHandler {
    [SerializeField] private float verticalMoveAmount = 30f;
    [SerializeField] private float moveTime = 0.1f;
    [Range(0, 2f), SerializeField] private float scaleAmount = 1.05f;
    [SerializeField] private CardStatisticDisplay csd;
    public UnityEvent OnCardBought;
    
    private CardStatistics statistics;

    private bool canBeClicked = true;
    private Vector3 startPos;
    private Vector3 startScale;

    private void Start() {
      startPos = transform.position;
      startScale = transform.localScale;
    }

    private IEnumerator AnimateCards(bool startingAnimation, Vector3 target) {
      float elapsedTime = 0f;
      
      while (elapsedTime < moveTime * 2) {
        // Increment timer
        elapsedTime += Time.deltaTime;

        // Assess move direction
        Vector3 endPosition;
        Vector3 endScale;
        
        if (startingAnimation) {
          endPosition = startPos + new Vector3(0f, verticalMoveAmount, 0f);
          endScale = startScale * scaleAmount;
        } else {
          endPosition = startPos;
          endScale = target;
        }

        // Calculate the step
        Vector3 lerpedPosition = Vector3.Lerp(transform.position, endPosition, (elapsedTime / moveTime / 15));
        Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime / 15));

        // Apply the changes
        transform.position = lerpedPosition;
        transform.localScale = lerpedScale;

        // Animation stuff - await next frame kind of
        yield return null;
      }
    }

    public void OnPointerEnter(PointerEventData eventData) {
      // Select the card
      if (this.canBeClicked) {
        eventData.selectedObject = gameObject;
      }
    }

    public void OnPointerExit(PointerEventData eventData) {
      // Deselect the card
      eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData) {
      if (this.canBeClicked) {
        StartCoroutine(AnimateCards(true, this.startScale));
      }
    }

    public void OnDeselect(BaseEventData eventData) {
      if (this.canBeClicked) {
        StartCoroutine(AnimateCards(false, this.startScale));
      } else {
        StartCoroutine(AnimateCards(false, new Vector3(0,0,0)));
      }
    }

    public void OnClick() {
      if (this.canBeClicked && this.gameObject != null) {
        this.canBeClicked = false;
        UpdateStatisticsRepo();
        OnCardBought.Invoke();
      }
    }

    public void SetStats(CardStatistics stats) {
      statistics = stats;
      csd.SetDisplay(stats);
    }

    private void UpdateStatisticsRepo() {
      float value = statistics.value;
      bool isMultiplicative = statistics.entityType == EntityType.Enemy
        ? GenerateDescription.IsMultiplicative(statistics.enemyStatistic)
        : GenerateDescription.IsMultiplicative(statistics.playerStatistic);

      if (isMultiplicative) {
        value = 1 + value / 100f;
      }
      
      switch (statistics.entityType) {
        case EntityType.Player: {
          switch (statistics.playerStatistic) {
            case PlayerStatistics.Health: {
              StatisticsRepo.I.PlayerHealthMaxMul *= value;
              break;
            }
            case PlayerStatistics.Armor: {
              StatisticsRepo.I.PlayerArmorAdd += value;
              StatisticsRepo.I.PlayerArmorAdd = Mathf.Max(StatisticsRepo.I.PlayerArmorAdd, 0);
              break;
            }
            case PlayerStatistics.Damage: {
              StatisticsRepo.I.PlayerDamageAdd += value;
              StatisticsRepo.I.PlayerDamageAdd = Mathf.Max(StatisticsRepo.I.PlayerDamageAdd, 0);
              break;
            }
            case PlayerStatistics.Speed: {
              StatisticsRepo.I.PlayerSpeedMul *= value;
              break;
            }
            case PlayerStatistics.ReloadSpeed: {
              StatisticsRepo.I.PlayerReloadSpeedMul *= value;
              break;
            }
            case PlayerStatistics.ShotRange: {
              StatisticsRepo.I.PlayerShotRangeMul *= value;
              break;
            }
            case PlayerStatistics.SightRange: {
              StatisticsRepo.I.PlayerSightRangeMul *= value;
              break;
            }
            case PlayerStatistics.Luck: {
              StatisticsRepo.I.PlayerLuckMul *= value;
              break;
            }
            case PlayerStatistics.CobaltPickRate: {
              StatisticsRepo.I.PlayerCobaltPickRateMul *= value;
              break;
            }
            default: {
              Debug.LogWarning("Unknown Player property type");
              break;
            }
          }

          break;
        }
        case EntityType.Enemy: {
          switch (statistics.enemyStatistic) {
            case EnemyStatistics.Health: {
              StatisticsRepo.I.EnemyHealthMaxMul *= value;
              break;
            }
            case EnemyStatistics.Armor: {
              StatisticsRepo.I.EnemyArmorAdd += value;
              StatisticsRepo.I.EnemyArmorAdd = Mathf.Max(StatisticsRepo.I.EnemyArmorAdd, 0);
              break;
            }
            case EnemyStatistics.Damage: {
              StatisticsRepo.I.EnemyDamageAdd += value;
              StatisticsRepo.I.EnemyDamageAdd = Mathf.Max(StatisticsRepo.I.EnemyDamageAdd, 0);
              break;
            }
            case EnemyStatistics.Speed: {
              StatisticsRepo.I.EnemySpeedMul *= value;
              break;
            }
            case EnemyStatistics.ShotRange: {
              StatisticsRepo.I.EnemyAttackRangeMul *= value;
              break;
            }
            case EnemyStatistics.DropRate: {
              StatisticsRepo.I.EnemyDropRateMul *= value;
              break;
            }
            default: {
              Debug.LogWarning("Unknown enemy property type");
              break;
            }
          }
          break;
        }
        default: {
          Debug.LogWarning("Unknown entity type");
          break;
        }
      }
    }
  }
}
