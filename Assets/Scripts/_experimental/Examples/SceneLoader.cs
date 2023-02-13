using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadScene();
        }        
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Scene2");
    }
}
