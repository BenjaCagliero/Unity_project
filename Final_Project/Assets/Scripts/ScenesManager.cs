using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string mainMenu = "Main Menu";
    public string lvl1 = "GameLevel1";
    public string lvl2 = "GameLevel2";
    public string lvl3 = "GameLevel3";



    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        
    }

    public void LoadGameLevel1()
    {
        SceneManager.LoadScene(lvl1);
        
    }

    public void LoadGameLevel2()
    {
        SceneManager.LoadScene(lvl2);

    }

    public void LoadGameLevel3()
    {
        SceneManager.LoadScene(lvl3);
    }

}