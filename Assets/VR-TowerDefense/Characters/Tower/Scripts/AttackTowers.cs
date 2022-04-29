using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AttackTowers : Element
{
    [Header("Debug")]
    public GameObject targetIndicatorPrefab;

    private GameObject targetIndicator;

    [Header("Movement")]
    public float setNewTargetDelay = 1f;

    public float stoppingDistance = 2f;

    [Header("Ballistic Weapon")]
    public GameObject bullet;

    public float firingDelay = 0.5f;
    public Transform bulletSpawnPoint;

    private NavMeshAgent agent;
    private AttackSlot[] targets;
    private AttackSlot targetAttackSlot;
    private Vector3 targetPos;
    private bool attackOn = false;
    private float nextTargetUpdate;

    private void Awake()
    {
        LoadNextTarget();
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        RotateToTarget();
        if (nextTargetUpdate + setNewTargetDelay > Time.time)
            return;
        SetNextTargetUpdate();
        agent.SetDestination(AttackPosition());
    }

    private void RotateToTarget()
    {
        Vector3 dir = agent.destination - transform.position;
        dir.y = 0;
        if (dir == Vector3.zero)
            return;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, agent.angularSpeed * Time.deltaTime);
    }

    private void LoadNextTarget()
    {
        CleanTarget();
        if (targets == null)
            targets = app.GetAttackSlots();
        try
        {
            AttackSlot[] freeSlots = targets.Where(t => t.free == true).ToArray();
            targetAttackSlot = freeSlots[Random.Range(0, freeSlots.Length)];
            targetPos = targetAttackSlot.tower.position;
        }
        catch
        {
            Debug.LogError("Enemy has no target");
            throw;
        }
        targetAttackSlot.free = false;
        targetIndicator = Instantiate(targetIndicatorPrefab, targetAttackSlot.transform.position, Quaternion.identity);
    }

    private void CleanTarget()
    {
        if (targetAttackSlot != null)
            targetAttackSlot.free = true;
        if (targetIndicator != null)
            Destroy(targetIndicator);
    }

    private void SetNextTargetUpdate()
    {
        nextTargetUpdate = Random.Range(setNewTargetDelay / 2, setNewTargetDelay) + Time.time;
    }

    private Vector3 AttackPosition()
    {
        try
        {
            return targetAttackSlot.transform.position;
        }
        catch
        {
            Debug.LogError("target is missing");
            throw;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Tower" || attackOn)
            return;
        attackOn = true;
        TriggerShot();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Tower")
            return;
        attackOn = false;
    }

    /// <summary>
    /// triggers the shot of an bullet
    /// </summary>
    public void TriggerShot()
    {
        if (!attackOn)
            return;
        GameObject bulletGO = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletGO.GetComponent<Bullet>().SetTarget(targetPos);
        Invoke("TriggerShot", firingDelay);
    }
}