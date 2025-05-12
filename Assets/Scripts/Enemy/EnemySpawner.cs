using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemyPrefab;
    [Header("Spawn Parameters")]
    [SerializeField] private float innerRadius = 10f;
    [SerializeField] private float outerRadius = 20f;
    [Header("Survival")]
    [SerializeField] private bool enableContinuousEnemySpawning = false;
    [Tooltip("First enemy will spawn after this amount of seconds")]
    [SerializeField] private float initialSpawnTime = 1f;
    [Tooltip("Next enemy will appear so many times faster")]
    [SerializeField] private float spawnTimeIncrease = 1.01f;
    [SerializeField] private float minSpawnTime = 0.2f;

    private const int MAX_OVERLAP_CHECK_COUNT = 10;
    private const float DISTANCE_TO_GROUND_CHECK = 10f;

    private float enemyRadius;
    private float enemyHalfHeight;

    private float enemySpawnRate;

    private void Awake()
    {
        NavMeshAgent enemyNavAgent = enemyPrefab.GetComponent<NavMeshAgent>();
        enemyRadius = enemyNavAgent.radius;
        enemyHalfHeight = enemyNavAgent.height / 2;

        if (enableContinuousEnemySpawning )
        {
            enemySpawnRate = initialSpawnTime;
            StartCoroutine(SpawnEnemyContinuous());
        }
    }

    private IEnumerator SpawnEnemyContinuous()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemySpawnRate);
            SpawnEnemy();
            enemySpawnRate = Mathf.Max(enemySpawnRate / spawnTimeIncrease, minSpawnTime);
        }
    }

    /// <summary>
    /// Spawns an enemy at a random position within a specified radius around the player.
    /// </summary>
    public void SpawnEnemy()
    {
        if (FindEnemySpawnPosition(out Vector3 spawnPosition))
        {
            Vector3 directionToPlayer = player.transform.position - spawnPosition;
            Enemy enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.LookRotation(directionToPlayer));
            enemy.SetTargetPlayer(player);
        }
    }

    /// <summary>
    /// Finds suitable position to spawn an enemy around player.
    /// </summary>
    /// <param name="position"></param>
    /// <returns>Whether a suitable position was found</returns>
    private bool FindEnemySpawnPosition(out Vector3 position)
    {
        Vector3 spawnPoint = FindRandomPointAroundPlayer();
        int checkCount = 0;     // Exit loop if can't find suitable position

        while (!IsSpawnpointSuitable(spawnPoint) && checkCount < MAX_OVERLAP_CHECK_COUNT)
        {
            spawnPoint = FindRandomPointAroundPlayer();
            checkCount++;
        }
        
        if (checkCount == MAX_OVERLAP_CHECK_COUNT)
        {
            position = Vector3.zero;
            return false;
        }

        position = spawnPoint;
        return true;
    }

    /// <summary>
    /// Finds random point around player in Oxz plane in given <c>innerRadius</c> and <c>outerRadius</c>.
    /// </summary>
    /// <returns></returns>
    private Vector3 FindRandomPointAroundPlayer()
    {
        float distance = Random.Range(innerRadius, outerRadius);
        Vector2 randomPointOnCircle = Random.insideUnitCircle.normalized * distance;
        Vector3 randomPointOnCircleVector3 = new Vector3(randomPointOnCircle.x, 0f, randomPointOnCircle.y);
        return player.transform.position + randomPointOnCircleVector3;
    }

    /// <summary>
    /// Checks if <paramref name="point"/> is suitable for spawning enemy.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    private bool IsSpawnpointSuitable(Vector3 point)
    {
        // Ground check
        if (!Physics.Linecast(point, point + Vector3.down * DISTANCE_TO_GROUND_CHECK))
            return false;

        // Collision check
        Vector3 capsuleStart = point - Vector3.up * (enemyHalfHeight - enemyRadius);
        Vector3 capsuleEnd = point + Vector3.up * (enemyHalfHeight - enemyRadius);
        return !Physics.CheckCapsule(capsuleStart, capsuleEnd, enemyRadius);
    }
}
