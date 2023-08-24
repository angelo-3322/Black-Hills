using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mostrarCreditos());
    }
    IEnumerator mostrarCreditos ()
    {
        GameObject fadeObject = GameObject.Find("Nombres");



        Creditos controller = fadeObject.GetComponent<Creditos>();



        controller.Aparecer();



        yield return new WaitForSeconds(5f);

        controller.Desaparecer();

        yield return new WaitForSeconds(2f);

        GameObject NombreJuegoObject = GameObject.Find("NombreJuego");



        Creditos controllerJuego = NombreJuegoObject.GetComponent<Creditos>();



        controllerJuego.Aparecer();

        yield return new WaitForSeconds(5f);

        controllerJuego.Desaparecer();


    }

}
