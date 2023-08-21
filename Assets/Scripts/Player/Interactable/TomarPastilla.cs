using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarPastilla : Interactable
{
    public GameObject Pills;



    public override void Interact()
    {
        base.Interact();

        Destroy(Pills);
    }
}
