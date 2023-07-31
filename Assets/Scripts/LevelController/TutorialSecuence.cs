using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class for the TutorialSecuence
/// </summary>
public class TutorialSecuence : MonoBehaviour
{
    [SerializeField] private List<PopUpText> popUpText;
    [FormerlySerializedAs("position")] [SerializeField]
    private Transform enemyPos;
    [SerializeField] private float maxTimeBetweenText;
    [SerializeField] private VoidChannelSO OnEnemyDead;
    [SerializeField] private float timeUntilChangeScene = 2f;
    [SerializeField] private VoidChannelSO goBackToMenu;
    [SerializeField] private EnemyManager factory;
    private EnemyHealth enemy;
    private int currentTextCounter = -1;
    private int maxTextCounter = 0;
    private float currentTimeBetweenText;

    private void Awake()
    {
        OnEnemyDead.Subscribe(CheckIfGameShouldEnd);
    }

    private void OnDisable()
    {
        OnEnemyDead.Unsubscribe(CheckIfGameShouldEnd);
    }

    private IEnumerator Start()
    {
        factory.SpawnEnemy(EnemyType.Tank, this.transform, out enemy);
        enemy.GetComponent<EnemyShooting>().enabled = false;
        enemy.transform.position = enemyPos.position;
        enemy.transform.rotation = enemyPos.rotation;
        currentTextCounter = -1;
        currentTimeBetweenText = maxTimeBetweenText;
        maxTextCounter = popUpText.Count - 1;
        while (gameObject.activeSelf)
        {
            ShowMessage();
            yield return new WaitForSeconds(maxTimeBetweenText);
        }
    }



    /// <summary>
    /// Show a PopUpText according to time
    /// </summary>
    private void ShowMessage()
    {
        if (currentTextCounter < maxTextCounter)
        {
            if (currentTextCounter > -1)
            {
                popUpText[currentTextCounter].DeactivateBox();
            }

            currentTextCounter++;
            Debug.Log(currentTextCounter);
            popUpText[currentTextCounter].ActiveBox();
        }
    }

    /// <summary>
    /// Check if the condition to end the tutorial is fullfil and changes scene
    /// </summary>
    private void CheckIfGameShouldEnd()
    {
        Invoke(nameof(GoBackToMenu), timeUntilChangeScene);
    }

    /// <summary>
    /// Load MenuScene
    /// </summary>
    private void GoBackToMenu()
    {
        goBackToMenu.RaiseEvent();
    }
}