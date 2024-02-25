using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
  [SerializeField] private float verticalMoveAmount = 30f;
  [SerializeField] private float moveTime = 0.1f;
  [Range(0, 2f), SerializeField] private float scaleAmount = 1.05f;

  public CardStatistics statistics;

  private Vector3 startPos;
  private Vector3 startScale;

  void Start() {
    this.startPos = transform.position;
    this.startScale = transform.localScale;
  }

  private IEnumerator animateCards(bool startingAnimation) {
    Vector3 endPosition;
    Vector3 endScale;

    float elapsedTime = 0f;
    while (elapsedTime < this.moveTime) {
      // Increment timer
      elapsedTime += Time.deltaTime;

      // Assess move direction
      if (startingAnimation) {
        endPosition = this.startPos + new Vector3(0f, this.verticalMoveAmount, 0f);
        endScale = this.startScale * this.scaleAmount;
      } else {
        endPosition = this.startPos;
        endScale = this.startScale;
      }

      // Calculate the step
      Vector3 lerpedPosition = Vector3.Lerp(transform.position, endPosition, (elapsedTime / moveTime));
      Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, (elapsedTime / moveTime));

      // Apply the changes
      transform.position = lerpedPosition;
      transform.localScale = lerpedScale;

      // Animation stuff - await next frame kind of
      yield return null;
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    // Select the card
    eventData.selectedObject = gameObject;
  }

  public void OnPointerExit(PointerEventData eventData) {
    // Deselect the card
    eventData.selectedObject = null;
  }

  public void OnSelect(BaseEventData eventData) {
    StartCoroutine(this.animateCards(true));
  }

  public void OnDeselect(BaseEventData eventData) {
    StartCoroutine(this.animateCards(false));
  }

  public void onClick() {
    this.updateStats(this.statistics);
  }

  public void updateStats(CardStatistics cardStatisticsScriptableObjects) {
    switch (cardStatisticsScriptableObjects.entityType) {
      case EntityType.Player: {
          switch (cardStatisticsScriptableObjects.playerStatistic) {
            case PlayerStatistics.Health: {
                StatisticsRepo.I.playerHealthMaxMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case PlayerStatistics.Armor: {
                StatisticsRepo.I.playerArmorAdd += cardStatisticsScriptableObjects.value;
                break;
              }
            case PlayerStatistics.Damage: {
                StatisticsRepo.I.playerDamageAdd += cardStatisticsScriptableObjects.value;
                break;
              }
            case PlayerStatistics.Speed: {
                StatisticsRepo.I.playerSpeedMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case PlayerStatistics.ReloadSpeed: {
                StatisticsRepo.I.playerSpeedMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case PlayerStatistics.ShotRange: {
                StatisticsRepo.I.playerShotRangeMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case PlayerStatistics.SightRange: {
                StatisticsRepo.I.playerSightRangeMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case PlayerStatistics.Luck: {
                StatisticsRepo.I.playerLuckMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case PlayerStatistics.CobaltPickRate: {
                StatisticsRepo.I.playerCobaltPickRateMul *= cardStatisticsScriptableObjects.value;
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
          switch (cardStatisticsScriptableObjects.enemyStatistic) {
            case EnemyStatistics.Health: {
                StatisticsRepo.I.enemyHealthMaxMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case EnemyStatistics.Armor: {
                StatisticsRepo.I.enemyArmorAdd += cardStatisticsScriptableObjects.value;
                break;
              }
            case EnemyStatistics.Damage: {
                StatisticsRepo.I.enemyDamageAdd += cardStatisticsScriptableObjects.value;
                break;
              }
            case EnemyStatistics.Speed: {
                StatisticsRepo.I.enemySpeedMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case EnemyStatistics.ShotRange: {
                StatisticsRepo.I.playerShotRangeMul *= cardStatisticsScriptableObjects.value;
                break;
              }
            case EnemyStatistics.DropRate: {
                StatisticsRepo.I.enemyDropRateMul *= cardStatisticsScriptableObjects.value;
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

