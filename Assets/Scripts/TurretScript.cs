using System.Collections;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    private Transform target;

    [Header("Attributes")]

    public float range = 15;    //max distanca balliste
    public float fireRate = 1;  //koliko strela izbacuje po sekundi
    private float fireCountdown = 0; // tajmer za izbacivanje strela

    [Header("Fields")]
    public string enemyTag = "Enemy"; 
    public GameObject projectilePrefab; 
    public Transform firePoint;  //lokacija gde se izbacuje strela

    public bool isCannon=false;
    public GameObject cannonFireEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); //pozivanje funkcije UpdateTarget, prvi put posle 0 sekundi(kad se pokrene program), a kasnije na pola sekunde
    }

    void UpdateTarget()
    {

        if (target != null) //omogucava da se ne menja target sve dok on ne izadje iz range-a
        {
            float distanceToCurrent = Vector3.Distance(transform.position, target.position);    //uzima distancu do trenutnog targeta, i ako je u okviru range-a, ostavlja taj target, ako nije, resetuje target

            if (distanceToCurrent <= range)
            {
                return; //ostavlja trenutni target
            }
            else
            {
                target = null; // trazi novi target
            }
        }


        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //pronalazi enemy-je i popunjava niz

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) 
        {
            //za svakog enemy-ja trazimo distancu do njega
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //ako je ta distanca manja od prethodno najmanje, postavljamo da nam je najmanja distanca ustv ta, i da nam je najblizi enemy ustv taj enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction=target.position-transform.position;

        direction.y = 0f;   //kako bi se ballista okretala samo oko y ose

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
        }

        fireCountdown -= Time.deltaTime; //svake sekunde fireCountdown se smanjuje za 1
    }

    void Shoot()
    {
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        ProjectileScript projectile = projectileGO.GetComponent<ProjectileScript>();
        if (projectile != null)
        {
            projectile.Seek(target);
        }

        if (isCannon)
        {
            Vector3 cannonEffectOffset = firePoint.forward * 0.3f;
            GameObject effect = Instantiate(cannonFireEffect, firePoint.position + cannonEffectOffset, firePoint.rotation);
            Destroy(effect, 2f);
        }

    }

    private void OnDrawGizmosSelected() //da bi mogli da vidimo range u samom editoru
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
