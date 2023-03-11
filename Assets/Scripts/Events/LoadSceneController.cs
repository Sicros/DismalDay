using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
