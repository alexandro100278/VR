using UnityEngine;

public class RubikController : MonoBehaviour
{
    void Start()
    {
        foreach (Transform pieza in transform)
        {
            if (pieza.name == "Camera")
                continue;

            Debug.Log(
                pieza.name +
                " LOCAL = " + pieza.localPosition +
                " WORLD = " + pieza.position
            );
        }
    }
}