using UnityEngine;

public class AudioController : MonoBehaviour
{
  [Header("Channels")]
  [SerializeField] private AudioChannelSO audioChannelSO;
  [SerializeField] private FloatChannelSO volumeChannelSO;
  private AudioSource audioSource;
  [SerializeField] private float volume;
  
  private void Awake()
  {
    audioSource =GetComponent<AudioSource>();
    //volumeChannelSO.Subscribe(SetVolume);
    audioChannelSO.Subscribe(PlayAudio);
  }
  private void OnDisable()
  {
    audioChannelSO.Unsubscribe(PlayAudio);
   // volumeChannelSO.Unsubscribe(SetVolume);
  }
  private void SetVolume(float newVolume) => this.volume = newVolume;

  private void PlayAudio(AudioClip clip, float soundVolume = 1.0f) => audioSource.PlayOneShot(clip,soundVolume);
}
