using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player3DController : MonoBehaviour
{
    [SerializeField]
    float walkSpeed = 3.0f;

    [SerializeField]
    float runSpeed = 5.0f;

    [SerializeField]
    float rotationSpeed = 8.0f;

    [SerializeField]
    float mouseSensitivity = 8.0f;

    [SerializeField]
    float jumpForce = 6.0f;

    [SerializeField]
    float gravityMultiplier = 2.0f;

    [Header("Health")]
    [SerializeField]
    float health = 100.0F;

    [SerializeField]
    Slider healthbar;

    CharacterController characterController;
    Transform cameraTransform;


    public AudioSource Respiracion;
    private Animator modelAnimator;

    [SerializeField]
    float verticalRotation = 0.0f;
    [SerializeField]
    float verticalVelocity = 0.0f;


    bool isScared = false;
    bool isDead = false;

    void Start()
    {
        modelAnimator = GetComponentInChildren<Animator>();
    }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healthbar.maxValue = health;
        healthbar.value = health;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void HandleMovement()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isWalking = Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveZ) > 0.1f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && isWalking;

        modelAnimator.SetBool("isWalk", isWalking);
        modelAnimator.SetBool("isRunning", isRunning);

        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;
        Vector3 movement = moveDirection * moveSpeed;

        verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        movement.y = verticalVelocity;

        characterController.Move(movement * Time.deltaTime);
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);

        verticalRotation -= mouseY * rotationSpeed * Time.deltaTime;
        verticalRotation = Mathf.Clamp(verticalRotation, -90.0f, 90.0f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0.0f, 0.0f);
    }

    void HandleJump()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WitchStanding") && !isScared)
        {
            isScared = true;
            AudioSource audioSource1 = Instantiate(Respiracion, transform.position, Quaternion.identity);
            audioSource1.Play();
            Destroy(audioSource1.gameObject, 7F);
        }
    }
    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= Mathf.Abs(damage);
            healthbar.value = health;

            if (health <= 0)
            {
                isDead = true;
                StartCoroutine(ProcesoDeMuerte());
            }
        }
    }

    public void TakeHealth(float powerup)
    {
        health += Mathf.Abs(powerup);

        if (health > 100)
        {
            health = 100;
        }

        healthbar.value = health;
    }

    IEnumerator ProcesoDeMuerte()
    {
        isDead = true;
        BloquearVistaCamaraHaciaAdelante();
        modelAnimator.SetTrigger("_isDead");

        yield return new WaitForSeconds(5.0f);

        GameObject fadeObject = GameObject.Find("Panel");

        Fade controller = fadeObject.GetComponent<Fade>();

        controller.FadeOut();

        yield return new WaitForSeconds(2.0f);

        GameObject texto = GameObject.Find("MessageGameOver");

        AparecerTexto textoController = texto.GetComponent<AparecerTexto>();

        textoController.Aparecer();

        yield return new WaitForSeconds(4f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void BloquearVistaCamaraHaciaAdelante()
    {
        mouseSensitivity = 0.0f;

        cameraTransform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }
}
