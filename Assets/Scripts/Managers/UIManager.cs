using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject controlsCanvas;
    [SerializeField] GameObject gameScoreBar;
    [SerializeField] GameObject introSection;
    [SerializeField] GameObject game1IntroSection;
    [SerializeField] GameObject game1P2IntroSection;
    [SerializeField] GameObject game1FeedbackSection;
    [SerializeField] GameObject game2IntroSection;
    [SerializeField] GameObject game2FeedbackSection;
    [SerializeField] GameObject game3IntroSection;
    [SerializeField] GameObject game3FeedbackSection;
    [SerializeField] GameObject gameEndSection;
    [SerializeField] TMP_Text game1FeedbackText;
    [SerializeField] TMP_Text game2FeedbackText;
    [SerializeField] TMP_Text game3FeedbackText;

    public void DisableMainMenuCanvas()
    {
        mainMenuCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
    }

    public void EnableMainMenuCanvas()
    {
        mainMenuCanvas.SetActive(true);
        controlsCanvas.SetActive(true);
    }

    public void EnableGame1Intro()
    {
        DisableAllSections();
        game1IntroSection.SetActive(true);
    }

    public void EnableGame1P2Intro()
    {
        DisableAllSections();
        game1P2IntroSection.SetActive(true);
    }

    public void EnableGame1Feedback(string feedbackText)
    {
        DisableAllSections();
        game1FeedbackText.text = feedbackText;
        game1FeedbackSection.SetActive(true);
    }

    public void EnableGame2Intro()
    {
        DisableAllSections();
        game2IntroSection.SetActive(true);
    }

    public void EnableGame2Feedback(string feedbackText)
    {
        DisableAllSections();
        game2FeedbackText.text = feedbackText;
        game2FeedbackSection.SetActive(true);
    }

    public void EnableGame3Intro()
    {
        DisableAllSections();
        game3IntroSection.SetActive(true);
    }

    public void EnableGame3Feedback(string feedbackText)
    {
        DisableAllSections();
        game3FeedbackText.text = feedbackText;
        game3FeedbackSection.SetActive(true);
    }

    public void EnableGameEnd()
    {
        DisableAllSections();
        gameEndSection.SetActive(true);
    }

    public void DisableAllSections()
    {
        introSection.SetActive(false);
        game1IntroSection.SetActive(false);
        game1P2IntroSection.SetActive(false);
        game1FeedbackSection.SetActive(false);
        game2IntroSection.SetActive(false);
        game2FeedbackSection.SetActive(false);
        game3IntroSection.SetActive(false);
        game3FeedbackSection.SetActive(false);
        gameEndSection.SetActive(false);
    }
}
