using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public float fadeDuration;
    [SerializeField] private Color fadeColor;
    private Renderer rend;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        FadeIn();
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    private void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    private IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while(timer <= fadeDuration)
        {
            fadeColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.color = fadeColor;

            timer += Time.deltaTime;
            yield return null;
        }
        fadeColor.a = alphaOut;
        rend.material.color = fadeColor;
    }
}
