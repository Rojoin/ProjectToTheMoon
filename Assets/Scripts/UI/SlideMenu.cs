using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the SlideMenu
/// </summary>
public class SlideMenu : MonoBehaviour
{
    [SerializeField] private AudioChannelSO sfxChannel;
    [SerializeField] private VoidChannelSO goToNextLevel;
    [SerializeField] private VoidChannelSO goToMenu;
    [SerializeField] private VoidChannelSO resetLevel;
    [SerializeField] private TMPro.TextMeshProUGUI textMesh;
    [SerializeField] private GameObject currentbutton;
    [SerializeField] private PopUpText screen;
    private int playerScore;
    [SerializeField] private CanvasGroup screenCanvas;
    private bool isActive;
    [SerializeField] private AudioClip openSlideSound;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private float timeUntilNextScene = 0.5f;
    [SerializeField] private string menuSceneName = "MainMenu";

    private void Awake()
    {
        screenCanvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        screenCanvas.interactable = false;
        screenCanvas.blocksRaycasts = false;
        isActive = false;
    }

    /// <summary>
    /// Activates the PopUpText and activates the CanvasGroup
    /// </summary>
    public void OpenSlide()
    {
        if (isActive) return;
        if (this.CompareTag("EndScreen"))
        {
            GetComponentInParent<UIController>().EndScreenSecuence();
        }

        sfxChannel.RaiseEvent(openSlideSound, 1);
        isActive = true;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(currentbutton);
        screen.ActiveBox();
        playerScore = LevelController.score;
        if (textMesh)
        {
            textMesh.text = "Score:" + playerScore;
        }

        screenCanvas.interactable = true;
        screenCanvas.alpha = 1;
        screenCanvas.blocksRaycasts = true;
    }

    /// <summary>
    /// Loads the menu scene after 0.5f, set Time.timeScale to 1 and resets the crosshair 
    /// </summary>
    public void GoBackToMenu()
    {
        Time.timeScale = 1;
        screen.DeactivateBox();
        Invoke(nameof(LoadMenu), 0.5f);
    }

    /// <summary>
    /// Resets the current scene after 0.5f, set Time.timeScale to 1 and resets the crosshair 
    /// </summary>
    public void ResetGame()
    {
        Time.timeScale = 1;
        screen.DeactivateBox();
        Invoke(nameof(ResetScene), timeUntilNextScene);
    }

    /// <summary>
    /// Loads the next scene after 0.5f, set Time.timeScale to 1 and resets the crosshair 
    /// </summary>
    public void ContinueToNextGame()
    {
        Time.timeScale = 1;
        screen.DeactivateBox();
        Invoke(nameof(LoadNextScene), timeUntilNextScene);
    }

    /// <summary>
    /// Close this screen
    /// </summary>
    public void ReturnToGame()
    {
        isActive = false;
        sfxChannel.RaiseEvent(buttonSound, 1);
        screen.DeactivateBox();
    }

    /// <summary>
    /// Loads MenuScene
    /// </summary>
    private void LoadMenu()
    {
        sfxChannel.RaiseEvent(buttonSound, 1);
        goToMenu.RaiseEvent();
    }

    /// <summary>
    /// Loads next Scene
    /// </summary>
    private void LoadNextScene()
    {
        sfxChannel.RaiseEvent(buttonSound, 1);
        goToNextLevel.RaiseEvent();
    }

    /// <summary>
    /// Reset CurrentScene
    /// </summary>
    private void ResetScene()
    {
        sfxChannel.RaiseEvent(buttonSound, 1);
        resetLevel.RaiseEvent();
    }
}