using System.Collections;
using UnityEngine;

public class RotarCubo : MonoBehaviour
{
    public float velocidadGiro = 300f;

    private bool girando = false;

    public void GirarArriba()
    {
        if (!girando)
        {
            StartCoroutine(GirarCubo(Vector3.right, 90f));
        }
    }

    public void GirarLado()
    {
        if (!girando)
        {
            StartCoroutine(GirarCubo(Vector3.up, 90f));
        }
    }

    private IEnumerator GirarCubo(Vector3 eje, float angulo)
    {
        girando = true;

        Vector3 centro = ObtenerCentroRubik();

        float girado = 0f;
        float objetivo = Mathf.Abs(angulo);
        float direccion = Mathf.Sign(angulo);

        while (girado < objetivo)
        {
            float paso = velocidadGiro * Time.deltaTime;

            if (girado + paso > objetivo)
            {
                paso = objetivo - girado;
            }

            transform.RotateAround(
                centro,
                eje,
                paso * direccion
            );

            girado += paso;

            yield return null;
        }

        girando = false;
    }

    private Vector3 ObtenerCentroRubik()
    {
        Vector3 centro = Vector3.zero;
        int cantidad = 0;

        foreach (Transform pieza in transform)
        {
            if (pieza.name.StartsWith("Cube"))
            {
                centro += pieza.position;
                cantidad++;
            }
        }

        if (cantidad == 0)
        {
            Debug.LogError("No se encontraron las piezas del Rubik.");
            return transform.position;
        }

        centro /= cantidad;

        Debug.Log("Centro real del Rubik: " + centro);

        return centro;
    }
}