using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance == this)

        if (Instance == null)
        {
            Instance = this as T;
        }
    }
}