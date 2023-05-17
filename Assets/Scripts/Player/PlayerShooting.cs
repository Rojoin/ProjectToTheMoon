﻿using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShooting : MonoBehaviour
{
    private bool canShoot;

    [SerializeField] private PlayerSettings player;
    [SerializeField] Transform rayPosition;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform World;
    [SerializeField] private ParticleSystem prefire;
    [SerializeField] private Transform[] shootingPoints;
    [SerializeField] private Transform cannon;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] [Range(0, 1)] private float shootVolume;
    [SerializeField] private float laserDamage = 200f;
    [SerializeField] private AudioClip laserClip;
    [SerializeField] [Range(0, 1)] private float laserVolume;
    [SerializeField] private AudioClip prepareLaserClip;
    [SerializeField] [Range(0, 1)] private float prepareLaserVolume;
    public int raycastDistance;
    private bool isPressingButton;

    [Header("Cooldowns Presets")]
    private float currentBeanTimer;
    private bool canFireSpecialBean;
    private float specialBeanCooldown;
    private float specialBeanCooldownTimer = 0.0f;
    private bool isChargingSpecialBean = false;
    private float specialBeanTimer = 1.2f;
    private float minHoldShootTimer = 0.2f;
    private float currentHoldShootTimer;
    private float minShootTimer = 0.05f;
    private float currentSingleShootTimer;

    private bool singleBulletShoot;
    [SerializeField]
    private Transform bulletHolder;

    [SerializeField] private GameObject rayObject;

    private void Awake()
    {
        shootingPoints = cannon.transform.Cast<Transform>().ToArray();
    }

    private void Start()
    {
        canShoot = true;
        specialBeanCooldown = player.specialBeanCooldown;
        minShootTimer = player.minShootTimer;
        minHoldShootTimer = player.minHoldShootTimer;
        PlayerMovement.OnRoll += PlayerMovement_OnRoll;
    
    }

    private void PlayerMovement_OnRoll(bool isOnRoll)
    {
        canShoot = !isOnRoll;
    }
    private void OnDestroy()
    {
        PlayerMovement.OnRoll -= PlayerMovement_OnRoll;
    }

    private void Update()
    {
        if (LevelController.levelStatus != LevelController.LevelState.playing) return;
        AttackLogic();
    }
    private void AttackLogic()
    {
        if (!canShoot) return;

        SpecialBeanCooldownTimers();
        currentHoldShootTimer += Time.deltaTime;
        currentSingleShootTimer += Time.deltaTime;
        if (isPressingButton)
        {
            if (!isChargingSpecialBean)
            {
                currentBeanTimer += Time.deltaTime;
                if (currentSingleShootTimer > minShootTimer && !singleBulletShoot)
                {
                    singleBulletShoot = true;
                    ShootBullet();
                }
                else if (currentHoldShootTimer > minHoldShootTimer && singleBulletShoot && currentSingleShootTimer > minHoldShootTimer)
                {
                    ShootBullet();
                    currentHoldShootTimer -= minHoldShootTimer;
                }
            }
            if (currentBeanTimer > specialBeanTimer && canFireSpecialBean)
            {
                prefire.Play();
                if (!isChargingSpecialBean)
                {
                    SoundManager.Instance.PlaySound(prepareLaserClip, prepareLaserVolume);
                }
                isChargingSpecialBean = true;
            }
            else
            {
                prefire.Stop();
            }

        }
        else
        {
            if (currentBeanTimer > specialBeanTimer && canFireSpecialBean)
            {
                Debug.Log("Disparo");
                ShootRay();
                canFireSpecialBean = false;
                specialBeanCooldownTimer = 0.0f;
            }
            singleBulletShoot = false;
            currentSingleShootTimer = 0.0f;
            currentBeanTimer = 0.0f;
            currentHoldShootTimer = minHoldShootTimer;
            isChargingSpecialBean = false;
            prefire.Stop();
        }

    }

    private void SpecialBeanCooldownTimers()
    {
        if (!canFireSpecialBean) specialBeanCooldownTimer += Time.deltaTime;
        if (!(specialBeanCooldownTimer > specialBeanCooldown)) return;
        canFireSpecialBean = true;

    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isPressingButton = true;

        }
        else if (ctx.canceled)
        {
            isPressingButton = false;
        }
    }
    private void ShootBullet()
    {
        SoundManager.Instance.PlaySound(shootClip, shootVolume);
        foreach (Transform shootingPos in shootingPoints)
        {
            var newBullet = Instantiate(bullet, shootingPos.position, transform.rotation, bulletHolder);
            var currentDirection = (World.transform.InverseTransformDirection(shootingPos.forward));

            newBullet.SetStartPosition(shootingPos);
            newBullet.SetActiveState(true);
            newBullet.ResetBulletTimer();
            newBullet.SetDirection(World);
            newBullet.SetDirection(currentDirection);
        }
    }

    public float GetSpecialBeanCooldown() => specialBeanCooldown;
    public float GetSpecialBeanCooldownTimer() => specialBeanCooldownTimer;

    public void ShootRay()
    {

        var newRay = Instantiate(rayObject, rayPosition.position, transform.rotation, transform);
        SoundManager.Instance.PlaySound(laserClip, laserVolume);
        newRay.transform.localPosition += new Vector3(0, 0, 45f);
        if (CheckLaserHitBox(out var hit) && hit.collider.CompareTag("Enemy") && hit.collider.GetComponent<EnemyBaseStats>().isActive)
        {
            hit.collider.GetComponent<EnemyBaseStats>().CurrentHealth -= hit.collider.GetComponent<EnemyBaseStats>().CurrentHealth;
            hit.collider.GetComponent<EnemyBaseStats>().CheckEnemyStatus();

        }

        if (CheckLaserHitBox(out hit) && hit.collider.CompareTag("Boss") && hit.collider.GetComponent<EnemyBaseStats>().isActive)
        {
            hit.collider.GetComponent<EnemyBaseStats>().CurrentHealth -= laserDamage;
            hit.collider.GetComponent<EnemyBaseStats>().CheckEnemyStatus();
        }
        Destroy(newRay, 0.2f);
    }
    private bool CheckLaserHitBox(out RaycastHit hit)
    {
        return Physics.Raycast(rayPosition.position, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.up / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.down / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.right / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.left / 2, rayPosition.forward, out hit, raycastDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayPosition.position, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.up / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.down / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.right / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.left / 2, rayPosition.forward * raycastDistance);
    }
}

