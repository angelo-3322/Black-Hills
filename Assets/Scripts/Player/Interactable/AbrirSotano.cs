using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirSotano : Interactable
{
    public GameObject puerta;
    public GameObject ObjetoLlave;

    public override void Interact()
    {
        base.Interact();
        Destroy(ObjetoLlave);

        if (puerta != null)
        {
            BoxCollider boxCollider = puerta.GetComponent<BoxCollider>();


            if (boxCollider != null)
            {
                boxCollider.enabled = true;
                
            }
        }


    }
}
