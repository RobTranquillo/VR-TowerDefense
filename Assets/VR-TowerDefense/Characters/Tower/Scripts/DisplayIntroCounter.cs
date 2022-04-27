using TMPro;
using UnityEngine;

public class DisplayIntroCounter : MonoBehaviour
{
    public TMP_Text textElement;
    private int counter = 0;

    private void Start()
    {
        textElement.text = "";
        EventManager.current.GamePreStart += GamePreStart;
        EventManager.current.GameStart += GameStart;

    }

    private void OnDestroy()
    {
        EventManager.current.GamePreStart -= GamePreStart;
        EventManager.current.GameStart -= GameStart;
    }

    private void GamePreStart()
    {
        if (counter == 0)
            textElement.text = "3";
        if (counter == 1)
            textElement.text = "2";
        if (counter == 2)
            textElement.text = "1";
        if (counter == 3)
            EventManager.current.Trigger_GameStart();
        if (counter == 4) {
            textElement.text = "";
            return;
        }
        counter++;
        Invoke("GamePreStart", 1f);
    }

    private void GameStart()
    {
        textElement.text = "Game GameStart";
    }
}
