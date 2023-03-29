using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadAction : MonoBehaviour
{
    [SerializeField] private LoadSceneController _loadSceneController;
    [SerializeField] private Transform player;
    public SceneInfo sceneInfo;

    private void OnEnable()
    {
        SaveAndLoad.instance.LoadDataJSON("/Saves/CurrentSave.json");
        GameManager.instance.gameObject.TryGetComponent<LoadSceneController>(out _loadSceneController);
        sceneInfo.previousScene = sceneInfo.currentScene;
        sceneInfo.currentScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void OnDisable()
    {
        SaveAndLoad.instance.SaveDataJSON("/Saves/CurrentSave.json");
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void SceneLoaded(Scene oldScene, LoadSceneMode mode)
    {
        CharacterPositions _characterPosition = _loadSceneController.GetInitialPosition(sceneInfo.previousScene, sceneInfo.currentScene);
        player.position = _characterPosition.initialPosition;
        player.rotation = _characterPosition.initialRotation;
    }
}
