using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardPrefabGenerator : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardStatistics cardStatistics;
    void Start()
    {
        this.GenerateCardPrefab(this.cardStatistics);
    }
    public void GenerateCardPrefab(CardStatistics cardData)
    {
        // Instantiate the card prefab
        GameObject newCard = Instantiate(cardPrefab, transform.position, Quaternion.identity);

        // Get the Text components from the prefab
        TMP_Text statistic = newCard.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text value = newCard.transform.GetChild(1).GetComponent<TMP_Text>();

        // Display data from the CardData asset
        statistic.text = cardData.displayStat;
        value.text = cardData.displayVal;

        newCard.transform.SetParent(transform);
    }
}
