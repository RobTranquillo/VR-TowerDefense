using System;
using UnityEngine;

public class AttacNextEnemy : MonoBehaviour
{
    public GameObject canonHead;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float firingDelay = 0.5f;
    public float sphereOfAction;
    public float lookupDelay = 1f;
    public float angularSpeed = 1f;
    public AudioClip[] audioClips;

    private GameObject enemy;
    private bool attackOn = false;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        FindNextEnemy();
    }

    private void Update()
    {
        if (enemy == null)
            return;

        Vector3 targetDir = enemy.transform.position;
        targetDir.y = transform.position.y;
        targetDir = transform.position - targetDir;

        Quaternion rot = Quaternion.LookRotation(targetDir);
        canonHead.transform.rotation = Quaternion.Lerp(canonHead.transform.rotation, rot, angularSpeed * Time.deltaTime);
    }

    private void FindNextEnemy()
    {
        Invoke("FindNextEnemy", lookupDelay);
        if (enemy != null)
            return;
        Collider[] enemys = Utils.EnemysInRadius(transform.position, sphereOfAction);
        if (enemys.Length > 0)
        {
            enemy = enemys[0].gameObject;
            attackOn = true;
            TriggerShot();
        }
        else
        {
            enemy = null;
            attackOn = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphereOfAction);
    }

    /// <summary>
    /// triggers the shot of an bullet
    /// </summary>
    public void TriggerShot()
    {
        if (!attackOn)
            return;

        if (enemy == null)
            FindNextEnemy();

        PlayRandomSound();
        GameObject bulletGO = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bulletGO.GetComponent<CanonBullet>().SetTarget(Vector3.forward);  //enemy.transform.position
        Invoke("TriggerShot", firingDelay);
    }

    private void PlayRandomSound()
    {
        if (audioSource == null)
            return;

        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
}