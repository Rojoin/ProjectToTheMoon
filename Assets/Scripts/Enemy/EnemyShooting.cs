using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// Class for the EnemyShooting
/// </summary>
public abstract class EnemyShooting : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] private Transform shootingPoints;
    private Transform[] bulletPoint;

    [FormerlySerializedAs("shootBulletCooldown")]
    [Header("Cooldowns Presets")]
    [SerializeField] protected float attackCooldown = 0.2f;
    private float currentAttackTimer = 0.0f;
    protected bool isActive = false;
    protected bool isAlive;
    protected EnemyHealth _enemyBaseStats;


    private void Awake()
    {
        _enemyBaseStats = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        bulletPoint = shootingPoints.transform.Cast<Transform>().ToArray();
        isActive = true;
    }


    private void Update()
    {
        if (LevelController.levelStatus != LevelController.LevelState.playing) return;
        isAlive = _enemyBaseStats.IsAlive();
        ShootLogic();
    }

    /// <summary>
    /// Logic for shooting bullets according to the timer
    /// One bullet for each bulletPoint
    /// Bullets spawn lookit at the player
    /// </summary>
    private void ShootLogic()
    {
        if (isActive && isAlive)
        {
            currentAttackTimer += Time.deltaTime;
            if (currentAttackTimer > attackCooldown)
            {
                foreach (var shootingPoint in bulletPoint)
                {
                    Shoot(shootingPoint);
                }

                currentAttackTimer -= attackCooldown;
            }
        }
    }

    /// <summary>
    /// Intantiate a attack in the <paramref name="shooting"/> position
    /// </summary>
    /// <param name="shooting">Transform from where the attack spawn</param>
    protected abstract void Shoot(Transform shooting);
}