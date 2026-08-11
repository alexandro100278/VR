using UnityEngine;

public class BotonRotarCubo : MonoBehaviour
{
    public RotarCubo rotador;

    public enum TipoRotacion
    {
        Arriba,
        Lado
    }

    public TipoRotacion tipoRotacion;

    private void OnMouseDown()
    {
        Presionar();
    }

    public void Presionar()
    {
        if (rotador == null)
        {
            Debug.LogError("No se asignó RotadorRubik.");
            return;
        }

        if (tipoRotacion == TipoRotacion.Arriba)
        {
            rotador.GirarArriba();
        }
        else if (tipoRotacion == TipoRotacion.Lado)
        {
            rotador.GirarLado();
        }
    }
}