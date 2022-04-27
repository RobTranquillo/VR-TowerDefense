
using UnityEngine;
using TMPro;

public class PointCounter : MonoBehaviour
{
    private int count = 0;
    public TMP_Text text;

    void Start()
    {
        EventManager.current.CubeAttached += Add;              
    }

    private void Add()
    {
        count = count + GameSettings.value_cubeAdded;
        text.text = count.ToString();
    }
}
