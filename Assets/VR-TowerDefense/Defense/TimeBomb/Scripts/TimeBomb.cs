using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimeBomb : MonoBehaviour
{
    [Header("General")]
    public float timer = 5f;
    public float killRadius = 3f;
    public int maxDamage = 250;   
    public float triggerNewSpawnDistance = 2f;
    [Header("FX")]
    public AudioSource audioSource;
    public float audioStartDelay;
    public bool pumpingBeforeExplode = true;
    public float pumpingInterval = 1f;
    public GameObject explosionParticleSystemPrefab;


    float startTime;
    Vector3 startPos;
    bool alreadyTrigged = false;
    GameObject explosionParticleSystem;

    private void OnDisable()
    {
        startTime = Time.time;
    }

    private void OnEnable()
    {
        startPos = transform.position;
        startTime = Time.time;
        Invoke("EnableAudioSource", audioStartDelay);
        Invoke("Pumping", pumpingInterval);
        explosionParticleSystem = Instantiate(explosionParticleSystemPrefab);
    }

    void Update()
    {
        if (!alreadyTrigged)
            TriggerEvent();
        if (startTime + timer > Time.time)
            return;
        Destroy(gameObject);
    }

    private void Pumping()
    {
        if (!pumpingBeforeExplode)
            return;

        Vector3 defaultScale = transform.localScale;
        Vector3 jumpHeight = transform.localPosition;
        jumpHeight.y += 0.2f;
        transform.DOLocalJump(jumpHeight, .5f, 2, 0.3f);
        transform.DOShakeScale(0.3f, 1, 5, 90);
        Invoke("Pumping", pumpingInterval);
    }

    private void EnableAudioSource()
    {
        if (audioSource == null)
            return;
        audioSource.enabled = true;
    }

    private void TriggerEvent()
    {
        if (Vector3.Distance(startPos, transform.position) < triggerNewSpawnDistance)
            return;
        EventManager.current?.Trigger_SpawnNewCube();
        alreadyTrigged = true;
    }

    private void OnDestroy()
    {
        ParticleBurst();
        ExplosionDamage(transform.position, killRadius, maxDamage);
    }

    private void ParticleBurst()
    {
        if (explosionParticleSystem == null)
            return;
        explosionParticleSystem.transform.position = transform.position;
        explosionParticleSystem.SetActive(true);
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
