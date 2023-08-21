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

            float nuevaRotacionZ = rotacionActual.eulerAngles.z + velocidadRotacion * Time.deltaTime;

            Quaternion nuevaRotacion = Quaternion.Euler(rotacionActual.eulerAngles.x, rotacionActual.eulerAngles.y, nuevaRotacionZ);

            puerta.transform.rotation = nuevaRotacion;

            if (nuevaRotacionZ >= -90.0f)
            {
                puerta.transform.rotation = Quaternion.Euler(rotacionActual.eulerAngles.x, rotacionActual.eulerAngles.y, -90.0f);
                enabled = false;
            }
        }

    }
}
