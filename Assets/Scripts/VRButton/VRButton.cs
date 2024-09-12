using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    [SerializeField] private bool isResetTrigger = false;
    [SerializeField] private AudioClip buttonClip;
    public UnityEvent onPressed, onReleased, onReset;

    private void OnTriggerEnter(Collider other)
    {
        if(isResetTrigger)
        {
            onReset?.Invoke();
        }
        else if(other.tag == "VRButton")
        {
            onPressed?.Invoke();
            AudioManager.Instance.PlayClipOnce(buttonClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "VRButton")
        {
            onReleased?.Invoke();
        }
    }
}
