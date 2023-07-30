using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class for the MenuController
/// </summary>
public class MenuController : MonoBehaviour
{
    [SerializeField] private VoidChannelSO gotoFirstLevelChannel;
    [SerializeField] private VoidChannelSO goToTutorialChannel;
    [SerializeField] private AudioChannelSO musicChannel;
    [SerializeField] private AudioChannelSO sfxChannel;
    [SerializeField] private AudioClip button;
    [SerializeField] private AudioClip mainMenuTheme;
    [SerializeField] private CanvasGroup MainMenu;
    [SerializeField] private CanvasGroup Options;
    [SerializeField] private CanvasGroup Credits;


    private CanvasGroup currentActiveCanvas;

    private void Start()
    {
        currentActiveCanvas = MainMenu;

        SetCanvasState(MainMenu, true);
        SetCanvasState(Options, false);
        SetCanvasState(Credits, false);
        musicChannel.RaiseEvent(mainMenuTheme, 1);
    }

    /// <summary>
    /// Loads First GameScene
    /// </summary>
    public void GoToGame()
    {
        sfxChannel.RaiseEvent(button, 1);
        gotoFirstLevelChannel.RaiseEvent();
    }

    /// <summary>
    /// Loads TutorialScene
    /// </summary>
    public void GoToTutorial()
    { ;
        goToTutorialChannel.RaiseEvent();
        sfxChannel.RaiseEvent(button, 1);
    }

    /// <summary>
    /// Activate OptionsCanvas
    /// </summary>
    public void GoToOptions()
    {
        SetCanvasState(currentActiveCanvas, false);
        SetCanvasState(Options, true);
        currentActiveCanvas = Options;
        sfxChannel.RaiseEvent(button, 1);
    }

    /// <summary>
    /// Activate CreditsCanvas
    /// </summary>
    public void GoToCredits()
    {
        SetCanvasState(currentActiveCanvas, false);
        SetCanvasState(Credits, true);
        currentActiveCanvas = Credits;
        sfxChannel.RaiseEvent(button, 1);
    }

    /// <summary>
    /// Deactivate currentCanvas and goes Back To Menu
    /// </summary>
    public void GoBackToMenu()
    {
        SetCanvasState(currentActiveCanvas, false);
        SetCanvasState(MainMenu, true);
        currentActiveCanvas = MainMenu;
        sfxChannel.RaiseEvent(button, 1);
    }

    /// <summary>
    /// Exits the game
    /// If activate in Unity Editor exits playmode
    /// </summary>
    public void ExitAplication()
    {
        sfxChannel.RaiseEvent(button, 1);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
   	    Application.Quit();
#endif
    }

    /// <summary>
    /// Set the canvas interactable, block raycas and alpha according to the bool
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="state"></param>
    private void SetCanvasState(CanvasGroup canvas, bool state)
    {
        canvas.interactable = state;
        canvas.blocksRaycasts = state;
        canvas.alpha = state ? 1 : 0;
    }
}