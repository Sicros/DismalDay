using UnityEngine;

public class SpiderKill : MonoBehaviour
{
    [SerializeField] private GameObject canvasEndGame;
    
    [SerializeField] private GameObject textEndGame;

    [SerializeField] private float timeToPrintMessage;

    [SerializeField] private LoadSceneController _loadSceneController;

    private float _nextPrintMessage;

    private bool _isEndGame;

    private void Start()
    {
        _isEndGame = false;
    }

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
