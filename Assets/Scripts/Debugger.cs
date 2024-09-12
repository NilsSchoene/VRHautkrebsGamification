using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debugger : MonoBehaviour
{
    [SerializeField] private TMP_Text fpsText;
    private int fps;

    void Update()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
        gameObject.transform.position = Camera.main.transform.position;
        calculateFps();
    }

    private void calculateFps()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        fpsText.text = "FPS: " + fps.ToString();
    }
}
