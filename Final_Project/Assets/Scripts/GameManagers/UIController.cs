using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIController : MonoBehaviour
{

    public Slider HPBar;
    public Slider StaminaBar;
    public Toggle CanDashTg;
    private GameManager gameManager;
    [SerializeField] private FPSController fPSController;

    [SerializeField] private float sprintTimer = Mathf.Clamp(0f, 0f, 5f);
    private float staminaPercentage;


    void Start()
    {
        HPBar.value = 100f;
        StaminaBar.value = 100f;
        CanDashTg.isOn = true;

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Stamina();
        Dash();
        StaminaBar.value = staminaPercentage;
        HPBar.value = fPSController.GetHealth();
    }

    void Stamina()
    {
        // Se utiliza Input con los mismos valores para mostrar en Slider el tiempo de Stamina utilizable
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            if (sprintTimer >= 0)
            {
                sprintTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (sprintTimer < 5)
            {
                sprintTimer += Time.deltaTime;
            }
        }
        staminaPercentage = (sprintTimer) / 5 * 100;
    }
    void Dash()
    {
        if (fPSController.canDash)
        {
            CanDashTg.isOn = true;
        }
        else
        {
            CanDashTg.isOn = false;
        }

    }
}

