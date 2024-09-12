using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private FadeScreen fadeScreen;
    [SerializeField] private GameObject mainMenuEnvironment;
    private int sceneIndex = 0;
    
    public void LoadScene1()
    {
        StartCoroutine(LoadScene1Async());
    }

    private IEnumerator LoadScene1Async()
    {
        fadeScreen.FadeOut();
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game1", LoadSceneMode.Additive);
        operation.allowSceneActivation = false;

        float timer = 0;
        while(timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        UnloadActiveScenes();
        operation.allowSceneActivation = true;
        sceneIndex = 1;
        UIManager.Instance.EnableGame1Intro();
        fadeScreen.FadeIn();
    }

    public void LoadScene2()
    {
        StartCoroutine(LoadScene2Async());
    }

    private IEnumerator LoadScene2Async()
    {
        fadeScreen.FadeOut();
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game2", LoadSceneMode.Additive);
        operation.allowSceneActivation = false;

        float timer = 0;
        while(timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        UnloadActiveScenes();
        mainMenuEnvironment.SetActive(false);
        operation.allowSceneActivation = true;
        sceneIndex = 2;
        UIManager.Instance.EnableGame2Intro();
        fadeScreen.FadeIn();
    }
    
    public void LoadScene3()
    {
        UnloadActiveScenes();
        SceneManager.LoadScene("Game3", LoadSceneMode.Additive);
        UIManager.Instance.EnableGame3Intro();
        sceneIndex = 3;
    }

    public void UnloadActiveScenes()
    {
        if (sceneIndex != 0)
        {
            SceneManager.UnloadSceneAsync("Game" + sceneIndex);
            sceneIndex = 0;
        }
    }
}
