using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadAction : MonoBehaviour
{
    [SerializeField] private LoadSceneController loadSceneController;
    [SerializeField] private Transform player;
    public SceneInfo sceneInfo;

    private void OnEnable()
    {
        sceneInfo.previousScene = sceneInfo.currentScene;
        sceneInfo.currentScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene oldScene, LoadSceneMode mode)
    {
        CharacterPositions _characterPosition = loadSceneController.GetInitialPosition(sceneInfo.previousScene, sceneInfo.currentScene);
        player.position = _characterPosition.initialPosition;
        player.rotation = _characterPosition.initialRotation;
        Debug.Log("Prueba de carga de escena");
        Debug.Log(sceneInfo.previousScene + " - " + sceneInfo.currentScene + ": " + player.position + " / " + player.rotation);
    }
}
