using UnityEngine;
using UnityEditor;

public class MenuEntry_GameSettings : MonoBehaviour
{
    /// <summary>
    /// Create a new file holding the whole game settings
    /// </summary>
    [MenuItem("VR-Game/Create new GameSettings.asset")]
    static void CreateScriptableAsset()
    {
        string destDath = "Assets/GameSettings.asset";
        ScriptableObject settingsFile = (GameSettings) ScriptableObject.CreateInstance(typeof(GameSettings));
        AssetDatabase.CreateAsset(settingsFile, destDath);
    }
}


public class GameSettings : ScriptableObject
{
    internal static int value_cubeAdded = 5;
    [Tooltip("ReadOnly! Muss so eingestellt sein, wie der scale des GameWorld Objektes")]
    public float gameWorldScale = 0.1f;
    public Vector3 gameWorldStartPosition;
    public float distanceOffset = 0.03f;
    public float snapAxisTolerance = 0.1f;
    public float cubeStoreySelectTollerance = 0.03f;

    [Header("game play settings")]

    [Range(2,100)]
    [Tooltip("Max count of cubes in a storey, from which they are cleared away.")]
    public int maxPuzzleGoal = 23;


}
