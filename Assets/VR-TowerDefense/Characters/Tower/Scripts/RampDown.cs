using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RampDown : MonoBehaviour
{
    public float rampDownDuration;
    public bool physicallyWrongLowering = true;
    private Destroy[] stakes;
    private float lowestStake;

    void Start()
    {
        FindStakes();
        if (stakes.Length < 1)
            return;
        for (int i = 0; i < stakes.Length; i++)
        {
            if (physicallyWrongLowering)
                stakes[i].OnDestroy.AddListener(LowerLinear);
            else
                stakes[i].OnDestroy.AddListener(LooseStake);
        }
        lowestStake = LowestStake();
    }

    private void FindStakes()
    {
       stakes = GetComponentsInChildren<Destroy>();
    }

    private void OnDestroy()
    {
        if (stakes.Length < 1)
            return;
        for (int i = 0; i < stakes.Length; i++)
        { 
            if (physicallyWrongLowering)
                stakes[i].OnDestroy.RemoveListener(LowerLinear);
            else
                stakes[i].OnDestroy.RemoveListener(LooseStake);
        }
    }

    /// <summary>
    /// Lower the Element independed of physics just by a count value.
    /// </summary>
    private void LowerLinear()
    {
        float negativeRaise = transform.position.y - 1f;
        transform.DOMoveY(negativeRaise, rampDownDuration);
    }

    /// <summary>
    /// Lets falling down to its lowest child element
    /// </summary>
    private void LooseStake()
    {
        FindStakes();
        float theNewLow = LowestStake();
        if (theNewLow > lowestStake)
        {
            lowestStake = theNewLow;
            transform.DOMoveY(theNewLow, rampDownDuration);
        }
    }


    /// <summary>
    /// get  the lowest point where a stake is
    /// </summary>
    /// <returns></returns>
    private float LowestStake()
    {
        return stakes.Min(el => el.transform.position.y);
    }

}
