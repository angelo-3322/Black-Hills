using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaSotano : Interactable
{
    public GameObject puerta;

    public float velocidadRotacion;

    public override void Interact()
    {
        base.Interact();

        if (puerta != null)
        {
            Quaternion rotacionActual = puerta.transform.rotation;

            float nuevaRotacionY = rotacionActual.eulerAngles.y + velocidadRotacion * Time.deltaTime;

            Quaternion nuevaRotacion = Quaternion.Euler(rotacionActual.eulerAngles.x, nuevaRotacionY, rotacionActual.eulerAngles.z);

            puerta.transform.rotation = nuevaRotacion;

            if (nuevaRotacionY >= 90.0f)
            {
                puerta.transform.rotation = Quaternion.Euler(rotacionActual.eulerAngles.x, 90.0f, rotacionActual.eulerAngles.z);
                enabled = false;
            }
        }


    }
}
