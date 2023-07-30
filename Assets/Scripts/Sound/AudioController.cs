using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] protected AudioChannelSO audioChannelSO;
    [SerializeField] protected FloatChannelSO volumeChannelSO;
    protected AudioSource audioSource;
    [SerializeField] private float volume;
    [SerializeField] private string sliderKey;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey(sliderKey))
        {
            SetVolume(PlayerPrefs.GetFloat(sliderKey));
        }
        else
        {
            SetVolume(1.0f);
        }
        volumeChannelSO.Subscribe(SetVolume);
        audioChannelSO.Subscribe(PlayAudio);
    }

    private void OnDisable()
    {
        audioChannelSO.Unsubscribe(PlayAudio);
        volumeChannelSO.Unsubscribe(SetVolume);
    }

    private void SetVolume(float newVolume)
    {
        this.volume = newVolume;
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(sliderKey, volume);
    }

    protected virtual void PlayAudio(AudioClip clip, float soundVolume = 1.0f) =>
        audioSource.PlayOneShot(clip, soundVolume);
}