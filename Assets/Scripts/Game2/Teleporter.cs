using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private Game2Manager game2Manager;

    [SerializeField] private List<Quaternion> correctSunRotations;
    [SerializeField] private Canvas teleporterCanvas;
    [SerializeField] private TMP_Text onTeleportText;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;
    private bool hit = true;

    void Awake()
    {
        game2Manager = FindObjectOfType<Game2Manager>();
    }

    public void OnTeleport()
    {
        StartCoroutine(WaitForTeleportDelay());
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.SwitchInteractor("RAY");
    }

    public IEnumerator WaitForTeleportDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Quaternion currentSunRot = game2Manager.GetSunRotation();

        foreach(Quaternion q in correctSunRotations)
        {
            if(Quaternion.Angle(q, currentSunRot) < 1)
            {
                hit = false;
            }
        }

        if(!hit)
        {
            AudioManager.Instance.PlayClipOnce(correctSound);
            onTeleportText.text = "Super! Schattenplatz gefunden.";
            game2Manager.checkForSunHits = false;
        }
        else
        {
            AudioManager.Instance.PlayClipOnce(incorrectSound);
            onTeleportText.text = "Nicht im Schatten! Suche einen neuen Schattenplatz.";
        }
        
        teleporterCanvas.gameObject.SetActive(true);
        teleporterCanvas.worldCamera = FindObjectOfType<Camera>();
    }
    public void OnTeleportButtonClicked()
    {
        teleporterCanvas.gameObject.SetActive(false);
        game2Manager.ResetPlayerPos(hit);
        hit = true;
    }
}
