using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    private TextMeshProUGUI nombreTextMeshPro;
    private TextMeshProUGUI dialogoTextMeshPro;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    GameObject barra;

    [SerializeField]
    private string nombre;

    [SerializeField]
    private string dialogo;

    private void Start()
    {
        nombreTextMeshPro = GameObject.Find("Nombre").GetComponent<TextMeshProUGUI>();
        dialogoTextMeshPro = GameObject.Find("Dialogo").GetComponent<TextMeshProUGUI>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (nombreTextMeshPro != null && dialogoTextMeshPro != null)
            {
                nombreTextMeshPro.text = nombre;
                dialogoTextMeshPro.text = dialogo;
                barra.SetActive(true);

                if (audioSource != null)
                {
                    AudioSource audioSource1 = Instantiate(audioSource, transform.position, Quaternion.identity);
                    audioSource1.Play();
                    StartCoroutine(DeactivateBarAfterAudio());
                }
            }
        }
    }

    IEnumerator DeactivateBarAfterAudio()
    {
        yield return new WaitForSeconds(audioSource.clip.length);

        if (barra != null)
        {
            barra.SetActive(false);
        }
    }
}