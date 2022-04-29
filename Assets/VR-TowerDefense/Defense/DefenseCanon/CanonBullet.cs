using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : MonoBehaviour
{
    [Range(1, 100)]
    public float speed = 5f;

    public float lifetime = 2f;

    [Range(1, 200)]
    public int strength = 10;

    private float start;
    private float lastPos;

    private Vector3 targetPos;
    private Vector3 startPos;

    private void Awake()
    {
        speed = speed / 100;
        start = Time.time;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (targetPos == null)
            return;

        if (start + lifetime < Time.time)
        {
            Destroy(gameObject);
            return;
        }

        //targetPos = Vector3.up;
        //targetPos.y = startPos.y;
        //Vector3 moveDir = targetPos - startPos;
        transform.position += transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Enemy")
            return;
        other.gameObject?.GetComponent<Destroyabillity>()?.Hit(strength);
        Destroy(gameObject);
    }

    public void SetTarget(Vector3 pos)
    {
        targetPos = pos;
    }
}