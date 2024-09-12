using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource environmentAudioSource;
    [SerializeField] private AudioSource globalAudioSource;
    [SerializeField] private AudioListener audioListener;

    public void PlayClipOnce(AudioClip audioClip)
    {
        globalAudioSource.PlayOneShot(audioClip);
    }

    public void PlayAmbienceClip(AudioClip audioClip)
    {
        environmentAudioSource.Pause();
        environmentAudioSource.clip = audioClip;
        environmentAudioSource.Play();
    }
}
