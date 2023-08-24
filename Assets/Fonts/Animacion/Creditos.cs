using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : MonoBehaviour
{
    [SerializeField]
     Animator animator;

    void Start()
    {

    }

    public void Aparecer()
    {
        animator.Play("AparecerTexto");
    }
    public void Desaparecer()
    {
        animator.Play("Desaparecer");
    }

}
