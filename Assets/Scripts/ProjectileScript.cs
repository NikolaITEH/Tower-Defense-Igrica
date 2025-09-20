using Unity.VisualScripting;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    private Transform target;

    public float speed = 40;

    public GameObject impactEffect;

    public float explosionRadius = 0;

    public int damage = 50;

    public void Seek(Transform _target)
    {
        target= _target;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;   //koliko arrow treba da putuje u jednom frame-u

        if (direction.magnitude <= distanceThisFrame)   //direction.magnitute je distanca do target-a
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        //transform.rotation = Quaternion.LookRotation(direction);    //da se strela okrece ka target-u svaki frame
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectInstance=Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);    //efekat se unistava nakon dve sekunde

        if (explosionRadius > 0)
        {
            Explode();
        }
       else
        {
            Damage(target);
        }
     
        Destroy(gameObject); //unistava se strela
          
    }

    void Damage(Transform enemy)
    {
        EnemyScript e=enemy.GetComponent<EnemyScript>();
        if (e != null) 
        {
            e.TakeDamage(damage);
        }
        
    }

    void Explode()
    {
        Collider[] colliders=Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
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
