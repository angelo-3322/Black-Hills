using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroController : MonoBehaviour
{
    [SerializeField]
    private float velocidad = 14.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 velocidadAvance = transform.right * velocidad;

        rb.velocity = velocidadAvance;
    }
}
