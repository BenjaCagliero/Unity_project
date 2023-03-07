using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIController : MonoBehaviour
{

    public Slider HPBar;
    public Slider StaminaBar;
    public Toggle CanDash;
    private GameManager gameManager;

    [SerializeField] private float sprintTimer = Mathf.Clamp(0f, 0f, 5f);
    private float staminaPercentage;
    [SerializeField] private bool evadeNow;
    public bool canEvade;
    [SerializeField] private float evadeDuration = 0.2f;
    [SerializeField] private float evadeTimer;


    void Start()
    {
        HPBar.value = 100f;
        StaminaBar.value = 100f;
        CanDash.isOn = true;
        canEvade= true;

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        Stamina();
        StaminaBar.value = staminaPercentage;
        HPBar.value = gameManager.playerHealth;
        Dash();
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
        bool evade = Input.GetKeyDown(KeyCode.E);
        if (evade)
        {
            if (canEvade)
            {
                evadeNow = true;
                CanDash.isOn = false; // Desactivar el Toggle "CanDash"
            }
        }
        if (evadeNow)
        {
            canEvade = false;
            if (evadeDuration > 0)
            {
                evadeDuration -= Time.deltaTime;
            }
            else
            {
                evadeDuration = 0.2f;
                evadeNow = false;
            }
        }
        if (!canEvade)
        {
            if (evadeTimer > 0)
            {
                evadeTimer -= Time.deltaTime;
            }
            else
            {
                evadeTimer = 3;
                canEvade = true;
                CanDash.isOn = true; // Reactivar el Toggle "CanDash"
            }
        }
    }
}

