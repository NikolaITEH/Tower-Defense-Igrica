using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float speed = 10;    //brzina Enemy-ja
    private Transform target;       //sledeca lokacija gde enemy ide, Transform tip sadrzi position, rotation, scale
    private int waypointIndex = 0;  //index waypoint-a

    public int health = 100;
    public int goldGain = 50;

    public GameObject deathEffect;

    private void Start()
    {
        target = WaypointScript.points[0];  //postavljanje prvog waypoint-a kao target
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStatsScript.gold += goldGain;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;   //pravac se dobija kad se od pozicije target-a oduzme trenutna pozicija enemy-ja

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);  //pomeranje enemy-ja
        //Space.World - enemy se pomera u globalnom koordinatnom sistemu scene, ne zavisi od rotacije objekta.

        if (Vector3.Distance(transform.position, target.position) <= 0.2)   //kada enemy dodje na 0.2 od target-a, dobija se sledeci waypoint
        {
            GetNextWaypoint();
        }    
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= WaypointScript.points.Length - 1)  //ako je dosao do poslednjeg waypoint-a, brise se
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target=WaypointScript.points[waypointIndex];    //target se postavlja na sledeci waypoint
    }

    void EndPath()
    {
        PlayerStatsScript.lives--;
        Destroy(gameObject);
    }

}
