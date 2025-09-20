using UnityEngine;
using System.Collections;

public class CatapultProjectile : MonoBehaviour
{
    private Rigidbody rb;

    public float launchHeight = 10f;
    public float gravity = -9.81f;
    public float explosionRadius = 0f;
    public int damage = 50;
    public GameObject impactEffect;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float horizontalDistance = new Vector3(direction.x, 0, direction.z).magnitude;
        float verticalDistance = direction.y;

        float tUp = Mathf.Sqrt(-2 * launchHeight / gravity);
        float tDown = Mathf.Sqrt(2 * Mathf.Max(0.01f, Mathf.Abs(verticalDistance - launchHeight)) / -gravity);
        float time = tUp + tDown;
        Vector3 velocity = new Vector3(direction.x / time, 0, direction.z / time);

        velocity.y = Mathf.Sqrt(-2 * gravity * launchHeight);

        rb.linearVelocity = velocity;

        transform.rotation = Quaternion.LookRotation(velocity);

        StartCoroutine(RotateBoulder());
    }

    private IEnumerator RotateBoulder()
    {
        while (true)
        {
            transform.Rotate(Vector3.right * 360f * Time.deltaTime, Space.Self);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explosionRadius > 0)
            Explode();
        else if (collision.gameObject.CompareTag("Enemy"))
            Damage(collision.transform);

        if (impactEffect != null)
        {
            var effect = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        EnemyScript e = enemy.GetComponent<EnemyScript>();
        if (e != null)
        {
            e.TakeDamage(damage);
            e.Slow(0.3f, 4f);
        }

    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
