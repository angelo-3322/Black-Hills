using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    private Transform camara;

    public float rayDistance;

    void Start()
    {
        camara = FindCameraInChildren(transform);
    }

    Transform FindCameraInChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag("MainCamera")) // Puedes usar una etiqueta para identificar la c�mara si es necesario.
            {
                return child;
            }

            Transform found = FindCameraInChildren(child);
            if (found != null)
            {
                return found;
            }
        }

        return null; // No se encontr� la c�mara en los hijos.
    }

    void Update()
    {
        if (camara == null)
        {
            Debug.LogWarning("No se encontr� la c�mara en la jerarqu�a del jugador.");
            return;
        }

        Debug.DrawRay(camara.position, camara.forward * rayDistance, Color.red);

        if (Input.GetButton("Fire1"))
        {
            RaycastHit hit;

            if (Physics.Raycast(camara.position, camara.forward, out hit, rayDistance, LayerMask.GetMask("Interactable")))
            {
                hit.transform.GetComponent<Interactable>().Interact();
            }
        }
    }


}
