using UnityEngine;
using UnityEngine.UI;
using System;

/*
Canvas del menú principal. Permite interactuar con los botones del menú principal.
En este caso, al presionar el botón de "Nuevo Juego" se cambiará a la escena de juego.
*/

public class CanvasController : MonoBehaviour
{
    // Botón para iniciar un nuevo juego.
    [SerializeField] private Button _startButton;

    // Botón para abrir los controles del juego.
    [SerializeField] private Button _controlButton;

    // Referencia al componente de control de escenas.
    private LoadSceneController _loadSceneController;
    
    // Referencia a los inputs del juego.
    private KeyInputsSetup _keyInputsSetup;

    // Evento invocado al pinchar en el botón de nuevo juego.
    public event Action<string> onStartGame;

    // Nombre de escena que carga al pinchar nuevo juego.
    [SerializeField] private string sceneName;

    // Referencia a componentes de control de escenas e inputs del jugador. También
    // se añaden los métodos a los eventos para iniciar juego y ver controles.
    private void Start()
    {
        GameManager.instance.TryGetComponent<LoadSceneController>(out _loadSceneController);
        GameManager.instance.TryGetComponent<KeyInputsSetup>(out _keyInputsSetup);
        onStartGame += _loadSceneController.LoadScene;
        _startButton.onClick.AddListener(StartGame);
        _controlButton.onClick.AddListener(OpenControlKeys);
    }

    // Permite volver a la pantalla principal en caso de estar revisando la pantalla de controles.
    private void Update()
    {
        if (!transform.Find("Panel").gameObject.activeSelf && Input.GetKeyDown(_keyInputsSetup.GetEscapeKey()))
        {
            transform.Find("PanelControl").gameObject.SetActive(false);
            transform.Find("Panel").gameObject.SetActive(true);
        }
    }

    // Método que inicia el nuevo juego. Se encarga de cargar la nueva escena.
    private void StartGame()
    {
        SaveAndLoad.instance.NewGame();
        SaveAndLoad.instance.ApplyValues();
        SaveAndLoad.instance.SaveDataJSON("CurrentSave.json");
        onStartGame?.Invoke(sceneName);
    }

    // Abre la pantalla con los controles del juego.
    private void OpenControlKeys()
    {
        transform.Find("PanelControl").gameObject.SetActive(true);
        transform.Find("Panel").gameObject.SetActive(false);
    }
}
