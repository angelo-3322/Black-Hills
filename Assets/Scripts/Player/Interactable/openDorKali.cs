using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDorKali : Interactable
{
    [SerializeField]
     GameObject puerta;
    [SerializeField]
     float velocidadRotacion;

    [SerializeField]
    GameObject personaje;

    public override void Interact()
    {
        base.Interact();
        MovimientoNPC npc = personaje.GetComponent<MovimientoNPC>();

        if (puerta != null)
        {
            Quaternion rotacionActual = puerta.transform.rotation;

            float nuevaRotacionY = rotacionActual.eulerAngles.y + velocidadRotacion * Time.deltaTime;

            Quaternion nuevaRotacion = Quaternion.Euler(rotacionActual.eulerAngles.x, nuevaRotacionY, rotacionActual.eulerAngles.z);

            puerta.transform.rotation = nuevaRotacion;

            npc.enabled = true;

            if (nuevaRotacionY >= 0.0f)
            {
                puerta.transform.rotation = Quaternion.Euler(rotacionActual.eulerAngles.x, 0.0f, rotacionActual.eulerAngles.z);
                enabled = false;
            }
        }


    }
}
