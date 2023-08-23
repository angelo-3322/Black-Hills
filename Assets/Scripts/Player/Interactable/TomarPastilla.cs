using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarPastilla : Interactable
{
    public GameObject Pills;
    public Player3DController _controller;

    [SerializeField]
    float _powerup = 25.0F;


    void Awake()
    {
        _controller = GetComponent<Player3DController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player3DController controller = other.GetComponent<Player3DController>();
            controller.TakeHealth(_powerup);
        }

    }

    public override void Interact()
    {
        //base.Interact();


        Destroy(Pills);
    }
    
}
