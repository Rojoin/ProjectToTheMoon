using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Class for the Boss attacks
/// </summary>
public class BossAttacks : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] private AskForBulletChannelSO askForBulletChannel;
    [FormerlySerializedAs("askForKLaserChannel")] [SerializeField]
    private AskForLaserChannel askForLaserChannel;
    [SerializeField] private BulletConfiguration bulletConfiguration;
    [SerializeField] private LaserConfiguration laserConfiguration;
    [FormerlySerializedAs("shootingPoints")]
    [Header("Transforms")]
    [SerializeField] private Transform bulletShootingPoints;
    [SerializeField] private Transform laserShootingPoints;
    [SerializeField] public Transform playerTransforms;
    private Transform[] bulletPoint;
    private Transform[] laserPoint;
    [Header("Cooldowns Presets")]
    [SerializeField] private float shootBulletCooldown = 0.2f;
    [SerializeField]  private float shootLaserCooldown = 1.2f;
    private float currentShootBulletCooldown = 0.0f;
    private float currentShootLaserCooldown = 0.0f;


    private void Start()
    {
        bulletPoint = bulletShootingPoints.transform.Cast<Transform>().ToArray();
        laserPoint = laserShootingPoints.transform.Cast<Transform>().ToArray();
    }

    private void Update()
    {
        if (LevelController.levelStatus != LevelController.LevelState.playing) return;
        Attack();
    }

    /// <summary>
    /// Logic for shooting bullets
    /// One bullet for each bulletPoint
    /// Bullets spawn looking at the player
    /// </summary>
    private void Attack()
    {
        currentShootBulletCooldown += Time.deltaTime;
        if (currentShootBulletCooldown > shootBulletCooldown)
        {
            foreach (var shoot in bulletPoint)
            {
                shoot.LookAt(playerTransforms.position);
                ShootBullet(shoot);
            }

            currentShootBulletCooldown -= shootBulletCooldown;
        }

        currentShootLaserCooldown += Time.deltaTime;
        if (currentShootLaserCooldown > shootLaserCooldown)
        {
            foreach (Transform laser in laserPoint)
            {
                ShootLaser(laser);
            }

            currentShootLaserCooldown -= shootLaserCooldown;
        }
    }

    /// <summary>
    /// Intantiate a Bullet in the <paramref name="shooting"/> position
    /// </summary>
    /// <param name="shooting">Transform from where the bullet spawn</param>
    private void ShootBullet(Transform shooting)
    {
        askForBulletChannel.RaiseEvent(shooting, LayerMask.LayerToName(gameObject.layer), bulletConfiguration,
            shooting.rotation);
    }

    private void ShootLaser(Transform shooting)
    {
        askForLaserChannel.RaiseEvent(shooting, LayerMask.LayerToName(gameObject.layer), laserConfiguration, shooting,
            shooting.rotation);
    }
}