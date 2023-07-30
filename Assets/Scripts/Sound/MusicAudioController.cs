using UnityEngine;

public class MusicAudioController : AudioController
{
    protected override void PlayAudio(AudioClip clip, float soundVolume = 1)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}