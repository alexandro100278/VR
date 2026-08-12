using UnityEngine;

public class PersistentVRSystems : MonoBehaviour
{
    private static PersistentVRSystems instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}