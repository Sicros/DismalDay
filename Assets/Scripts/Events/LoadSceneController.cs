using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public class LoadSceneController : MonoBehaviour
{
    public Dictionary <string, string> sceneNames = new Dictionary<string, string>();

    private void Awake()
    {
        sceneNames.Add("MainMenu", "Scene00");
        sceneNames.Add("MainRoom", "Scene01");
        sceneNames.Add("Snowfield", "Scene02");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneNames[sceneName], LoadSceneMode.Single);
    }
}
