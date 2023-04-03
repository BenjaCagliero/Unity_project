using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIController : MonoBehaviour
{
    [SerializeField] private KeyController keyController;
    [SerializeField] private GateZoneController gateZoneController;
    private bool m_key = false;
    public Slider HPBar;
    public Slider StaminaBar;
    public Toggle CanDashTg;
    public Toggle Key;
    private GameManager gameManager;
    [SerializeField] private FPSController fPSController;

    [SerializeField] private float sprintTimer = Mathf.Clamp(0f, 0f, 5f);
    private float staminaPercentage;


    void Start()
    {
        HPBar.value = 100f;
        StaminaBar.value = 100f;
        CanDashTg.isOn = true;
        Key.isOn = false;
        keyController.onKeyPick += GotKey;
        gateZoneController.onGateZone += OnZone;
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
        var sprintTimer = fPSController.GetStamina();
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
    private void GotKey(bool key)
    {
        m_key = key;
        Key.isOn = true;
        keyController.onKeyPick -= GotKey;
    }
    private void OnZone(bool zone)
    {
        if (m_key)
        {
            Key.isOn = false;
            gateZoneController.onGateZone -= OnZone;
        }
    }
}

