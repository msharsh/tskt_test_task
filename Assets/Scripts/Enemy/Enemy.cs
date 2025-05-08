using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Player targetPlayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(UpdatePlayerPosition());
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
    /// Sets Player reference to which move to
    /// </summary>
    /// <param name="targetPlayer"></param>
    public void SetTargetPlayer(Player targetPlayer)
    {
        this.targetPlayer = targetPlayer;
    }

    public void KillEnemy()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
