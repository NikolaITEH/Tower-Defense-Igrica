using UnityEngine;

public class BallistaScript : MonoBehaviour
{

    private Transform target;
    public float range = 15;
    public string enemyTag = "Enemy";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected() //da bi mogli da vidimo range u samom editoru
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
