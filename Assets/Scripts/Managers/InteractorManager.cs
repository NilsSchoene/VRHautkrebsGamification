using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorManager : MonoBehaviour
{
    [SerializeField] private GameObject rayInteractorLeft;
    [SerializeField] private GameObject rayInteractorRight;
    [SerializeField] private GameObject directInteractorLeft;
    [SerializeField] private GameObject directInteractorRight;
    [SerializeField] private GameObject teleportInteractorRight;

    public void SwitchInteractor(string interactorType)
    {
        switch(interactorType)
        {
            case "RAY":
                rayInteractorLeft.SetActive(true);
                rayInteractorRight.SetActive(true);
                directInteractorLeft.SetActive(false);
                directInteractorRight.SetActive(false);
                teleportInteractorRight.SetActive(false);
                break;
            case "DIRECT":
                rayInteractorLeft.SetActive(false);
                rayInteractorRight.SetActive(false);
                directInteractorLeft.SetActive(true);
                directInteractorRight.SetActive(true);
                teleportInteractorRight.SetActive(false);
                break;
            case "TELEPORT":
                rayInteractorLeft.SetActive(false);
                rayInteractorRight.SetActive(false);
                directInteractorLeft.SetActive(false);
                directInteractorRight.SetActive(false);
                teleportInteractorRight.SetActive(true);
                break;
            default:
                Debug.Log("InteractorManager: Wrong interactorType selected.");
                break;
        }
    }
}
