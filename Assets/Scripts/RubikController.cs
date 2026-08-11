using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubikController : MonoBehaviour
{
    public float velocidadGiro = 300f;

    private bool girando = false;

    public bool EstaGirando
    {
        get { return girando; }
    }

    public void GirarCaraExterna(string cara)
    {
        if (!girando)
        {
            StartCoroutine(GirarCara(cara));
        }
    }

    void Update()
    {
        if (girando)
            return;

        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(GirarCara("L"));

        if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine(GirarCara("R"));

        if (Input.GetKeyDown(KeyCode.U))
            StartCoroutine(GirarCara("U"));

        if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(GirarCara("D"));

        if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(GirarCara("F"));

        if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(GirarCara("B"));
    }

    public void GirarDerecha()
    {
        if (!girando)
            StartCoroutine(GirarCara("L"));
    }

    public void GirarIzquierda()
    {
        if (!girando)
            StartCoroutine(GirarCara("R"));
    }

    public void GirarArriba()
    {
        if (!girando)
            StartCoroutine(GirarCara("U"));
    }

    public void GirarAbajo()
    {
        if (!girando)
            StartCoroutine(GirarCara("D"));
    }

    public void GirarFrente()
    {
        if (!girando)
            StartCoroutine(GirarCara("F"));
    }

    public void GirarAtras()
    {
        if (!girando)
            StartCoroutine(GirarCara("B"));
    }

    IEnumerator GirarCara(string cara)
    {
        girando = true;

        List<Transform> piezas = new List<Transform>();

        float limite = 0f;
        Vector3 eje = Vector3.zero;
        float angulo = 90f;

        // Encontrar límite de cada cara
        switch (cara)
        {
            case "R":
                limite = ObtenerMaximoX();
                eje = transform.right;
                angulo = -90f;
                break;

            case "L":
                limite = ObtenerMinimoX();
                eje = transform.right;
                angulo = 90f;
                break;

            case "U":
                limite = ObtenerMaximoY();
                eje = transform.up;
                angulo = 90f;
                break;

            case "D":
                limite = ObtenerMinimoY();
                eje = transform.up;
                angulo = -90f;
                break;

            case "F":
                limite = ObtenerMaximoZ();
                eje = transform.forward;
                angulo = -90f;
                break;

            case "B":
                limite = ObtenerMinimoZ();
                eje = transform.forward;
                angulo = 90f;
                break;
        }

        // Encontrar las 9 piezas
        foreach (Transform pieza in transform)
        {
            Vector3 pos = pieza.localPosition;

            bool pertenece = false;

            switch (cara)
            {
                case "R":
                case "L":
                    pertenece = Mathf.Abs(pos.x - limite) < 0.1f;
                    break;

                case "U":
                case "D":
                    pertenece = Mathf.Abs(pos.y - limite) < 0.1f;
                    break;

                case "F":
                case "B":
                    pertenece = Mathf.Abs(pos.z - limite) < 0.1f;
                    break;
            }

            if (pertenece)
                piezas.Add(pieza);
        }

        Debug.Log(cara + " - Piezas encontradas: " + piezas.Count);

        // Centro real del Rubik
        Vector3 centroRubik = Vector3.zero;
        int cantidad = 0;

        foreach (Transform pieza in transform)
        {
            centroRubik += pieza.position;
            cantidad++;
        }

        centroRubik /= cantidad;

        // Crear pivote temporal
        GameObject objetoPivote = new GameObject("Pivot_" + cara);

        Transform pivote = objetoPivote.transform;

        pivote.position = centroRubik;
        pivote.rotation = transform.rotation;

        // Poner piezas como hijas del pivote
        foreach (Transform pieza in piezas)
        {
            pieza.SetParent(pivote, true);
        }

        float anguloActual = 0f;
        float direccion = Mathf.Sign(angulo);
        float objetivo = Mathf.Abs(angulo);

        while (anguloActual < objetivo)
        {
            float giro = velocidadGiro * Time.deltaTime;

            if (anguloActual + giro > objetivo)
            {
                giro = objetivo - anguloActual;
            }

            pivote.Rotate(
                eje,
                giro * direccion,
                Space.World
            );

            anguloActual += giro;

            yield return null;
        }

        // Regresar piezas al Rubik
        foreach (Transform pieza in piezas)
        {
            pieza.SetParent(transform, true);
        }

        Destroy(objetoPivote);

        girando = false;
    }

    float ObtenerMaximoX()
    {
        float valor = float.MinValue;

        foreach (Transform pieza in transform)
            valor = Mathf.Max(valor, pieza.localPosition.x);

        return valor;
    }

    float ObtenerMinimoX()
    {
        float valor = float.MaxValue;

        foreach (Transform pieza in transform)
            valor = Mathf.Min(valor, pieza.localPosition.x);

        return valor;
    }

    float ObtenerMaximoY()
    {
        float valor = float.MinValue;

        foreach (Transform pieza in transform)
            valor = Mathf.Max(valor, pieza.localPosition.y);

        return valor;
    }

    float ObtenerMinimoY()
    {
        float valor = float.MaxValue;

        foreach (Transform pieza in transform)
            valor = Mathf.Min(valor, pieza.localPosition.y);

        return valor;
    }

    float ObtenerMaximoZ()
    {
        float valor = float.MinValue;

        foreach (Transform pieza in transform)
            valor = Mathf.Max(valor, pieza.localPosition.z);

        return valor;
    }

    float ObtenerMinimoZ()
    {
        float valor = float.MaxValue;

        foreach (Transform pieza in transform)
            valor = Mathf.Min(valor, pieza.localPosition.z);

        return valor;
    }
}