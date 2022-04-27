using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event trigger class, to trigger all events from Event Mananger class
/// </summary>
public class EventTrigger : MonoBehaviour
{
    public void SpawnNewCube()
    {
        EventManager.current.Trigger_SpawnNewCube();
    }
}
