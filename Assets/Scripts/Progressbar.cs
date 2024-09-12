using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progressbar : MonoBehaviour
{
    [SerializeField] private Slider gameScoreProgress;
    [SerializeField] private Image fillIMG;
    [SerializeField] private TMP_Text gameScoreText;
    [SerializeField] private Color[] colors;

    public void GetCurrentFill(float current)
    {
        int currentInt = Mathf.RoundToInt(current);
        gameScoreProgress.value = currentInt;
        gameScoreText.text = "Hautkrebsrisiko: " + currentInt.ToString() + "%";
        if (gameScoreProgress.value <= 20)
        {
            fillIMG.color = colors[0];
        }
        else if (gameScoreProgress.value <= 40)
        {
            fillIMG.color = colors[1];
        }
        else if (gameScoreProgress.value > 40)
        {
            fillIMG.color = colors[2];
        }
    }
}
