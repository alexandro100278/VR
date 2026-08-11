using UnityEngine;
using TMPro;

public class SelectorRetos : MonoBehaviour
{
    public TMP_Text textoInfoReto;

    public void SeleccionarReto(int numeroReto)
    {
        DatosReto.retoSeleccionado = numeroReto;

        switch (numeroReto)
        {
            case 1:
                textoInfoReto.text =
                    "RETO 1 - ENCUENTRA EL BLANCO\n\n" +
                    "Objetivo:\n" +
                    "Encuentra la cara con centro blanco.\n\n" +
                    "Instrucciones:\n" +
                    "- Observa todas las caras.\n" +
                    "- Localiza el centro blanco.\n\n" +
                    "Dificultad: Facil";
                break;

            case 2:
                textoInfoReto.text =
                    "RETO 2 - ORIENTA EL CUBO\n\n" +
                    "Objetivo:\n" +
                    "Coloca la cara blanca frente a ti.\n\n" +
                    "Instrucciones:\n" +
                    "- Busca el centro blanco.\n" +
                    "- Gira el cubo completo hasta verlo de frente.\n\n" +
                    "Dificultad: Facil";
                break;

            case 3:
                textoInfoReto.text =
                    "RETO 3 - ENCUENTRA LA PIEZA\n\n" +
                    "Objetivo:\n" +
                    "Encuentra una pieza que contenga color blanco.\n\n" +
                    "Instrucciones:\n" +
                    "- Observa las esquinas y bordes.\n" +
                    "- Identifica una pieza blanca.\n\n" +
                    "Dificultad: Facil";
                break;

            case 4:
                textoInfoReto.text =
                    "RETO 4 - COLOCA UNA PIEZA BLANCA\n\n" +
                    "Objetivo:\n" +
                    "Coloca una pieza blanca junto al centro blanco.\n\n" +
                    "Instrucciones:\n" +
                    "- Localiza una pieza blanca.\n" +
                    "- Gira las caras hasta acercarla al centro.\n\n" +
                    "Dificultad: Medio";
                break;

            case 5:
                textoInfoReto.text =
                    "RETO 5 - FORMA UNA LINEA\n\n" +
                    "Objetivo:\n" +
                    "Forma una linea de 3 cuadros blancos.\n\n" +
                    "Instrucciones:\n" +
                    "- Observa las piezas blancas.\n" +
                    "- Gira las caras necesarias.\n" +
                    "- Consigue 3 blancos alineados.\n\n" +
                    "Dificultad: Medio";
                break;

            case 6:
                textoInfoReto.text =
                    "RETO 6 - FORMA UNA L\n\n" +
                    "Objetivo:\n" +
                    "Forma una L con cuadros blancos.\n\n" +
                    "Instrucciones:\n" +
                    "- Busca las piezas blancas.\n" +
                    "- Colocalas formando una esquina.\n\n" +
                    "Dificultad: Medio";
                break;

            case 7:
                textoInfoReto.text =
                    "RETO FINAL - CRUZ BLANCA\n\n" +
                    "Objetivo:\n" +
                    "Forma una cruz blanca alrededor del centro blanco.\n\n" +
                    "Instrucciones:\n" +
                    "- Localiza las piezas blancas de borde.\n" +
                    "- Gira las caras necesarias.\n" +
                    "- Forma la cruz alrededor del centro blanco.\n\n" +
                    "Dificultad: Dificil";
                break;
        }

        Debug.Log("Reto seleccionado: " + DatosReto.retoSeleccionado);
    }
}