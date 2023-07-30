using UnityEngine;

public class EnemyShootingLasers : EnemyShooting
{
    [Header("ScriptableObjects")]
    [SerializeField] private AskForLaserChannel askForLaserChannel;
    [SerializeField] private LaserConfiguration laserConfig;

    protected override void Shoot(Transform shooting)
    {
        askForLaserChannel.RaiseEvent(shooting, LayerMask.LayerToName(gameObject.layer), laserConfig, shooting,
            shooting.rotation);
    }
}