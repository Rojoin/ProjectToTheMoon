﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private PlayerSettings player;
    private List<Bullet> _bullets;
    [SerializeField]
    Transform rayPosition;
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private ParticleSystem prefire;
    [SerializeField]
    private ParticleSystem fireLaser;
    public int raycastDistance;
    private bool isPressingButton;
    [Header("Cooldowns Presets")]
    private float currentBeanTimer;
    private bool canFireSpecialBean;
    private float specialBeanCooldown;
    private float specialBeanCooldownTimer = 0.0f;
    private float specialBeanTimer = 1.2f;
    private float minHoldShootTimer = 0.2f;
    private float currentHoldShootTimer;
    private float minShootTimer = 0.05f;
    private float currentSingleShootTimer;

    private bool singleBulletShoot;
    [SerializeField]
    private Transform bulletHolder;

    private void Awake()
    {
        specialBeanCooldown = player.specialBeanCooldown;
        minShootTimer = player.minShootTimer;
        minHoldShootTimer = player.minHoldShootTimer;
    }
    private void Update()
    {
        AttackLogic();
    }
    private void AttackLogic()
    {

        SpecialBeanCooldownTimers();
        currentHoldShootTimer += Time.deltaTime;
        currentSingleShootTimer += Time.deltaTime;
        if (isPressingButton)
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

            if (currentBeanTimer > specialBeanTimer && canFireSpecialBean)
            {
                prefire.Play();

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
        var newBullet = Instantiate(bullet, rayPosition.position, bulletHolder.rotation, bulletHolder);

        newBullet.SetStartPosition(transform);
        newBullet.SetActiveState(true);
        newBullet.ResetBulletTimer();
    }

    public float GetSpecialBeanCooldown() => specialBeanCooldown;
    public float GetSpecialBeanCooldownTimer() => specialBeanCooldownTimer;

    public void ShootRay()
    {
        fireLaser.Play();
        if (CheckLaserHitBox(out var hit) && hit.collider.CompareTag("Enemy") && hit.collider.GetComponent<EnemyBaseStats>().isActive)
        {
            hit.collider.GetComponent<EnemyBaseStats>().CurrentHealth -= hit.collider.GetComponent<EnemyBaseStats>().CurrentHealth;

            Debug.Log("RayoLaser");
        }

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
    }
}
