using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game3Manager : Singleton<Game3Manager>
{
    private GameManager gameManager;
    [SerializeField] private List<MoleInteractable> moles;
    [SerializeField] private List<MoleInteractable> alteredMoles;
    [SerializeField] private int molesToAlter;
    [SerializeField] private float timeToInspect = 30;
    [SerializeField] private float timeToSearch = 60;
    [SerializeField] private TMP_Text moleInfoText;
    [SerializeField] private TMP_Text instructionsText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Canvas dotInspectCanvas;
    [SerializeField] private Image dotSpriteImageUI;
    [SerializeField] private GameObject dotVoteButtonsUI;
    [SerializeField] private FeedbackData game3Feedback;
    [SerializeField] private AudioClip game3Music;
    [SerializeField] private AudioClip singleBell;
    [SerializeField] private AudioClip tripleBell;

    private float game3Score = 0;
    private bool countdownRunning = false;
    private float countdownTime;
    private string feedbackText;
    private string missedMolesFeedbackText;
    private int gameState = 0;
    private int wrongOrMissedMoles = 0;
    private MoleInteractable selectedMole;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        instructionsText.text = "";
        moleInfoText.text = "";
        dotInspectCanvas.worldCamera = FindObjectOfType<Camera>();
        AudioManager.Instance.PlayAmbienceClip(game3Music);
    }

    void Update()
    {
        if(countdownRunning)
        {
            if(countdownTime > 0)
            {
                countdownTime -= Time.deltaTime;
                DisplayCountdown(countdownTime);
            }
            else
            {
                countdownTime = 0;
                countdownRunning = false;
            }
        }
    }

    public void StartGameplay()
    {
        gameManager.SwitchInteractor("RAY");
        foreach(MoleInteractable m in moles)
        {
            m.gameObject.SetActive(true);
        }
        dotInspectCanvas.gameObject.SetActive(true);
        StartCoroutine(StartInspection());
    }

    private IEnumerator StartInspection()
    {
        gameState = 1;
        AudioManager.Instance.PlayClipOnce(singleBell);
        instructionsText.text = "Leberflecke einprägen";
        StartCountdown(timeToInspect);
        yield return new WaitForSeconds(timeToInspect);
        AlterRandomMoles();
    }

    private void AlterRandomMoles()
    {
        for(int i = molesToAlter; i > 0; i--)
        {
            MoleInteractable randomMole = moles[Random.Range(0, moles.Count)];
            randomMole.AlterThisMole();
            moles.Remove(randomMole);
            alteredMoles.Add(randomMole);
        }
        StartCoroutine(StartSearch());
    }

    public void CheckMole(MoleInteractable mole, bool altered)
    {
        if(altered)
        {
            moleInfoText.text = "Veränderten Leberfleck gefunden!";
            game3Score -= 1;
            alteredMoles.Remove(mole);
        }
        else
        {
            moleInfoText.text = "Der ausgewählte Leberfleck ist nicht verändert.";
            game3Score += 0.5f;
        }
    }

    private IEnumerator StartSearch()
    {
        gameState = 2;
        gameManager.SwitchInteractor("RAY");
        AudioManager.Instance.PlayClipOnce(tripleBell);
        instructionsText.text = "Finde veränderte Leberflecke";
        StartCountdown(timeToSearch);
        yield return new WaitForSeconds(timeToSearch);
        ShowMissedMoles();
        gameManager.SaveScoreGame3(game3Score, wrongOrMissedMoles);
        gameManager.UpdateScore(game3Score);
        AudioManager.Instance.PlayClipOnce(tripleBell);
        MolesMissedInfoText();
        GenerateFeedbackText();
        dotInspectCanvas.gameObject.SetActive(false);
        UIManager.Instance.EnableGame3Feedback(feedbackText);
        UIManager.Instance.EnableMainMenuCanvas();
    }

    private void MolesMissedInfoText()
    {
        switch(wrongOrMissedMoles)
        {
            case 0:
                moleInfoText.text = "Alle veränderten Leberflecke richtig erkannt!";
                break;
            case 1:
                moleInfoText.text = "1 veränderten Leberfleck übersehen.";
                break;
            default:
                moleInfoText.text = wrongOrMissedMoles.ToString() + " veränderte Leberflecke übersehen.";
                break;
        }
    }

    private void ShowMissedMoles()
    {
        foreach(MoleInteractable m in alteredMoles)
        {
            wrongOrMissedMoles += 1;
            m.SetMissedMaterial();
            game3Score += 1;
        }

        foreach(MoleInteractable m in moles)
        {
            m.BlockSelection();
        }
    }

    private void GenerateFeedbackText()
    {
        if(alteredMoles.Count == 0)
        {
            missedMolesFeedbackText = game3Feedback.FeedbackText1;
        }
        else
        {
            missedMolesFeedbackText = game3Feedback.FeedbackText2;
        }

        feedbackText = "Super! Spiel abgeschlossen. So hast du dich geschlagen:\n" + missedMolesFeedbackText;
    }

    private void StartCountdown(float timeRemaining)
    {
        countdownTime = timeRemaining;
        countdownRunning = true;
    }

    private void DisplayCountdown(float timeToShow)
    {
        timeToShow += 1;

        float minutes = Mathf.FloorToInt(timeToShow / 60);
        float seconds = Mathf.FloorToInt(timeToShow % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void InspectDot(MoleInteractable selectedDot, Sprite dotSprite)
    {
        selectedMole = selectedDot;

        moleInfoText.gameObject.SetActive(false);
        dotSpriteImageUI.sprite = dotSprite;
        dotSpriteImageUI.gameObject.SetActive(true);
        if(gameState == 2)
        {
            dotVoteButtonsUI.SetActive(true);
        }
    }

    public void OnDismissButtonClick()
    {
        selectedMole.OnMoleVoted(false);
        dotSpriteImageUI.gameObject.SetActive(false);
        dotVoteButtonsUI.SetActive(false);
    }

    public void OnSelectButtonClick()
    {
        selectedMole.OnMoleVoted(true);
        dotSpriteImageUI.gameObject.SetActive(false);
        dotVoteButtonsUI.SetActive(false);
    }

    public void OnVoteCorrect(MoleInteractable mole, bool altered)
    {
        if(altered)
        {
            moleInfoText.text = "Richtig! Der Leberfleck war verändert.";
            game3Score -= 1;
            alteredMoles.Remove(mole);
        }
        else
        {
            moleInfoText.text = "Richtig! Der Leberfleck war unverändert.";
            game3Score -= 0.25f;
        }
        moleInfoText.gameObject.SetActive(true);
    }

    public void OnVoteFalse(MoleInteractable mole, bool altered)
    {
        if(altered)
        {
            moleInfoText.text = "Falsch! Der Leberfleck war verändert.";
            game3Score += 1;
            alteredMoles.Remove(mole);
            wrongOrMissedMoles += 1;
        }
        else
        {
            moleInfoText.text = "Falsch! Der Leberfleck war unverändert.";
            game3Score += 0.25f;
        }
        moleInfoText.gameObject.SetActive(true);
    }
}
