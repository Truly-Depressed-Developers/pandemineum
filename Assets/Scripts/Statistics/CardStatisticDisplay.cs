using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace Statistics {
  public class CardStatisticDisplay : MonoBehaviour {
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text badge;
    [SerializeField] private TMP_Text value;
    [SerializeField] private Image cardImage;

    public void SetDisplay(CardStatistics stats) {
      var entity = stats.entityType;
      bool isGood = stats.buffType == BuffType.Buff;

      description.text = entity == EntityType.Enemy
        ? GenerateDescription.Description(stats.buffType, stats.enemyStatistic)
        : GenerateDescription.Description(stats.buffType, stats.playerStatistic);

      badge.text = entity == EntityType.Enemy
        ? GenerateDescription.Badge(stats.enemyStatistic)
        : GenerateDescription.Badge(stats.playerStatistic);

      if (entity == EntityType.Enemy) {
        value.text = (stats.value > 0 ? "+" : "") + Mathf.FloorToInt(stats.value);
        value.text += (GenerateDescription.IsMultiplicative(stats.enemyStatistic) ? "%" : "");
      } else {
        value.text = (stats.value > 0 ? "+" : "") + Mathf.FloorToInt(stats.value);
        value.text += (GenerateDescription.IsMultiplicative(stats.playerStatistic) ? "%" : "");
      }

      value.color = isGood ? Color.green : Color.red;

      cardImage.sprite = stats.sprite;

      // var buff = stats.buffType;
      //
      // // Fill out text fields' values
      // string sign = stats.value > 0 ? "+" : "";
      // value.text = sign + stats.value.ToString();
      //
      // entityType.text = stats.entityType.ToString();
      // if (stats.entityType == EntityType.Player) {
      //   chosenStatistic.text = stats.playerStatistic.ToString();
      // }
      //
      // transform.parent.GetComponent<Image>().sprite = stats.sprite;
      //
      // chosenStatistic.text = stats.enemyStatistic.ToString();
      // if (buff == BuffType.Buff) {
      //   value.color = Color.green;
      //   entityType.color = Color.green;
      //   chosenStatistic.color = Color.green;
      // } else {
      //   value.color = Color.red;
      //   entityType.color = Color.red;
      //   chosenStatistic.color = Color.red;
      // }
    }
  }
}
