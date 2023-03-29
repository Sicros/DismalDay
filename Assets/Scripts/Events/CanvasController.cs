using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/*
Canvas del menú principal. Permite interactuar con los botones del menú principal.
En este caso, al presionar el botón de "Nuevo Juego" se cambiará a la escena de juego.
*/

public class CanvasController : MonoBehaviour
{
    // Botón para iniciar un nuevo juego.
    [SerializeField] private Button _startButton;

    private LoadSceneController _loadSceneController;

    public event Action<string> onStartGame;

    [SerializeField] private string sceneName;

    private void Start()
    {
        GameManager.instance.TryGetComponent<LoadSceneController>(out _loadSceneController);
        onStartGame += _loadSceneController.LoadScene;
        _startButton.onClick.AddListener(StartGame);
    }

    // Método que inicia el nuevo juego. Se encarga de cargar la nueva escena.
    private void StartGame()
    {
        SaveAndLoad.instance.LoadDataJSON("/Saves/StartingSave.json");
        SaveAndLoad.instance.SaveDataJSON("/Saves/CurrentSave.json");
        onStartGame?.Invoke(sceneName);
    }
}
