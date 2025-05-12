using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [Tooltip("Delay in seconds between shots")]
    [SerializeField] private float fireDelay = 1f;
    [SerializeField] private VisualEffect deathEffect;

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
    /// Fires a projectile with constant rate.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LaunchProjectile()
    {
        while (true)
        {
            Quaternion rotationToTarget = Quaternion.LookRotation(targetPlayer.transform.position - transform.position);
            Instantiate(projectilePrefab, projectileSpawnPoint.position, rotationToTarget);
            yield return new WaitForSeconds(fireDelay);
        }
    }

    /// <summary>
    /// Sets Player reference to which move to.
    /// </summary>
    /// <param name="targetPlayer"></param>
    public void SetTargetPlayer(Player targetPlayer)
    {
        this.targetPlayer = targetPlayer;
    }

    /// <summary>
    /// Kills an enemy.
    /// </summary>
    public void KillEnemy()
    {
        // Spawn death VFX
        VisualEffect vfxInstance = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(vfxInstance.gameObject, 1f);

        // Destroy this gameobject
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
