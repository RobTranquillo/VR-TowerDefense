using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnStart : MonoBehaviour
{
    public bool onPreStart;
    public bool onStart;


    void Start()
    {
        EventManager.current.GamePreStart += GamePreStart;
        EventManager.current.GameStart += GameStart;
        gameObject.SetActive(false);
    }
    
    private void GamePreStart()
    {     
        gameObject.SetActive(onPreStart);
    }

    private void GameStart()
    {
        gameObject.SetActive(onStart);
    }        
}
