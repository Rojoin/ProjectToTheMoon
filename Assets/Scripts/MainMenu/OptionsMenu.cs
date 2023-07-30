using UnityEngine;
using static UnityEngine.Screen;

/// <summary>
/// Class for the OptionsMenu
/// </summary>

public class OptionsMenu : MonoBehaviour
{
    private bool isFullScreen = false;
    [SerializeField] private AudioChannelSO sfxChannel;
    [SerializeField] private AudioClip buttonSound;
    private void Start()
    {

        isFullScreen = fullScreenMode == FullScreenMode.FullScreenWindow;

    }
    /// <summary>
    /// Toggle Screen mode
    /// </summary>
    public void ChangeScreenMode()
    {
        sfxChannel.RaiseEvent(buttonSound,1);
        isFullScreen = !isFullScreen;
        var fullsScreenMode = isFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        SetResolution(1920, 1080, fullsScreenMode);
    }
    /// <summary>
    /// Toggle SFX mute state
    /// </summary>
    public void ChangeSFX()
    {        
        sfxChannel.RaiseEvent(buttonSound,1);
        //TODO
        //SoundManager.Instance.PlayButtonSound();
        //SoundManager.Instance.ToggleEffects();
    }
    /// <summary>
    /// Toggle Music mute state
    /// </summary>
    public void ChangeMusic()
    {
        sfxChannel.RaiseEvent(buttonSound,1);
        //TODO
        //SoundManager.Instance.PlayButtonSound();
        //SoundManager.Instance.ToggleEffects();
    }
}
