using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileLifetime = 10f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * projectileSpeed;

        // Set lifetime
        Destroy(gameObject, projectileLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Apply damage to player
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            player.ApplyDamage(projectileDamage);
        }

        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
