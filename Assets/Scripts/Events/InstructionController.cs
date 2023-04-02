using UnityEngine;

public class InstructionController : MonoBehaviour
{
    // Nombre de la escena al cargar tras presionar la tecla.
    [SerializeField] private string sceneName;

    // Referencia a componente de inputs del jugador.
    private KeyInputsSetup _keyInputsSetup;

    // Referencia al componente del controlador de carga de escenas.
    private LoadSceneController _loadSceneController;

    // Definición y obtención de componentes del controlador de carga de escenas e inputs.
    private void Start()
    {
        GameManager.instance.TryGetComponent<LoadSceneController>(out _loadSceneController);
        GameManager.instance.TryGetComponent<KeyInputsSetup>(out _keyInputsSetup);
    }

    // Si se presiona la tecla indicada, cargará la escena.
    private void Update()
    {
        if (Input.GetKeyDown(_keyInputsSetup.GetEscapeKey()))
        {
            _loadSceneController.LoadScene(sceneName);
        }
    }
}
