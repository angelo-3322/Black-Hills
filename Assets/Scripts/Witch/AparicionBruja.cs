using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparicionBruja : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject witchObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(HideTrigger());
        }
    }

    private IEnumerator HideTrigger()
    {
        AudioSource audioSource1 = Instantiate(audioSource, transform.position, Quaternion.identity);
        audioSource1.Play();

        yield return new WaitForSeconds(6f);

        Destroy(witchObject);
    }
}
