using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : Element
{
    public GameObject prefab;
    private bool spawnerIsFree = true;
    private Collider coll;
    private int _interactable;

    public Transform spawnPoint;

    void Start()
    {
        _interactable = LayerMask.NameToLayer("Interactable");
        coll = GetComponent<Collider>();
        SpawnNewCube();
        CoupleEvents();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != _interactable)
            return;
        spawnerIsFree = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != _interactable)
            return;
        spawnerIsFree = true;
    }

    private void CoupleEvents()
    {
        EventManager.current.SpawnNewCube += SpawnNewCube;
    }

    private void SpawnNewCube()
    {
        if (!spawnerIsFree)
            return;

        Instantiate(prefab, spawnPoint);
        spawnerIsFree = false;
    }
}
