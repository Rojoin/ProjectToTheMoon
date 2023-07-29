using UnityEngine;

public class PlayerCheats : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] private VoidChannelSO OnInfinityLaser;
    [SerializeField] private VoidChannelSO OnFlash;
    [SerializeField] private VoidChannelSO OnNextLevel;
    [SerializeField] private VoidChannelSO OnGodMode;
    [SerializeField] private VoidChannelSO OnNuke;
    [SerializeField] private StringChannelSO OnCheatActivated;
    [Header("References")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerSettings playerSettings;
    [Header("Variables")]
    [SerializeField] private string cheatText = "Cheat:";
    [SerializeField] private float cheatSpeed;
    [SerializeField] private float cheatLaserCooldown;
    private float normalSpeed;
    private float normalLaserCooldown;
    private bool isGodModeActive;
    private bool isFlashModeActive;
    private bool isInfinityLaserActive;

    private void Awake()
    {
        isGodModeActive = false;
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<PlayerShooting>();
        normalLaserCooldown = playerSettings.specialBeanCooldown;
        normalSpeed = playerSettings.xySpeed;
        OnInfinityLaser.Subscribe(InfinityLaser);
        OnFlash.Subscribe(Flash);
        OnNextLevel.Subscribe(NextLevel);
        OnGodMode.Subscribe(GodMode);
        OnNuke.Subscribe(Nuke);
    }

    private void OnDisable()
    {
        OnInfinityLaser.Unsubscribe(InfinityLaser);
        OnFlash.Unsubscribe(Flash);
        OnNextLevel.Unsubscribe(NextLevel);
        OnGodMode.Unsubscribe(GodMode);
        OnNuke.Unsubscribe(Nuke);
    }

    private void Nuke()
    {
        var enemyHealths = Object.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        foreach (var enemy in enemyHealths)
        {
            enemy.ReceiveDamage(9999999);
            enemy.Deactivate();
        }

        OnCheatActivated.RaiseEvent(cheatText + nameof(Nuke));
    }

    private void GodMode()
    {
        isGodModeActive = !isGodModeActive;
        playerHealth.GodMode = isGodModeActive;

        var textToShow = cheatText + nameof(GodMode) + " " + (isGodModeActive ? "On" : "Off");
        OnCheatActivated.RaiseEvent(textToShow);
    }

    private void NextLevel()
    {
        OnCheatActivated.RaiseEvent(cheatText + nameof(NextLevel));
    }

    private void Flash()
    {
        isFlashModeActive = !isFlashModeActive;
        playerMovement.xySpeed = isFlashModeActive ? cheatSpeed : normalSpeed;
        var textToShow = cheatText + nameof(Flash) + " " + (isFlashModeActive ? "On" : "Off");
        OnCheatActivated.RaiseEvent(textToShow);
    }

    private void InfinityLaser()
    {
        isInfinityLaserActive = !isInfinityLaserActive;
        playerShooting.specialBeanCooldown = isInfinityLaserActive ? cheatLaserCooldown : normalLaserCooldown;

        var textToShow = cheatText + nameof(InfinityLaser) + " " + (isInfinityLaserActive ? "On" : "Off");
        OnCheatActivated.RaiseEvent(textToShow);
    }
}