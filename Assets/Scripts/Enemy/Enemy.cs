using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private NavMeshAgent agent;
    private Player targetPlayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(UpdatePlayerPosition());
        StartCoroutine(LaunchProjectile());
    }

    private IEnumerator UpdatePlayerPosition()
    {
        while (true)
        {
            agent.SetDestination(targetPlayer.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Spawns a projectile every second
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaunchProjectile()
    {
        while (true)
        {
            Quaternion rotationToTarget = Quaternion.LookRotation(targetPlayer.transform.position - transform.position);
            Instantiate(projectilePrefab, projectileSpawnPoint.position, rotationToTarget);
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Sets Player reference to which move to
    /// </summary>
    /// <param name="targetPlayer"></param>
    public void SetTargetPlayer(Player targetPlayer)
    {
        this.targetPlayer = targetPlayer;
    }

    /// <summary>
    /// Kills an enemy
    /// </summary>
    public void KillEnemy()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
