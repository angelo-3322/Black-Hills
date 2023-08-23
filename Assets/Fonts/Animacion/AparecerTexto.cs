using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerTexto : MonoBehaviour
{
    public Animator animator;

    void Start()
    {

    }

    public void Aparecer()
    {
        animator.Play("AparecerTexto");
    }
}
