using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    public float rayDistance;

    private Transform camara;
    public GameObject iconTake;
    private bool _isLookingAtInteractable = false;

    void Start()
    {
        camara = FindCameraInChildren(transform);
        iconTake.SetActive(false);
    }

    Transform FindCameraInChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag("MainCamera"))
            {
                return child;
            }

            Transform found = FindCameraInChildren(child);
            if (found != null)
            {
                return found;
            }
        }

        return null;
    }

    void Update()
    {
        if (camara == null)
        {
            Debug.LogWarning("No se encontró la cámara en la jerarquía del jugador.");
            return;
        }

        Debug.DrawRay(camara.position, camara.forward * rayDistance, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(camara.position, camara.forward, out hit, rayDistance, LayerMask.GetMask("Interactable")))
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>();

            if (interactable != null)
            {
                _isLookingAtInteractable = true;

                if (iconTake != null)
                {
                    iconTake.SetActive(true);
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            _isLookingAtInteractable = false;

            if (iconTake != null)
            {
                iconTake.SetActive(false);
            }
        }

        if (_isLookingAtInteractable && Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit2;
            if (Physics.Raycast(camara.position, camara.forward, out hit2, rayDistance, LayerMask.GetMask("Interactable")))
            {
                Interactable interactable = hit2.transform.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
