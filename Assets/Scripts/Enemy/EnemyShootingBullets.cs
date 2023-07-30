using UnityEngine;

public class EnemyShootingBullets : EnemyShooting
{
    [Header("ScriptableObjects")]
    [SerializeField] private AskForBulletChannelSO askForBulletChannel;
    [SerializeField] private BulletConfiguration bulletConfiguration;

    protected override void Shoot(Transform shooting)
    {
        askForBulletChannel.RaiseEvent(shooting, LayerMask.LayerToName(gameObject.layer), bulletConfiguration,
            shooting.rotation);
    }
}