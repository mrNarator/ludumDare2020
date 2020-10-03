using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI FoodScore;
    public TextMeshProUGUI DrinkScore;
    public TextMeshProUGUI StrengthScore;
    public TextMeshProUGUI HandsomeScore;
    public TextMeshProUGUI LoveScore;


    public void UpdateScores(Sentient sentient)
    {
        if(sentient == null) { return; }
        string foodText = sentient.Food + "/" + sentient.maxFood;
        string drinkText = sentient.Drink + "/" + sentient.maxDrink;
        string strengthText = sentient.Strength + "/" + sentient.maxStrength;
        string handsomeText = sentient.Handsome + "/" + sentient.maxHandsome;
        string loveText = sentient.Love + "/" + sentient.maxLove;

        FoodScore.SetText(foodText);
        DrinkScore.SetText(drinkText);
        StrengthScore.SetText(strengthText);
        HandsomeScore.SetText(handsomeText);
        LoveScore.SetText(loveText);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
