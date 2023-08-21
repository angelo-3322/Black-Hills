using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCameraRotation : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 2.0f;

    [SerializeField]
    private Transform cabezaTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cabezaTransform == null)
        {
            Debug.LogError("Asigna la cámara al campo 'cabezaTransform' en el Inspector.");
        }
    }

    private void Update()
    {
        if (cabezaTransform != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            cabezaTransform.Rotate(Vector3.up * mouseX);
            cabezaTransform.Rotate(Vector3.left * mouseY);
        }
    }
}