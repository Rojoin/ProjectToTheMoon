using System;
using UnityEngine;

public class EnemyHealth : HealthSystem
{
    private Action<EnemyHealth> killAction;
    [Header("Values")]
    public bool isActive;
    [SerializeField] private IntChannelSO OnScoreUpChannel;
    [SerializeField] private int scoreValue;

    public void Init(Action<EnemyHealth> OnKill)
    {
        killAction = OnKill;
    }

    public override void Deactivate()
    {
        isActive = false;
        if (!IsAlive())
        {
            onDeath.Invoke();
            OnScoreUpChannel.RaiseEvent(scoreValue);
        }
        transform.gameObject.SetActive(false);
        killAction(this);
    }
}