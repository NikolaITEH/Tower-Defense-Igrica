using System.Collections;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [Header("Attributes")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Setup Fields")]
    public Transform firePoint;
    public GameObject boulderPrefab;
    public GameObject rangeIndicator;

    [Header("Catapult Arm Settings")]
    public Transform catapultArm;
    public float armSwingAngle = 80f;
    public float armSwingSpeed = 1.5f;
    private Quaternion armRestRotation;

    [Header("Targeting")]
    public Vector3 targetPoint;
    private bool isHolding = false;
    private bool hasTarget = false;
    public float maxTargetRange = 30f;

    private void Start()
    {
        if (catapultArm != null)
            armRestRotation = catapultArm.localRotation;

        if (rangeIndicator != null)
            rangeIndicator.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                    isHolding = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isHolding)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 desiredPoint = hit.point;
                Vector3 offset = desiredPoint - transform.position;

                if (offset.magnitude > maxTargetRange)
                {
                    offset = offset.normalized * maxTargetRange;
                }

                targetPoint = transform.position + offset;
                hasTarget = true;
            }
            isHolding = false;
        }

        if (hasTarget && fireCountdown <= 0f)
        {
            StartCoroutine(ShootWithDelay());
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;


        if (catapultArm != null && hasTarget)
        {
            Vector3 dir = targetPoint - transform.position;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.01f)
            {
                Quaternion lookRot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * 5f);
            }
        }

        if (rangeIndicator != null)
        {
            rangeIndicator.transform.position = transform.position;
            rangeIndicator.SetActive(isHolding);
        }

    }

    void Shoot()
    {
        if (catapultArm != null)
            StartCoroutine(SwingArm());

        GameObject boulderGO = Instantiate(boulderPrefab, firePoint.position, firePoint.rotation);
        CatapultProjectile proj = boulderGO.GetComponent<CatapultProjectile>();
        if (proj != null)
            proj.Launch(targetPoint);
    }

    IEnumerator SwingArm()
    {
        float t = 0f;
        bool boulderSpawned = false;

        Quaternion downRot = armRestRotation * Quaternion.Euler(-35f, 0, 0);
        while (t < 1f)
        {
            t += Time.deltaTime * armSwingSpeed;
            catapultArm.localRotation = Quaternion.Slerp(armRestRotation, downRot, t);
            yield return null;
        }

        t = 0f;
        Quaternion launchRot = armRestRotation * Quaternion.Euler(90f, 0, 0);
        while (t < 1f)
        {
            t += Time.deltaTime * armSwingSpeed * 2f;
            catapultArm.localRotation = Quaternion.Slerp(downRot, launchRot, t);

            if (!boulderSpawned && t >= 0.5f)
            {
                GameObject boulderGO = Instantiate(boulderPrefab, firePoint.position, firePoint.rotation);
                CatapultProjectile proj = boulderGO.GetComponent<CatapultProjectile>();
                if (proj != null)
                    proj.Launch(targetPoint);

                boulderSpawned = true;
            }

            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * armSwingSpeed;
            catapultArm.localRotation = Quaternion.Slerp(launchRot, armRestRotation, t);
            yield return null;
        }

        catapultArm.localRotation = armRestRotation;
    }

    IEnumerator ShootWithDelay()
    {
        if (catapultArm != null)
            yield return SwingArm();
    }
}
