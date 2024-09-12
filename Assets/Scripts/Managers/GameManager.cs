using UnityEngine;
using System;
using System.IO;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private InteractorManager interactorManager;
    [SerializeField] private GameObject mainMenuEnvironment;
    [SerializeField] private Progressbar gameScoreBar;
    [SerializeField] private float gameScore;
    [SerializeField] private AudioClip mainMenuMusic;

    private float scoreGame1Items;
    private float scoreGame1Time;
    private float scoreGame2;
    private float scoreGame2Time;
    private float scoreGame3;
    private float scoreGame3Missed;

    void Awake()
    {
        gameScoreBar.GetCurrentFill(gameScore);
    }

    void Start()
    {
        AudioManager.Instance.PlayAmbienceClip(mainMenuMusic);
    }

    public void UpdateScore(float scoreToAdd)
    {
        gameScore = gameScore + scoreToAdd;
        gameScoreBar.GetCurrentFill(gameScore);
    }

    public void LoadGame1()
    {
        sceneLoader.LoadScene1();
        SwitchInteractor("DIRECT");
    }

    public void LoadGame2()
    {
        sceneLoader.LoadScene2();
    }

    public void StartGame2()
    {
        Game2Manager game2Manager = FindObjectOfType<Game2Manager>();
        UIManager.Instance.DisableAllSections();
        UIManager.Instance.DisableMainMenuCanvas();
        game2Manager.StartGame();
    }

    public void LoadGame3()
    {
        sceneLoader.LoadScene3();
        SwitchInteractor("RAY");
    }

    public void StartGame3()
    {
        Game3Manager game3Manager = FindObjectOfType<Game3Manager>();
        UIManager.Instance.DisableAllSections();
        UIManager.Instance.DisableMainMenuCanvas();
        game3Manager.StartGameplay();
    }

    public void LoadGameFeedback()
    {
        LoadMenu();
        UIManager.Instance.EnableGameEnd();
        WriteLogFile();
    }

    public void SaveScoreGame1Items(float score)
    {
        scoreGame1Items = score;
    }

    public void SaveScoreGame1Time(float score)
    {
        scoreGame1Time = score;
    }

    public void SaveScoreGame2(float score, float time)
    {
        scoreGame2 = score;
        scoreGame2Time = time;
    }

    public void SaveScoreGame3(float score, float missed)
    {
        scoreGame3 = score;
        scoreGame3Missed = missed;
    }

    private void WriteLogFile()
    {
        string dateAndTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        using(StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Log_" + dateAndTime + ".txt",true))
        {
            sw.WriteLine("scoreg1_items: " + scoreGame1Items.ToString());
            sw.WriteLine("scoreg1_time: " + scoreGame1Time.ToString());
            sw.WriteLine("scoreg2: " + scoreGame2.ToString());
            sw.WriteLine("scoreg2_time: " + scoreGame2Time.ToString());
            sw.WriteLine("scoreg3: " + scoreGame3.ToString());
            sw.WriteLine("scoreg3_missed: " + scoreGame3Missed.ToString());
        }
        Debug.Log("Log-File 'Log_" + dateAndTime + ".txt' saved to " + Application.dataPath.ToString());
    }

    public void LoadMenu()
    {
        sceneLoader.UnloadActiveScenes();
        mainMenuEnvironment.SetActive(true);
        AudioManager.Instance.PlayAmbienceClip(mainMenuMusic);
        SwitchInteractor("RAY");
    }

    public void SwitchInteractor(string type)
    {
        interactorManager.SwitchInteractor(type);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}