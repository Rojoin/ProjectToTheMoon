using System;
using Cinemachine;
using TreeEditor;
using UnityEngine;

public class EnemyOnGroundManager : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart dolly;
    [SerializeField] private GroundEnemyActivation[] pattern;
    [SerializeField] private float[] activationPoint;
    private int nextActivationPoints = 0;
    private float currentDollyPosition;
    private void Update()
    {
        ActivatePattern();
    }
    private void ActivatePattern()
    {
        currentDollyPosition = dolly.m_Position;
        if (nextActivationPoints < activationPoint.Length && currentDollyPosition >= activationPoint[nextActivationPoints])
        {
            pattern[nextActivationPoints].ActivateEnemies();
            nextActivationPoints++;
        }
    }
}