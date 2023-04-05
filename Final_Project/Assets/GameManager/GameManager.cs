using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //[Range (0f, 100f)]public float playerHealth = 100f;
    private int killCount;
    private int hordeKills;
    [SerializeField]private int _score;
    [SerializeField]private int _dashes;
    private bool ending=false;
    private void Start()
    {
        killCount= 0;
        var scene = SceneManager.GetActiveScene();
        if (SceneManager.GetSceneByBuildIndex(3) == scene)
        {
            ending=true;
        }
    }


    


    public void addScore (int scoreTotal)
    {
        _score += scoreTotal;
    }
    public void addDash (int dashtotal)
    {
        _dashes += dashtotal;
    }


    private void Awake()
    {

        _score = 0;
        _dashes = 0;

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    //Controla las kills y devuelve los valores, para controlar el avance del nivel
    public void AddKill()
    {
        killCount ++;
        hordeKills ++;
    }
    public void ResetHorde()
    {
        hordeKills = 0;
    }
    public int GetKillCount()
    {
        return killCount;
    }
    public int GetHordeKills()
    {
        return hordeKills;
    }
}
