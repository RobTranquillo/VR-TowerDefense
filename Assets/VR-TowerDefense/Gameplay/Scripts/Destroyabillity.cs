using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyabillity : Element
{    
    public int strength = 200;

    private bool triggerGameOverOnDestroy = false;
    
    public void TriggerGameOverOnDestroy(bool flag)
    {
        triggerGameOverOnDestroy = flag;
    }


    public void Hit(int particles)
    {        
        strength -= particles;
        if (strength > 0)
            return;
        if (triggerGameOverOnDestroy)
            EventManager.current.Trigger_GameOver();
        GetComponent<Destroy>().Me();
    }
}
