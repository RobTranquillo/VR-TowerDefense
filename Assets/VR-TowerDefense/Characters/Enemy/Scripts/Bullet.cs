using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 0.1f;
    public float lifetime = 2f;

    private float start;
    private float lastPos;

    private Vector3 targetPos;
    private Vector3 startPos;

    private void Awake()
    {
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

        targetPos.y = startPos.y;
        Vector3 moveDir = targetPos - startPos;
        transform.position += moveDir * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision?.body?.gameObject?.GetComponent<Destroyabillity>()?.Hit(1);
        Destroy(gameObject);
    }

    public void SetTarget(Vector3 pos)
    {
        targetPos = pos;
    }
}