using UnityEngine;

public class EnemyHealth : HealthSystem
{
    [Header("Values")]
    public bool isActive;
    [SerializeField] private IntChannelSO OnScoreUpChannel;
    [SerializeField] private int scoreValue;

    public override void Deactivate()
    {
        isActive = false;
        onDeath.Invoke();
        if (!IsAlive())
        {
            OnScoreUpChannel.RaiseEvent(scoreValue);
        }
        transform.gameObject.SetActive(false);
    }
}