using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destroy : MonoBehaviour
{
    public UnityEvent OnDestroy;

    public void Me()
    {
        Destroy(gameObject);
        OnDestroy.Invoke();
    }
}