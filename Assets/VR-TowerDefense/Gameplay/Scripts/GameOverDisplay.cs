using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    public TMP_Text textElement;

    private void Start()
    {
        textElement.text = "";

        EventManager.current.GameOver += GameOver;
    }

    private void OnDestroy()
    {
        EventManager.current.GameOver -= GameOver;
    }

    private void GameOver()
    {
        textElement.text = "Game Over";
    }
}
