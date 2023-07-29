using System;
using UnityEngine;

/// <summary>
/// Class for the PlayerController
/// </summary>
public class PlayerHealthSystem : MonoBehaviour, IFillable
{
    [Header("HealthValues")]
    [SerializeField] private int maxHealthPoints;
    [SerializeField] private float _currentHealth;
    [Header("Channels")]
    [SerializeField] private FillUIChannelSO fillUIChannel;
    [SerializeField] private VoidChannelSO playerDeathChannelSO;
    [Header("Effects")]
    [SerializeField] private ParticleSystem impactPrefab;
    [SerializeField] private ParticleSystem boom;
  

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

    /// <summary>
    /// Set the damage that the player received and starts the player deactivation 
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        CurrentHealth -= damage;
        if (!(CurrentHealth < 0.0f))
            return;
        playerDeathChannelSO.RaiseEvent();
        DeactivatePlayer();
    }

    /// <summary>
    /// Deactivates player and plays the death animation
    /// </summary>
    public void DeactivatePlayer()
    {
        var explosion = Instantiate(boom, transform.position, transform.rotation);
        explosion.Play();
        transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Bullet>(out var bullet))
            return;

        Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
        ReceiveDamage(bullet.Damage);
        bullet.DestroyGameObject();
    }

    /// <summary>
    /// Gets currentHealthPoints.
    /// Used for the PlayerHealthBar.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentFillValue()
    {
        return CurrentHealth;
    }

    /// <summary>
    /// Gets maxHealthPoints
    /// Used for the PlayerHealthBar
    /// </summary>
    /// <returns></returns>
    public float GetMaxFillValue()
    {
        return maxHealthPoints;
    }
}