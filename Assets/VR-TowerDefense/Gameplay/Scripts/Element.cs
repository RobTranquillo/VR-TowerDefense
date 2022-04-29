using UnityEngine;

public class Element : MonoBehaviour
{
    // Gives access to the application and all instances.
    public static GameApplication app
    {
        get
        {
            try
            {
                return GameObject.FindObjectOfType<GameApplication>();
            }
            catch
            {
                Debug.LogError("GameApplication not found");
                throw;
            }
        }
    }
}