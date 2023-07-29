using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CheatsUIText : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private TextMeshProUGUI textGUI;
    [SerializeField] private float cheatTextTime;
    [Header("Channel")]
    [SerializeField] private StringChannelSO cheatChannelSO;

    private void Awake()
    {
        cheatChannelSO.Subscribe(ActivateCheatText);
    }

    private void OnDisable()
    {
        cheatChannelSO.Unsubscribe(ActivateCheatText);
    }

    private void ActivateCheatText(string cheatText)
    {
        StartCoroutine(CheatText(cheatText));
    }

    private IEnumerator CheatText(string cheatText)
    {
        textGUI.text = cheatText;
        textGUI.enabled = true;
        yield return new WaitForSeconds(cheatTextTime);
        textGUI.enabled = false;
        yield break;
    }
}