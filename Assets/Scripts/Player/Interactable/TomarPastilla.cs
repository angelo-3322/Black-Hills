using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarPastilla : Interactable
{
    public GameObject Pills;
    
    [SerializeField]
    float _medicine = 25.0F;



    public override void Interact()
    {
        base.Interact();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Player3DController playerController = player.GetComponent<Player3DController>();

            if (playerController != null)
            {
                playerController.TakeHealth(_medicine);

                Destroy(Pills);
            }
            else
            {
                Debug.LogWarning("No se encontró el componente PlayerController en el objeto del jugador.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con la etiqueta 'Player'.");
        }
    }

}
