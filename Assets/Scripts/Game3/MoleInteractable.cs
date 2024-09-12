using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleInteractable : MonoBehaviour
{
    private Game3Manager game3Manager;
    [SerializeField] private Sprite dotCurrentSprite;
    [SerializeField] private List<Sprite> dotBadSprites;
    [SerializeField] private Material correctSelectedMat;
    [SerializeField] private Material falseSelectedMat;
    [SerializeField] private Material missedMat;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip incorrectSound;
    [SerializeField] private AudioClip selectSound;
    public bool isAltered = false;
    private bool wasSelectedBefore = false;

    void Awake()
    {
        game3Manager = FindObjectOfType<Game3Manager>();
    }

    public void AlterThisMole()
    {
        int spriteIndex = Random.Range(0, dotBadSprites.Count);
        dotCurrentSprite = dotBadSprites[spriteIndex];
        isAltered = true;
    }

    public void OnMoleSelected()
    {
        if(!wasSelectedBefore)
        {
            AudioManager.Instance.PlayClipOnce(selectSound);
            game3Manager.InspectDot(this, dotCurrentSprite);
        }
    }

    public void OnMoleVoted(bool moleBad)
    {
        if(moleBad == isAltered)
        {
            gameObject.GetComponent<MeshRenderer>().material = correctSelectedMat;
            AudioManager.Instance.PlayClipOnce(correctSound);
            game3Manager.OnVoteCorrect(this, isAltered);
        }
        else
        {
            if(isAltered)
            {
                gameObject.GetComponent<MeshRenderer>().material = falseSelectedMat;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material = missedMat;
            }
            AudioManager.Instance.PlayClipOnce(incorrectSound);
            game3Manager.OnVoteFalse(this, isAltered);
        }
        wasSelectedBefore = true;
    }

    public void SetMissedMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = falseSelectedMat;
        wasSelectedBefore = true;
    }

    public void BlockSelection()
    {
        wasSelectedBefore = true;
    }
}