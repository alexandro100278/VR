using System.Collections;
using UnityEngine;
using TMPro;

public class RetoRubik : MonoBehaviour
{
    public RubikController rubik;

    public TMP_Text textoReto;
    public TMP_Text textoTiempo;

    private float tiempo = 0f;
    private bool contando = false;
    private bool preparando = false;

    private string[] scrambleActual;

    void Start()
    {
        CargarRetoSeleccionado();
        MostrarTiempo();
    }

    void Update()
    {
        if (contando)
        {
            tiempo += Time.deltaTime;
            MostrarTiempo();
        }
    }

    void CargarRetoSeleccionado()
    {
        int reto = DatosReto.retoSeleccionado;

        switch (reto)
        {
            case 1:
                textoReto.text =
                    "RETO 1 - ENCUENTRA EL BLANCO\n\n" +
                    "Objetivo:\n" +
                    "Encuentra la cara que tiene el centro blanco.\n\n" +
                    "Instrucciones:\n" +
                    "- Observa todas las caras del cubo.\n" +
                    "- Localiza el centro blanco.\n\n" +
                    "Dificultad: Facil";

                scrambleActual = new string[] { };
                break;

            case 2:
                textoReto.text =
                    "RETO 2 - ORIENTA EL CUBO\n\n" +
                    "Objetivo:\n" +
                    "Coloca la cara blanca frente a ti.\n\n" +
                    "Instrucciones:\n" +
                    "- Encuentra el centro blanco.\n" +
                    "- Gira el cubo hasta colocarlo de frente.\n\n" +
                    "Dificultad: Facil";

                scrambleActual = new string[] { };
                break;

            case 3:
                textoReto.text =
                    "RETO 3 - ENCUENTRA LA PIEZA\n\n" +
                    "Objetivo:\n" +
                    "Encuentra una pieza que contenga color blanco.\n\n" +
                    "Instrucciones:\n" +
                    "- Observa las esquinas y los bordes.\n" +
                    "- Identifica una pieza con color blanco.\n\n" +
                    "Dificultad: Facil";

                scrambleActual = new string[]
                {
                    "R"
                };
                break;

            case 4:
                textoReto.text =
                    "RETO 4 - COLOCA UNA PIEZA BLANCA\n\n" +
                    "Objetivo:\n" +
                    "Coloca una pieza blanca junto al centro blanco.\n\n" +
                    "Instrucciones:\n" +
                    "- Encuentra una pieza blanca.\n" +
                    "- Mueve las caras necesarias.\n" +
                    "- Coloca la pieza junto al centro blanco.\n\n" +
                    "Dificultad: Medio";

                scrambleActual = new string[]
                {
                    "R",
                    "U"
                };
                break;

            case 5:
                textoReto.text =
                    "RETO 5 - FORMA UNA LINEA\n\n" +
                    "Objetivo:\n" +
                    "Forma una linea de 3 cuadros blancos.\n\n" +
                    "Instrucciones:\n" +
                    "- Observa las piezas blancas.\n" +
                    "- Gira las caras necesarias.\n" +
                    "- Consigue 3 cuadros blancos alineados.\n\n" +
                    "Dificultad: Medio";

                scrambleActual = new string[]
                {
                    "R",
                    "U",
                    "F"
                };
                break;

            case 6:
                textoReto.text =
                    "RETO 6 - FORMA UNA L\n\n" +
                    "Objetivo:\n" +
                    "Forma una L utilizando cuadros blancos.\n\n" +
                    "Instrucciones:\n" +
                    "- Busca las piezas blancas.\n" +
                    "- Gira las caras necesarias.\n" +
                    "- Coloca 3 cuadros formando una L.\n\n" +
                    "Dificultad: Medio";

                scrambleActual = new string[]
                {
                    "R",
                    "U",
                    "F",
                    "L"
                };
                break;

            case 7:
                textoReto.text =
                    "RETO FINAL - CRUZ BLANCA\n\n" +
                    "Objetivo:\n" +
                    "Forma una cruz blanca alrededor del centro blanco.\n\n" +
                    "Instrucciones:\n" +
                    "- Localiza las piezas blancas de borde.\n" +
                    "- Gira las caras necesarias.\n" +
                    "- Forma una cruz alrededor del centro blanco.\n\n" +
                    "Dificultad: Dificil";

                scrambleActual = new string[]
                {
                    "R",
                    "U",
                    "F",
                    "L",
                    "D"
                };
                break;

            default:
                textoReto.text =
                    "NO HAY RETO SELECCIONADO\n\n" +
                    "Regresa a la seleccion de retos.";

                scrambleActual = new string[] { };
                break;
        }
    }

    public void IniciarReto()
    {
        if (!preparando)
        {
            StartCoroutine(PrepararReto());
        }
    }

    private IEnumerator PrepararReto()
    {
        preparando = true;
        contando = false;

        tiempo = 0f;
        MostrarTiempo();

        foreach (string movimiento in scrambleActual)
        {
            rubik.GirarCaraExterna(movimiento);

            yield return new WaitUntil(() => rubik.EstaGirando);
            yield return new WaitUntil(() => !rubik.EstaGirando);

            yield return new WaitForSeconds(0.1f);
        }

        tiempo = 0f;
        contando = true;
        preparando = false;

        MostrarTiempo();

        Debug.Log(
            "Reto " +
            DatosReto.retoSeleccionado +
            " iniciado."
        );
    }

    public void TerminarReto()
    {
        contando = false;

        Debug.Log(
            "Tiempo final: " +
            tiempo.ToString("F2") +
            " segundos"
        );
    }

    private void MostrarTiempo()
    {
        if (textoTiempo == null)
            return;

        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        int centesimas =
            Mathf.FloorToInt((tiempo * 100f) % 100f);

        textoTiempo.text =
            minutos.ToString("00") + ":" +
            segundos.ToString("00") + "." +
            centesimas.ToString("00");
    }
}