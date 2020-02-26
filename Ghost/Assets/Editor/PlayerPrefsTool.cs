using UnityEditor;
using UnityEngine;

public class PlayerPrefsTool : MonoBehaviour
{
    [MenuItem("Tools/Delete PlayerPrefs")]

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}