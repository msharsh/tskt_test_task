using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemyPrefab;

    /// <summary>
    /// Spawns an enemy at a random position around the player
    /// </summary>
    public void SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab, FindEnemySpawnPosition(), Quaternion.identity);
        enemy.SetTargetPlayer(player);
    }

    /// <summary>
    /// Finds suitable position to spawn an enemy around player.
    /// </summary>
    private Vector3 FindEnemySpawnPosition()
    {
        Vector3 playerForward = player.transform.forward;
        return player.transform.position + playerForward * 5f;
    }
}
