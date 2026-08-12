using UnityEngine;

public class BotonFinalizarReto : MonoBehaviour
{
    public RetoRubik reto;

    private void OnMouseDown()
    {
        if (reto == null)
        {
            Debug.LogError("No se asignó RetoManager.");
            return;
        }

        reto.TerminarReto();
    }

    public void Seleccionar()
    {
        if (reto == null)
        {
            Debug.LogError("No se asignó RetoManager.");
            return;
        }

        reto.TerminarReto();
    }
}