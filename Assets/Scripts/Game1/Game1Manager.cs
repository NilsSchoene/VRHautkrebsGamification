using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Manager : Singleton<Game1Manager>
{
    [SerializeField] private GameObject itemInteractables;
    [SerializeField] private GameObject timeInteractables;
    [SerializeField] private GameObject itemUI;
    [SerializeField] private GameObject timeUI;
    [SerializeField] private Game1Bag beachBag;
    [SerializeField] private FeedbackData game1ItemFeedback;
    [SerializeField] private FeedbackData game1TimeFeedback;
    [SerializeField] private AudioClip game1Music;

    private GameManager gameManager;
    private float itemScore;
    private float timeScore;
    private string feedbackText;
    private string itemFeedbackText;
    private string timeFeedbackText;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        AudioManager.Instance.PlayAmbienceClip(game1Music);
    }

    public void OnItemButtonClicked()
    {
        bool bagFull = beachBag.CheckBagStatus();

        if(bagFull)
        {
            itemScore = beachBag.CalculateItemScore();
            gameManager.SaveScoreGame1Items(itemScore);
            gameManager.UpdateScore(itemScore);

            itemInteractables.SetActive(false);
            timeInteractables.SetActive(true);
            itemUI.SetActive(false);
            timeUI.SetActive(true);
            UIManager.Instance.EnableGame1P2Intro();
        }
    }

    public void OnTimeButtonClicked(int time)
    {
        if (time == 8) { timeScore = -1; }
        else if (time == 11) { timeScore = 2; }
        else if (time == 14) { timeScore = 2; }
        else if (time == 17) { timeScore = -1; }
        else if (time == 20) { timeScore = -1; }
        gameManager.SaveScoreGame1Time(timeScore);
        gameManager.UpdateScore(timeScore);

        timeInteractables.SetActive(false);
        timeUI.SetActive(false);
        
        GenerateFeedbackText();
        UIManager.Instance.EnableGame1Feedback(feedbackText);

        gameManager.LoadMenu();
    }

    private void GenerateFeedbackText()
    {
        if(itemScore < 0)
        {
            itemFeedbackText = game1ItemFeedback.FeedbackText1;
        }
        else
        {
            itemFeedbackText = game1ItemFeedback.FeedbackText2;
        }

        if(timeScore < 0)
        {
            timeFeedbackText = game1TimeFeedback.FeedbackText1;
        }
        else
        {
            timeFeedbackText = game1TimeFeedback.FeedbackText2;
        }

        feedbackText = "Super! Spiel abgeschlossen. Hier ein kurzes Feedback, wie du dich geschlagen hast:\n" + itemFeedbackText + "\n" + timeFeedbackText;
    }
}
