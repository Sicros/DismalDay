using UnityEngine;

public class SpiderKill : MonoBehaviour
{
    // Referencia al panel de canvas que muestra la pantalla de fin del juego.
    [SerializeField] private GameObject canvasEndGame;
    
    // Referencia al texto de la pantalla de fin del juego.
    [SerializeField] private GameObject textEndGame;

    // Tiempo de espera antes de que imprima el texto en la pantalla de fin del juego.
    [SerializeField] private float timeToPrintMessage;

    // Referencia al controlador de carga de escenas.
    private LoadSceneController _loadSceneController;

    // Próximo momento en que podrá imprimir el mensaje de fin del juego.
    private float _nextPrintMessage;

    // Booleano que indica si se ha llegado al final del juego.
    private bool _isEndGame;

    // Se carga al inicio el componen de carga de escena y se marca el booleano de fin del juego con un falso.
    private void Start()
    {
        GameManager.instance.TryGetComponent<LoadSceneController>(out _loadSceneController);
        _isEndGame = false;
    }

    // Al entrar en el rango de la araña en que mata, mostrará la pantalla de fin del juego y
    // comenzará a hacer el conteo antes de imprimir el mensaje.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvasEndGame.SetActive(true);
            textEndGame.SetActive(false);
            _nextPrintMessage = timeToPrintMessage + Time.time;
            _isEndGame = true;
        }
    }

    // Revisa en cada Frame si ya se ha llegado al final del juego y si es momento de imprimir el mensaje.
    private void Update()
    {
        if (_isEndGame && _nextPrintMessage <= Time.time)
        {
            textEndGame.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _loadSceneController.LoadScene("MainMenu");
            }
        }
    }
}
