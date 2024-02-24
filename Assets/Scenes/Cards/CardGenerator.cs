using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardPrefabGenerator : MonoBehaviour
{
    public GameObject cardPrefab; // Reference to the prefab you want to generate
    public CardStatistics cardStatistics;
    void Start()
    {
        Console.WriteLine("test");
        this.GenerateCardPrefab(this.cardStatistics);
    }
    public void GenerateCardPrefab(CardStatistics cardData)
    {
        // Instantiate the card prefab
        GameObject newCard = Instantiate(cardPrefab, transform.position, Quaternion.identity);

        // Get the Text components from the prefab
        TMP_Text statistic = newCard.GetComponentInChildren<TMP_Text>(); // Assuming the Text component is a child of the prefab
        TMP_Text value = newCard.GetComponentInChildren<TMP_Text>(); // Assuming the Text component is a child of the prefab

        // Display data from the CardData asset
        statistic.text = cardData.displayStat;
        value.text = cardData.displayVal;

        // Optionally, you can set the parent of the instantiated card
        newCard.transform.SetParent(transform);
    }



}
