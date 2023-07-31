using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// Class for the EnemyMovementPattern
/// </summary>
public class EnemyMovementPattern : MonoBehaviour
{
    public EnemyManager enemyFactory;
    [SerializeField] private Transform enemyHolder;
    [SerializeField] private int defaultEnemyCount = 0;
    [FormerlySerializedAs("specialEnemyCount")] [SerializeField]
    private int tankEnemyCount = 0;
    [FormerlySerializedAs("laserEnemyCouunt")] [SerializeField]
    private int laserEnemyCount = 0;
    [SerializeField] private Color color;
    [SerializeField] private float speed;
    [SerializeField] private Transform[] points;
    [SerializeField] private bool shouldLoop = false;
    [SerializeField] private float loopTimes;
    [SerializeField] private int startLoop;
    [SerializeField] private int endLoop;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private int enemyCounter;
    public List<EnemyMovement> enemysToSpawn;
    private bool isActive = false;
    private int maxEnemysInScene;
    private float spawnTimer = 0.0f;

    private void OnValidate()
    {
        points = transform.Cast<Transform>().ToArray();
    }

    private void Start()
    {
        if (isActive)
            StartPattern();
    }

    /// <summary>
    /// Initialize enemy Pattern
    /// </summary>
    public void StartPattern()
    {
        endLoop = Mathf.Clamp(endLoop, startLoop, points.Length - 1);
        startLoop = Mathf.Clamp(startLoop, 0, endLoop - 1);
        SetMaxEnemiesInScreen();
        SetRandomEnemies();
        maxEnemysInScene = enemysToSpawn.Count;
        isActive = true;
    }

    /// <summary>
    /// Randomize enemies from list
    /// </summary>
    private void SetRandomEnemies()
    {
        for (int i = 0; i < enemysToSpawn.Count; i++)
        {
            var temp = enemysToSpawn[i];
            int randomIndex = Random.Range(i, enemysToSpawn.Count);
            enemysToSpawn[i] = enemysToSpawn[randomIndex];
            enemysToSpawn[randomIndex] = temp;
        }
    }

    /// <summary>
    /// Creates enemies according to type and sets their properties
    /// </summary>
    private void SetMaxEnemiesInScreen()
    {
        enemyCounter = 0;
        enemysToSpawn = new List<EnemyMovement>();

        for (int i = 0; i < defaultEnemyCount; i++)
        {
            enemyFactory.SpawnEnemy(EnemyType.Basic, enemyHolder, out var aux);
            var enemyToAdd = aux.GetComponent<EnemyMovement>();
            enemysToSpawn.Add(enemyToAdd);
        }

        for (int i = 0; i < tankEnemyCount; i++)
        {
            enemyFactory.SpawnEnemy(EnemyType.Tank, enemyHolder, out var aux);
            var enemyToAdd = aux.GetComponent<EnemyMovement>();
            enemysToSpawn.Add(enemyToAdd);
        }

        for (int i = 0; i < laserEnemyCount; i++)
        {
            enemyFactory.SpawnEnemy(EnemyType.Laser, enemyHolder, out var aux);
            var enemyToAdd = aux.GetComponent<EnemyMovement>();
            enemysToSpawn.Add(enemyToAdd);
        }

        foreach (var enemy in enemysToSpawn)
        {
            enemy.SetStartParameters(speed, shouldLoop, loopTimes, startLoop, endLoop, false, points);
        }
    }

    private void Update()
    {
        SpawnEnemies();
    }

    /// <summary>
    /// Activate Enemies in scene according to spawnTimer and enemy count
    /// </summary>
    private void SpawnEnemies()
    {
        if (isActive)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay && enemyCounter < maxEnemysInScene)
            {
                spawnTimer = 0.0f;
                enemysToSpawn[enemyCounter].SetActive();
                enemysToSpawn[enemyCounter].GetComponent<EnemyShooting>().enabled = true;
                enemyCounter++;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        for (int i = 0; i < points.Length - 1; i++)
        {
            Transform t = points[i];
            Transform next = points[i + 1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, 0.1f);
        for (int i = 0; i < points.Length - 1; i++)
        {
            if (i == points.Length - 1) return;
            Transform t = points[i];
            Transform next = points[i + 1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }
}