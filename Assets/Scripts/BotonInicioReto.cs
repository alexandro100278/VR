using UnityEngine;

public class BotonInicioReto : MonoBehaviour
{
    public RetoRubik reto;

    private void OnMouseDown()
    {
        Iniciar();
    }

    public void Iniciar()
    {
        if (reto == null)
        {
            Debug.LogError("No se asignó el RetoManager.");
            return;
        }

        reto.IniciarReto();
    }
}