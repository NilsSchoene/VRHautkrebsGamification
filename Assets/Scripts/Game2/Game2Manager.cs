using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Game2Manager : Singleton<Game2Manager>
{
    private GameManager gameManager;

    private GameObject mainCameraGO;

    [SerializeField] private GameObject sunDirect;
    [SerializeField] private FeedbackData game2Feedback;
    [SerializeField] private AudioClip game2Music;

    public bool checkForSunHits = false;
    public bool hitBySun = false;
    private float timeInSun = 0.0f;
    private float timeScore;
    private int sunPositions;
    private string feedbackText;
    private string timeFeedbackText;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        mainCameraGO = FindObjectOfType<Camera>().gameObject;
        AudioManager.Instance.PlayAmbienceClip(game2Music);
    }

    void Update()
    {
        if (checkForSunHits)
        {
            if (Physics.Raycast(mainCameraGO.transform.position, -sunDirect.transform.forward))
            {
                hitBySun = false;
            }
            else
            {
                hitBySun = true;
                timeInSun += Time.deltaTime;
            }
        }
    }

    public void StartGame()
    {
        StartGame2();
    }

    public void AddGame2Score(float teleporterScore)
    {
        checkForSunHits = false;
    }
    
    public void OnSunDestinationReached()
    {
        checkForSunHits = true;
        gameManager.SwitchInteractor("TELEPORT");
    }

    private void GetTimeScore()
    {
        timeScore = Mathf.Round(timeInSun / sunPositions) - 4;
        if(timeScore >= 0)
        {
            timeFeedbackText = game2Feedback.FeedbackText2;
        }
        else
        {
            timeFeedbackText = game2Feedback.FeedbackText1;
        }
    }

    public void OnGameEnd()
    {
        GetTimeScore();
        gameManager.SaveScoreGame2(timeScore, timeInSun);
        gameManager.UpdateScore(timeScore);

        GenerateFeedbackText();
        UIManager.Instance.EnableGame2Feedback(feedbackText);
        UIManager.Instance.EnableMainMenuCanvas();

        gameManager.LoadMenu();
    }

    private void GenerateFeedbackText()
    {
        feedbackText = "Super! Spiel abgeschlossen. Hier ein kurzes Feedback, wie du dich geschlagen hast:\n" + timeFeedbackText;
    }

    private void StartGame2()
    {
        SunMovement sunRotator = sunDirect.GetComponent<SunMovement>();
        sunPositions = sunRotator.rotations.Count;
        sunRotator.GetNextRotByRandom();
    }

    public void ResetPlayerPos(bool hit)
    {
        XROrigin player = FindAnyObjectByType<XROrigin>();
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        if(!hit)
        {
            SunMovement sunRotator = sunDirect.GetComponent<SunMovement>();
            sunRotator.GetNextRotByRandom();
        }
        else
        {
            gameManager.SwitchInteractor("TELEPORT");
        }
    }

    public Quaternion GetSunRotation()
    {
        Quaternion sunRot = sunDirect.transform.rotation;
        return sunRot;
    }
}
