using System;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Stellt intern viele wichtige Events bereit,
/// an die sich beliebig rangehangen werden kann und
/// die frei gecalled werden können.
/// </summary>
public class EventManager : MonoBehaviour
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }


    //----------- EVENTS -----------------------
    public event Action SpawnNewCube;
    public event Action CubeAttached;
    public event Action ValidateCubes;  
    public event Action UpdatePuzzleGoal;
    public event Action StoreyKilled;
    public event Action GameOver;
    public event Action GamePreStart;
    public event Action GameStart;

    
    //----------EVENT TRIGGER ------------------------
    public void Trigger_SpawnNewCube() { SpawnNewCube?.Invoke(); }
    public void Trigger_CubeAttached() { CubeAttached?.Invoke(); }
    public void Trigger_ValidateCubes() { ValidateCubes?.Invoke(); }
    public void Trigger_UpdatePuzzleGoal() { UpdatePuzzleGoal?.Invoke(); }
    public void Trigger_StoreyCleaned() { StoreyKilled?.Invoke(); }
    public void Trigger_GameOver() { GameOver?.Invoke(); }
    public void Trigger_GameStart() { GameStart?.Invoke(); }
    public void Trigger_GamePreStart() { GamePreStart?.Invoke(); }

}
