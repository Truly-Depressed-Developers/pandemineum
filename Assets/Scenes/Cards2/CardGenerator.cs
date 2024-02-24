using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardPrefabGenerator : MonoBehaviour
{
    void Start()
    {
        this.GenerateCardPrefab();
    }
    public void GenerateCardPrefab()
    {
        for (int i = 0; i < this.transform.childCount; i++) { 
            var child = this.transform.GetChild(i);
            var cardStatistics = ScriptableObject.CreateInstance<CardStatistics>();

            card5 card5 = child.GetComponent<card5>(); // Skrypt
            card5.displayStat.text = cardStatistics.displayStat;
            card5.displayVal.text = cardStatistics.displayVal;
        }
    }
}
