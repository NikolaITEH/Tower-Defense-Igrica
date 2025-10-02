using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    public float speed = 10;    //brzina Enemy-ja
    private Transform target;       //sledeca lokacija gde enemy ide, Transform tip sadrzi position, rotation, scale
    private int waypointIndex = 0;  //index waypoint-a

    public float startHealth = 100;
    private float health;
    public int goldGain = 50;

    public GameObject deathEffect;

    private float originalSpeed;
    private bool isSlowed = false;

    [Header("Unity")]
    public Image healthBar;


    private bool isDead = false;
    public bool IsDead => isDead;
    private Animator animator;
    public Transform model;

    private void Start()
    {
        target = WaypointScript.points[0];  //postavljanje prvog waypoint-a kao target
        health = startHealth;

        
        animator = GetComponentInChildren<Animator>();
        animator.Play("walk");
        
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        PlayerStatsScript.gold += goldGain;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        WaveSpawnerScript.enemiesAlive--;
        WaveSpawnerScript.totalEnemies--;


        animator.SetTrigger("die");
        speed = 0f;
        Destroy(gameObject, 2f);

    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;   //pravac se dobija kad se od pozicije target-a oduzme trenutna pozicija enemy-ja

        if (direction != Vector3.zero && model != null)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            model.rotation = Quaternion.Slerp(model.rotation, lookRotation, Time.deltaTime * 10f);
        }

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
        WaveSpawnerScript.enemiesAlive--;
        WaveSpawnerScript.totalEnemies--;
        Destroy(gameObject);
    }

    public void Slow(float slowAmount, float duration)
    {
        if (!isSlowed)
        {
            originalSpeed = speed;
            speed *= (1f - slowAmount);
            isSlowed = true;
            Invoke(nameof(RemoveSlow), duration);
        }
    }

    void RemoveSlow()
    {
        speed = originalSpeed;
        isSlowed= false;
    }

}
