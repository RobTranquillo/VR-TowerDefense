using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTriggered : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (collisionEvents == null)
            return;
        Destroyabillity dest = other.GetComponent<Destroyabillity>();
        if (dest == null)
            return;
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        other.GetComponent<Destroyabillity>().Hit(numCollisionEvents);
    }
}
