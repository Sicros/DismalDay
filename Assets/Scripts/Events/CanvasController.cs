using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Canvas del menú principal. Permite interactuar con los botones del menú principal.
En este caso, al presionar el botón de "Nuevo Juego" se cambiará a la escena de juego.
*/

public class CanvasController : MonoBehaviour
{
    // Botón para iniciar un nuevo juego.
    [SerializeField] private Button _startButton;

    private void Update()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    // MNétodo que inicia el nuevo juego. Se encarga de cargar la nueva escena.
    private void StartGame()
    {
        SceneManager.LoadScene("Scene01", LoadSceneMode.Single);
    }
}
