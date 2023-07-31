using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] private Transform enemyPos;
    [SerializeField] private Transform player;
    [SerializeField] private Transform holder;
    [SerializeField] private EnemyManager factory;

    private void Start()
    {
        Debug.Log("Boss Awakens");
        factory.SpawnEnemy(EnemyType.Boss,holder,out var enemy);
        enemy.transform.position = enemyPos.position;
        enemy.GetComponent<BossAttacks>().playerTransforms = player;
    }
}