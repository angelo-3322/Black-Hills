using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarSiguienteEscena : MonoBehaviour
{
    [SerializeField]
    private string nombreEscenaSiguiente;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(EndScene());
        }
    }

    IEnumerator EndScene()
    {
        GameObject fadeObject = GameObject.Find("Panel");

        Fade controller = fadeObject.GetComponent<Fade>();

        controller.FadeOut();

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(nombreEscenaSiguiente);
    }


}
