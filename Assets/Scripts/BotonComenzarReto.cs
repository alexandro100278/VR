using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonComenzarReto : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (DatosReto.retoSeleccionado == 0)
        {
            Debug.LogWarning("Primero selecciona un reto.");
            return;
        }

        SceneManager.LoadScene("JuegoRubik");
    }

    public void Seleccionar()
    {
        if (DatosReto.retoSeleccionado == 0)
        {
            Debug.LogWarning("Primero selecciona un reto.");
            return;
        }

        SceneManager.LoadScene("JuegoRubik");
    }
}