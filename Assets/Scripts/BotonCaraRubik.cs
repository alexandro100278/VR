using UnityEngine;

public class BotonCaraRubik : MonoBehaviour
{
    public RubikController rubik;

    public enum Cara
    {
        Derecha,
        Izquierda,
        Arriba,
        Abajo,
        Frente,
        Atras
    }

    public Cara cara;

    private void OnMouseDown()
    {
        Presionar();
    }

    public void Presionar()
    {
        if (rubik == null)
        {
            Debug.LogError("No se asignó el Rubik al botón.");
            return;
        }

        switch (cara)
        {
            case Cara.Derecha:
                rubik.GirarDerecha();
                break;

            case Cara.Izquierda:
                rubik.GirarIzquierda();
                break;

            case Cara.Arriba:
                rubik.GirarArriba();
                break;

            case Cara.Abajo:
                rubik.GirarAbajo();
                break;

            case Cara.Frente:
                rubik.GirarFrente();
                break;

            case Cara.Atras:
                rubik.GirarAtras();
                break;
        }
    }
}