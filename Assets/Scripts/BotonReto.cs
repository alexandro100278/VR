using UnityEngine;

public class BotonReto : MonoBehaviour
{
    public SelectorRetos selector;
    public int numeroReto;

    private void OnMouseDown()
    {
        selector.SeleccionarReto(numeroReto);
    }

    public void Seleccionar()
    {
        selector.SeleccionarReto(numeroReto);
    }
}