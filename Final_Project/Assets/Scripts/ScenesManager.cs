using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";
    public string gameLevel1SceneName = "GameLevel1";
    public string gameLevel2SceneName = "GameLevel2";
    public string gameLevel3SceneName = "GameLevel3";

    [SerializeField]private Scene mainMenuScene;
    [SerializeField]private Scene gameLevel1Scene;
    [SerializeField]private Scene gameLevel2Scene;
    [SerializeField]private Scene gameLevel3Scene;

    void Awake()
    {

        SceneManager.LoadScene("MainMenu");
        SceneManager.LoadScene(gameLevel3SceneName);
        SceneManager.LoadScene(gameLevel2SceneName);
        SceneManager.LoadScene(gameLevel1SceneName);
        SceneManager.UnloadSceneAsync(gameLevel1Scene);
        SceneManager.UnloadSceneAsync(gameLevel3Scene);
        SceneManager.UnloadSceneAsync(gameLevel2Scene);
    }

    public void LoadMainMenu()
    {
        SetActiveScene(mainMenuScene);
        
    }

    public void LoadGameLevel1()
    {
        SceneManager.LoadScene(gameLevel1SceneName);
        SetActiveScene(gameLevel1Scene);
        
    }

    public void LoadGameLevel2()
    {
        SceneManager.LoadScene(gameLevel1SceneName);
        SetActiveScene(gameLevel2Scene);
    }

    public void LoadGameLevel3()
    {
        SceneManager.LoadScene(gameLevel1SceneName);
        SetActiveScene(gameLevel3Scene);
    }

    // Función auxiliar para activar la escena correspondiente y desactivar todas las demás
    private void SetActiveScene(Scene scene)
    {
        // Desactiva todas las escenas
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            go.SetActive(false);
        }

        // Activa la escena correspondiente
        SceneManager.SetActiveScene(scene);

        // Activa los GameObjects en la escena correspondiente
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            go.SetActive(true);
        }
    }
}