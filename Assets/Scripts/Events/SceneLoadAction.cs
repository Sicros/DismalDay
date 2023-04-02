using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadAction : MonoBehaviour
{
    // Referencia al controlador de carga de escenas.
    [SerializeField] private LoadSceneController _loadSceneController;

    // Referencia al transform del jugador.
    [SerializeField] private Transform player;

    // Referencia al scriptable object que almacena la última escena y la actual.
    public SceneInfo sceneInfo;

    // Obtención de componente de conttrol de escena. También se actualiza la última escena y la actual.
    // Al habilitar este componente se suscribe y llama al método de SceneLoaded(). Por último, se hace
    // una carga del estado actual del juego en un archivo temporal.
    private void OnEnable()
    {
        GameManager.instance.gameObject.TryGetComponent<LoadSceneController>(out _loadSceneController);
        sceneInfo.previousScene = sceneInfo.currentScene;
        sceneInfo.currentScene = SceneManager.GetActiveScene().name;
        SaveAndLoad.instance.LoadDataJSON("CurrentSave.json");
        SceneManager.sceneLoaded += SceneLoaded;
    }

    // Al deshabilitar este objeto, se hace un guardado del estado actual del juego y de desuscribe
    // el método del evento de carga de escena.
    private void OnDisable()
    {
        SaveAndLoad.instance.SaveDataJSON("CurrentSave.json");
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    // Permite obtener la posición y rotación que corresponde al personaje al entrar en una nueva escena.
    private void SceneLoaded(Scene oldScene, LoadSceneMode mode)
    {
        CharacterPositions _characterPosition = _loadSceneController.GetInitialPosition(sceneInfo.previousScene, sceneInfo.currentScene);
        player.position = _characterPosition.initialPosition;
        player.rotation = _characterPosition.initialRotation;
    }
}
