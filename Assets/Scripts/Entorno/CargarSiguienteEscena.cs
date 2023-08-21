using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarSiguienteEscena : MonoBehaviour
{
    [SerializeField]
    private string etiquetaObjeto = "Player";

    [SerializeField]
    private string nombreEscenaSiguiente;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(etiquetaObjeto))
        {
            SceneManager.LoadScene(nombreEscenaSiguiente);
        }
    }
}
