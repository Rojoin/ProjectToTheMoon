using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] private VoidChannelSO nextSceneChannel;
    [SerializeField] private VoidChannelSO goBackToMenuChannel;
    [SerializeField] private VoidChannelSO goToFirstLevelChannel;
    [SerializeField] private VoidChannelSO goToTutorialChannel;
    [SerializeField] private VoidChannelSO resetSceneChannel;
    [SerializeField] private string firstSceneName = "Level1";
    [SerializeField] private string tutorialSceneName = "Tutorial";
    [SerializeField] private string menuSceneName = "MainMenu";

    private void Awake()
    {
        nextSceneChannel.Subscribe(GoToNextLevel);
        goBackToMenuChannel.Subscribe(GoBackToMenu);
        goToFirstLevelChannel.Subscribe(GoToFirstLevel);
        goToTutorialChannel.Subscribe(GoToTutorial);
        resetSceneChannel.Subscribe(ResetScene);
    }

    private void OnDisable()
    {
        nextSceneChannel.Unsubscribe(GoToNextLevel);
        goBackToMenuChannel.Unsubscribe(GoBackToMenu);
        goToFirstLevelChannel.Unsubscribe(GoToFirstLevel);
        goToTutorialChannel.Unsubscribe(GoToTutorial);
        resetSceneChannel.Unsubscribe(ResetScene);
    }

    private void GoToTutorial()
    {
        SceneManager.LoadScene(tutorialSceneName);
    } 
    private void GoToFirstLevel()
    {
        SceneManager.LoadScene(firstSceneName);
    }
    private void GoBackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    } 
    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(menuSceneName);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}