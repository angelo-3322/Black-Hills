using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class MovimientoWitch : MonoBehaviour
{
    [SerializeField]
    float velMov;

    [SerializeField]
    float velRot;

    [SerializeField]
    float damage = 30.0F;

    public Transform target;

    public AudioSource audioSusto;
    public AudioSource audioAmbiente;

    private int tiempoReaccion, movimiento;

    public float rangoAlerta = 20F;
    public float rangoAttack = 15F;
    public float velocidad = 25F;

    public LayerMask capaDelJugador;
    public LayerMask capaDeEspalda;

    private BoxCollider boxCollider;

    bool espera, camina, gira;

    bool estarAlerta;
    bool hacerScream;
    bool audioPlayed = false;
    bool audioAmbience = false;
    bool _isAttacked = false;

    public AudioSource terrenoAudioSource; //Quitar en Caba�a
    public float volumenAlerta = 0.03f; //Quitar en Caba�a
    private float volumenOriginal; //Quitar en Caba�a

    private AudioSource audioAmbienteSource;


    private Rigidbody rb;

    void Awake()
    {
        target = GameObject.Find("Player")?.transform;
        terrenoAudioSource = GameObject.Find("Forest").GetComponent<AudioSource>();

        if (terrenoAudioSource != null)
        {
            volumenOriginal = terrenoAudioSource.volume;
        }

        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
    }


    void Update()
    {

        estarAlerta = Physics.CheckSphere(transform.position, rangoAlerta, capaDelJugador);
        hacerScream = Physics.CheckSphere(transform.position, rangoAttack, capaDelJugador);

        if (espera)
        {
            GetComponent<Animator>().SetBool("Active", false);
            GetComponent<Animator>().SetBool("_isRunning", false);
        }
        if (estarAlerta)
        {

            if (terrenoAudioSource != null)
            {
                terrenoAudioSource.volume = volumenOriginal * volumenAlerta;
            }

            if (!audioAmbience && audioAmbienteSource == null)
            {
                audioAmbienteSource = Instantiate(audioAmbiente, transform.position, Quaternion.identity);
                audioAmbienteSource.Play();
                audioAmbience = true;
                Destroy(audioAmbienteSource.gameObject, 7F);
            }


            Vector3 posJugador = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(posJugador);

            GetComponent<Animator>().SetBool("Active", true);
            transform.position = Vector3.MoveTowards(transform.position, posJugador, velMov * Time.deltaTime);

            if (hacerScream)
            {
                DesactivarCollider();
                GetComponent<Animator>().SetBool("_isRunning", true);
                transform.position = Vector3.MoveTowards(transform.position, posJugador, velocidad * Time.deltaTime);

                if (!audioPlayed)
                {
                    AudioSource audioSource1 = Instantiate(audioSusto, transform.position, Quaternion.identity);
                    audioSource1.Play();
                    audioPlayed = true;
                    Destroy(audioSource1.gameObject, 2F);
                }
            }

            if (gira)
            {
                transform.Rotate(Vector3.up * velRot * Time.deltaTime);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAlerta);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAttack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {
            if (audioAmbienteSource != null)
            {
                audioAmbienteSource.Stop();
            }
            terrenoAudioSource.volume = volumenOriginal;
            Destroy(gameObject);
        }

        if (other.CompareTag("Player") && !_isAttacked)
        {
            _isAttacked = true;
            if (audioAmbienteSource != null)
            {
                audioAmbienteSource.Stop();
            }

            terrenoAudioSource.volume = volumenOriginal;
            Destroy(gameObject);

            Player3DController playerController = other.GetComponent<Player3DController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage);
            }
        }
    }

    void DesactivarCollider()
    {
        boxCollider.enabled = false;
    }

}
