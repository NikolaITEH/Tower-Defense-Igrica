using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    private Transform target;

    public float speed = 40;

    public GameObject impactEffect;


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

        Vector3 direction=target.position-transform.position;
        float distanceThisFrame = speed * Time.deltaTime;   //koliko arrow treba da putuje u jednom frame-u

        if (direction.magnitude <= distanceThisFrame)   //direction.magnitute je distanca do target-a
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        transform.rotation = Quaternion.LookRotation(direction);    //da se strela okrece ka target-u svaki frame

    }

    void HitTarget()
    {
        GameObject effectInstance=Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);    //efekat se unistava nakon dve sekunde

        Destroy(gameObject); //unistava se strela

    }
}
