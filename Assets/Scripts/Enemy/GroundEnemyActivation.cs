using UnityEngine;

public class GroundEnemyActivation : MonoBehaviour
{
    [SerializeField] private Transform[] enemyPositions;
    [SerializeField] private EnemyManager factory;
    [SerializeField] private Transform world;
    
    public void ActivateEnemies()
    {
        foreach (var position in enemyPositions)
        {
            factory.SpawnEnemy(EnemyType.Turret, world, out var enemy);
            enemy.transform.position = position.position;
            enemy.transform.rotation = position.rotation;
        }
    }
}