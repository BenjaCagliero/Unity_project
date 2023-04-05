using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UIController : MonoBehaviour
{
    [SerializeField] private KeyController keyController;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private GateZoneController gateZoneController;
    private bool m_key = false;
    private bool m_wep = false;
    public Slider HPBar;
    public Slider StaminaBar;
    public Toggle CanDashTg;
    public Toggle Key;
    public Toggle Weapon;
    private GameManager gameManager;
    [SerializeField] private FPSController fPSController;
    public CanvasRenderer welcomeTxt;
    public CanvasRenderer levelTxt;
    public CanvasRenderer levelDet;
    [SerializeField]private float txTimer = 5;
    [SerializeField]private float sprintTimer = Mathf.Clamp(0f, 0f, 5f);
    private float staminaPercentage;
    private float m_time;
    private float m_duration;


    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        m_time = -2;
        m_duration = txTimer *2;
        HPBar.value = 100f;
        StaminaBar.value = 100f;
        CanDashTg.isOn = true;
        Key.isOn = false;
        Weapon.isOn = false;
        var scene = SceneManager.GetActiveScene();
        if (SceneManager.GetSceneByBuildIndex(3) != scene)
        {
            keyController.onKeyPick += GotKey;  
            gateZoneController.onGateZone += OnZone;  
        }
        if(SceneManager.GetSceneByBuildIndex(2) != scene && SceneManager.GetSceneByBuildIndex(3) != scene)
        { 
            weaponController.onGrabPick += GotGrab;
        }
        else
        {
            m_wep = true;
            Weapon.isOn = true;
        }
        
        gameManager = FindObjectOfType<GameManager>();
        welcomeTxt.SetAlpha(0);
        levelTxt.SetAlpha(0);
        levelDet.SetAlpha(0);
    }

    void Update()
    {
        
        Stamina();
        Dash();
        StaminaBar.value = staminaPercentage;
        HPBar.value = fPSController.GetHealth();
        fade(txTimer, welcomeTxt);
        fade(txTimer, levelTxt);
        fade(txTimer, levelDet);
        Weapon.isOn = m_wep; 
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
    private void fade(float timer, CanvasRenderer text) 
    {
        
        if (m_time < timer)
        {
            m_time += Time.deltaTime;
            text.SetAlpha((m_time/timer)*2);
        }
        else if ( ((2*timer) > m_time) && (m_time >= timer))
        {
            m_time += Time.deltaTime;
            text.SetAlpha((m_duration - m_time)/timer * 2);
        }
        else
        {
            text.SetAlpha(0);
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
    private void GotGrab(bool weapon)
    {
            m_wep = weapon;
            Weapon.isOn = true;
            weaponController.onGrabPick -= GotGrab;
    }

}

