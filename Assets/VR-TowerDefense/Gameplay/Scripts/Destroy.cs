using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroy : MonoBehaviour
{
    [Tooltip("Destroy the whole GameObject by self after given delay. If zero no self destroy happen.")]
    public float selfDestroyDelay = 0f;
    public UnityEvent OnDestroy;

    private void Start()
    {
        if (selfDestroyDelay > 0)
            Invoke("Me", selfDestroyDelay);
    }

    public void Me()
    {
        OnDestroy.Invoke();
        Destroy(gameObject);
    }
}