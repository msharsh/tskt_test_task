using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;

    private void Awake()
    {
        StartCoroutine(SetLifetime(10f));
    }

    private void Update()
    {
        transform.position = transform.position + transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.ApplyDamage(projectileDamage);
        }

        DestroyProjectile();
    }

    /// <summary>
    /// Destroys projectile after <paramref name="lifetime"/> seconds.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
