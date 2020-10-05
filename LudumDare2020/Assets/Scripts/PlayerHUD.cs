using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI FoodScore;
    public TextMeshProUGUI DrinkScore;
    public TextMeshProUGUI StrengthScore;
    public TextMeshProUGUI HandsomeScore;
    public TextMeshProUGUI LoveScore;

    public TextMeshProUGUI ReincarnationCount;

    public GameObject DeathUI;
    public TextMeshProUGUI RestartIn;
    float RestartTimer = 5.0f;

    bool deadNoticeIssued = false;

    public void UpdateScores(Sentient sentient)
    {
        if(sentient == null) { return; }
        string foodText = sentient.Food.ToString("F1") + "/" + sentient.maxFood;
        string drinkText = sentient.Drink.ToString("F1") + "/" + sentient.maxDrink;
        string strengthText = sentient.Strength.ToString("F1") + "/" + sentient.maxStrength;
        string handsomeText = sentient.Handsome.ToString("F1") + "/" + sentient.maxHandsome;
        string loveText = sentient.Love.ToString("F1") + "/" + sentient.maxLove;

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

    public void DeathNotice()
    {
        if(!deadNoticeIssued)
        {
            if(!DeathUI) { Debug.Log("Pls assign DeathUI on Player HUD!", this); return; }
            DeathUI.SetActive(true);
            StartCoroutine(RestartCountDown());
            deadNoticeIssued = true;
        }     
    }

    IEnumerator RestartCountDown()
    {
        while(RestartTimer > 0)
        {
            yield return null;

            RestartTimer -= Time.deltaTime;

            if(RestartIn)
            {
                RestartIn.SetText("Restarting in: " + ((int)RestartTimer).ToString());
            }
        }

        Restart();

        yield return null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
