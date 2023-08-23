using UnityEngine;

public class LinternaController : MonoBehaviour
{
    private Light flashlight;

    public GameObject TurnOn;
    public GameObject TurnOff;


    private void Start()
    {
        flashlight = GameObject.Find("Linterna").GetComponent<Light>();
        flashlight.enabled = false;
        TurnOff.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;

            if (flashlight.enabled)
            {
                TurnOn.SetActive(true);
                TurnOff.SetActive(false);
            }
            else
            {
                TurnOn.SetActive(false);
                TurnOff.SetActive(true);
            }
        }

    }
}