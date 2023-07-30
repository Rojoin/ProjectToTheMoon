using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    [FormerlySerializedAs("askForEnemy")]
    [FormerlySerializedAs("askForBulletChannel")]
    [Header("Channels")]
    [SerializeField] private AskForEnemySO askForEnemyChannel;
    [SerializeField] private EnemyHealth basicEnemy;
    [SerializeField] private EnemyHealth tankEnemy;
    [SerializeField] private EnemyHealth laserEnemy;
    [SerializeField] private EnemyHealth turretEnemy;
    [SerializeField] private EnemyHealth bossEnemy;
    [SerializeField] private Transform holder;
    private EnemyFactory factory = new EnemyFactory();

    private ObjectPool<EnemyHealth> basicEnemyPool;
    private ObjectPool<EnemyHealth> tankEnemyPool;
    private ObjectPool<EnemyHealth> laserEnemyPool;
    private ObjectPool<EnemyHealth> turretEnemyPool;
    private ObjectPool<EnemyHealth> bossEnemyPool;

    private void Awake()
    {
        basicEnemyPool = new ObjectPool<EnemyHealth>(() => Instantiate(basicEnemy,holder),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, 5, 40);
        tankEnemyPool = new ObjectPool<EnemyHealth>(() => Instantiate(tankEnemy,holder),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, 5, 30);
        laserEnemyPool = new ObjectPool<EnemyHealth>(() => Instantiate(laserEnemy,holder),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, 5, 30);
        turretEnemyPool = new ObjectPool<EnemyHealth>(() => Instantiate(turretEnemy,holder),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, 6, 16);
        bossEnemyPool = new ObjectPool<EnemyHealth>(() => Instantiate(bossEnemy,holder),
            enemy => { enemy.gameObject.SetActive(true); }, enemy => { enemy.gameObject.SetActive(false); },
            enemy => { Destroy(enemy.gameObject); }, false, 1, 1);
    }

    private void KillEnemyBasic(EnemyHealth enemy)
    {
        basicEnemyPool.Release(enemy);
    }

    private void KillEnemyTank(EnemyHealth enemy)
    {
        tankEnemyPool.Release(enemy);
    }

    private void KillEnemyLaser(EnemyHealth enemy)
    {
        laserEnemyPool.Release(enemy);
    }

    private void KillEnemyTurret(EnemyHealth enemy)
    {
        turretEnemyPool.Release(enemy);
    }

    private void KillEnemyBoss(EnemyHealth enemy)
    {
        bossEnemyPool.Release(enemy);
    }

    public void SpawnEnemy(EnemyType type, Transform holder, out EnemyHealth enemy)
    {
        enemy = null;
        switch (type)
        {
            case EnemyType.Basic:
                basicEnemyPool.Get(out enemy);
                enemy.Init(KillEnemyBasic);
                break;
            case EnemyType.Tank:
                tankEnemyPool.Get(out enemy);
                enemy.Init(KillEnemyTank);
                break;
            case EnemyType.Laser:
                laserEnemyPool.Get(out enemy);
                enemy.Init(KillEnemyLaser);
                break;
            case EnemyType.Turret:
                turretEnemyPool.Get(out enemy);
                enemy.Init(KillEnemyTurret);
                break;
            case EnemyType.Boss:
                bossEnemyPool.Get(out enemy);
                enemy.Init(KillEnemyBoss);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        factory.Configure(ref enemy, holder);
    }
}