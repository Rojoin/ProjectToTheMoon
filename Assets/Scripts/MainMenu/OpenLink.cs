using UnityEngine;
/// <summary>
/// Class for the OpenLink
/// </summary>
public class OpenLink : MonoBehaviour
{
    [SerializeField] private AudioChannelSO sfxChannel;
    [SerializeField] private AudioClip button;
    /// <summary>
    /// Open an Link
    /// </summary>
    /// <param name="Link">WebLink to open</param>
    public void OpenUrl(string Link)
    {
        sfxChannel.RaiseEvent(button, 1);
        Application.OpenURL(Link);
    }
}
