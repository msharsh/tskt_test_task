using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemyPrefab;

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab.gameObject, FindEnemySpawnPosition(), Quaternion.identity);
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
