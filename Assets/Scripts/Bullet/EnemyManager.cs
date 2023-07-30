using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager : MonoBehaviour
{
    [FormerlySerializedAs("askForEnemy")]
    [FormerlySerializedAs("askForBulletChannel")]
    [Header("Channels")]
    [SerializeField] private AskForEnemySO askForEnemyChannel;
    [SerializeField] private EnemyMovement basicEnemy;
    [SerializeField] private EnemyMovement tankEnemy;
    [SerializeField] private EnemyMovement laserEnemy;
    [SerializeField] private EnemyMovement turretEnemy;
    [SerializeField] private EnemyMovement bossEnemy;
    [SerializeField] private Transform holder;
    private EnemyFactory factory = new EnemyFactory();


    public void SpawnEnemy(EnemyType type,Transform holder, out EnemyMovement aux)
    {
        switch (type)
        {
            case EnemyType.Basic:
                aux = factory.CreateEnemy(basicEnemy,holder);
                break;
            case EnemyType.Tank:
                aux = factory.CreateEnemy(tankEnemy,holder);
                break;
            case EnemyType.Laser:
                aux = factory.CreateEnemy(laserEnemy,holder);
                break;
            case EnemyType.Turret:
                aux = factory.CreateEnemy(turretEnemy,holder);
                break;
            case EnemyType.Boss:
                aux = factory.CreateEnemy(bossEnemy,holder);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}