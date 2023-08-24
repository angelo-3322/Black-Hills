using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorClosed : Interactable
{
    public AudioSource SoundClosed;
    private bool isSoundPlaying = false;

    private TextMeshProUGUI nombreTextMeshPro;
    private TextMeshProUGUI dialogoTextMeshPro;

    [SerializeField]
    GameObject barra;

    [SerializeField]
    private string nombre;

    [SerializeField]
    private string dialogo;

    void Start()
    {
        StartCoroutine(ActivarBarra());

        barra = GameObject.Find("Barra");

        if (barra == null)
        {
            Debug.LogError("No se encontró el objeto 'Barra' en la jerarquía.");
        }
        else if (!barra.activeSelf)
        {
            barra.SetActive(true);
        }
    }

    public override void Interact()
    {
        base.Interact();

        barra.gameObject.SetActive(true);

        if (!isSoundPlaying)
        {
            isSoundPlaying = true;
            AudioSource sound = Instantiate(SoundClosed, transform.position, Quaternion.identity);


            nombreTextMeshPro.text = nombre;
            dialogoTextMeshPro.text = dialogo;
            barra.SetActive(true);

            sound.Play();
            Destroy(sound.gameObject, 2F);
            StartCoroutine(ResetSoundStatus());
        }
    }

    private IEnumerator ResetSoundStatus()
    {
        yield return new WaitForSeconds(1F);
        isSoundPlaying = false;

        yield return new WaitForSeconds(5F);

        if (barra != null)
        {
            barra.SetActive(false);
        }
    }

    private IEnumerator ActivarBarra()
    {
        barra.gameObject.SetActive(true);

        nombreTextMeshPro = GameObject.Find("Nombre").GetComponent<TextMeshProUGUI>();
        dialogoTextMeshPro = GameObject.Find("Dialogo").GetComponent<TextMeshProUGUI>();

        yield return new WaitForSeconds(0.1F);

        barra.gameObject.SetActive(false);
    }
}
