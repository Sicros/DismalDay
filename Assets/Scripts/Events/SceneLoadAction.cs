using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadAction : MonoBehaviour
{
    private LoadSceneController _loadSceneController;
    [SerializeField] private Transform player;
    public SceneInfo sceneInfo;

    private void OnEnable()
    {
        GameManager.instance.TryGetComponent<LoadSceneController>(out _loadSceneController);
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
        CharacterPositions _characterPosition = _loadSceneController.GetInitialPosition(sceneInfo.previousScene, sceneInfo.currentScene);
        player.position = _characterPosition.initialPosition;
        player.rotation = _characterPosition.initialRotation;
    }
}
