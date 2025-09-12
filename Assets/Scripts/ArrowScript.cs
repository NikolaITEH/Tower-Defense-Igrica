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
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

        transform.rotation = Quaternion.LookRotation(direction);

    }

    void HitTarget()
    {
        GameObject effectInstance=Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        Destroy(gameObject);

    }
}
