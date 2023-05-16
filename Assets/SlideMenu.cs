using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlideMenu : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI textMesh;
    [SerializeField] private GameObject currentbutton;
    [SerializeField] private PopUpText screen;
    private int playerScore;
    [SerializeField] private CanvasGroup screenCanvas;
    private bool isActive;
    private static MoveCrosshair cross;

    private void Awake()
    {
        screenCanvas = GetComponent<CanvasGroup>();
        cross = GameObject.Find("CrossHair").GetComponent<MoveCrosshair>();
    }

    void Start()
    {
        screenCanvas.interactable = false;
        screenCanvas.blocksRaycasts = false;
        isActive = false;
    }



    public void OpenSlide()
    {
        if (isActive) return;
        if (this.CompareTag("EndScreen"))
        {
            GetComponentInParent<UIController>().EndScreenSecuence();
        }
        isActive = true;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(currentbutton);
        screen.ActiveBox();
        playerScore = PlayerController.Score;
        textMesh.text = "Score:" + playerScore;
        screenCanvas.interactable = true;
        screenCanvas.alpha = 1;
        screenCanvas.blocksRaycasts = true;
    }

    public void GoBackToMenu()
    {
        screen.DeactivateBox();
        Invoke(nameof(LoadMenu), 0.5f);
        cross.ResetCrosshair();
    }
    public void ResetGame()
    {
        screen.DeactivateBox();
        Invoke(nameof(ResetScene), 0.5f);
        cross.ResetCrosshair();
    }

    public void ContinueToNextGame()
    {
        screen.DeactivateBox();
        Invoke(nameof(LoadNextScene), 0.5f);
        cross.ResetCrosshair();
    }

    public void ReturnToGame()
    {
        isActive = false;
        Debug.Log("Return");
        screen.DeactivateBox();
    }

    private void LoadMenu()
    {
        // SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene("MainMenu");
        cross.ResetCrosshair();
    }

    private void LoadNextScene()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(SceneManager.sceneCount + 1);
        cross.ResetCrosshair();
    }
    private void ResetScene()
    {
        //SoundManager.Instance.PlaySound(SoundManager.Instance.button);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        cross.ResetCrosshair();
    }
}