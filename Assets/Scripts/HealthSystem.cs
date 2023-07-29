using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class HealthSystem : MonoBehaviour, IFillable
{
    [Header("HealthValues")] 
    [SerializeField] protected int maxHealthPoints;
    [SerializeField] protected float _currentHealth;
    
    [Header("Unity Events")]
    public UnityEvent onDeath;
    public UnityEvent onHit;
    [Header("Channel")] 
    [SerializeField] protected FillUIChannelSO fillUIChannel;

    /// <summary>
    /// Value for the current health of the character
    /// Every time its sets Raises the FillUI Channel
    /// </summary>
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            fillUIChannel.RaiseEvent(this as IFillable);
        }
    }
    private void Start()
    {
        CurrentHealth = maxHealthPoints;
    }
    public virtual void ReceiveDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public bool IsAlive()
    {
        return (CurrentHealth > 0);
    }
    public abstract void Deactivate();

    public float GetCurrentFillValue()
    {
        return CurrentHealth;
    }

    public float GetMaxFillValue()
    {
        return maxHealthPoints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Bullet>(out var bullet))
            return;

        onHit.Invoke();
        ReceiveDamage(bullet.Damage);
        bullet.DestroyGameObject();
        if (!IsAlive())
        {
            Deactivate();
        }
    }
}