using UnityEngine;
using Unity.XR.CoreUtils;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Start()
    {
        XROrigin xrOrigin = FindFirstObjectByType<XROrigin>();

        if (xrOrigin == null)
        {
            Debug.LogError("No XR Origin found in the scene.");
            return;
        }

        xrOrigin.transform.position = transform.position;
        xrOrigin.transform.rotation = transform.rotation;
    }
}