using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameApplication : MonoBehaviour
{
    public GameSettings gameSettings;
    
    AttackSlot[] attackSlots;    
    GameObject[] towers;

    private void Start()
    {
        EventManager.current.CubeAttached += OnCubeAttached;        
        EventManager.current.Trigger_GamePreStart();        
    }

    public AttackSlot[] GetAttackSlots()
    {
        if (attackSlots == null)
            attackSlots = FindObjectsOfType<AttackSlot>();
        return attackSlots;
    }

    public GameObject[] GetTowers()
    {
        if (towers != null)
            if (towers.Length > 0)
                return towers;
        return GameObject.FindGameObjectsWithTag("Tower");
    }

    private void OnCubeAttached()
    {
        EventManager.current.Trigger_ValidateCubes();
    }
}