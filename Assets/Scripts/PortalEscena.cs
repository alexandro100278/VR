using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalEscena : MonoBehaviour
{
    public string nombreEscena;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscena);
        }
    }
}