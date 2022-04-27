using UnityEngine;

public class Element : MonoBehaviour
{
    // Gives access to the application and all instances.
    public static GameApplication app { get { return GameObject.FindObjectOfType<GameApplication>(); } }
}
