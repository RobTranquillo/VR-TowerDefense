using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    public float timer = 5f;
    public float killRadius = 3f;
    public int maxDamage = 250;   

    public float triggerNewSpawnDistance = 2f;   
        
    float startTime;
    Vector3 startPos;
    bool alreadyTrigged = false;

    private void OnDisable()
    {
        startTime = Time.time;
    }

    private void OnEnable()
    {
        startPos = transform.position;
        startTime = Time.time;
    }

    void Update()
    {
        if (!alreadyTrigged)
            TriggerEvent();
        if (startTime + timer > Time.time)
            return;
        Destroy(gameObject);
    }


    private void TriggerEvent()
    {
        var foo = Vector3.Distance(startPos, transform.position);
        if (foo < triggerNewSpawnDistance)
            return;
        EventManager.current.Trigger_SpawnNewCube();
        alreadyTrigged = true;
    }

    private void OnDestroy()
    {
        ExplosionDamage(transform.position, killRadius, maxDamage);
    }

    void ExplosionDamage(Vector3 center, float radius, int dmg)
    {        
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag != "Enemy")
                continue;
            hitCollider.gameObject.GetComponent<Destroyabillity>()?.Hit(dmg);
        }
    }
}
