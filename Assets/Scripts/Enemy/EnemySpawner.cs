using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemyPrefab;
    [Header("Spawn Parameters")]
    [SerializeField] private float innerRadius = 10f;
    [SerializeField] private float outerRadius = 20f;

    private const int MAX_OVERLAP_CHECK_COUNT = 10;
    private const float DISTANCE_TO_GROUND_CHECK = 10f;

    private float enemyRadius;
    private float enemyHalfHeight;

    private void Awake()
    {
        NavMeshAgent enemyNavAgent = enemyPrefab.GetComponent<NavMeshAgent>();
        enemyRadius = enemyNavAgent.radius;
        enemyHalfHeight = enemyNavAgent.height / 2;
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
        int checkCount = 0;

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

    private Vector3 FindRandomPointAroundPlayer()
    {
        float distance = Random.Range(innerRadius, outerRadius);
        Vector2 randomPointOnCircle = Random.insideUnitCircle.normalized * distance;
        Vector3 randomPointOnCircle3D = new Vector3(randomPointOnCircle.x, player.transform.position.y, randomPointOnCircle.y);
        return player.transform.position + randomPointOnCircle3D;
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
        return !Physics.CheckCapsule(capsuleStart, capsuleEnd, 0.5f);
    }
}
