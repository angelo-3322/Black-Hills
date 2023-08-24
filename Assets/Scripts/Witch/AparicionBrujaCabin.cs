using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparicionBrujaCabin : MonoBehaviour
{
    [SerializeField]
     Transform spawnWitch;

    [SerializeField]
     GameObject spawnPrefab;

     float spawnTime = 0.3f;

     float currentTime = 0f;

    [SerializeField]
    GameObject trigger;

    [SerializeField]
    Vector3 WitchRotation;

    void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentTime >= spawnTime)
        {
            trigger.gameObject.SetActive(false);
            Instantiate(spawnPrefab, spawnWitch.position, Quaternion.Euler(WitchRotation));
            currentTime = 0f;
        }
    }
}
