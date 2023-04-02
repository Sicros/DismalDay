using UnityEngine;
using UnityEngine.UI;

public class ReadDocument : MonoBehaviour
{
    // Referencia a un archivo de texto.
    [SerializeField] TextAsset textFile;

    // Ruta para llegar al objeto de canvas de la escena.
    [SerializeField] private string pathCanvasObject;

    // Ruta que permite llegar al objeto que contiene el panel de documento.
    [SerializeField] private string pathDocumentPanel;

    // Variable que almacena el contenido del archivo de texto.
    private string _textString;

    // Referencia que guarda el objeto de canvas.
    private GameObject _canvasObject;
    
    // Referencia al componente de controlador UI Text.
    private UIText _uiTextController;

    // Referencia a los inputs del personaje.
    private KeyInputsSetup _inputs;

    // Carga de los componentes de inputs y UI Text. También se obtiene el texto del archivo
    // de texto y el objeto de canvas.
    private void Start()
    {
        GameManager.instance.TryGetComponent<KeyInputsSetup>(out _inputs);
        _textString = textFile.text;
        _canvasObject = GameObject.Find(pathCanvasObject);
        _canvasObject.TryGetComponent<UIText>(out _uiTextController);
        _canvasObject.transform.Find(pathDocumentPanel).gameObject.SetActive(false);
    }

    // El personaje al entrar en el rango del documento, podrá interactuar con él, lo que
    // imprimirá el contenido del documento en pantalla.
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(_inputs.GetInteractionKey()))
        {
            if(_canvasObject.transform.Find(pathDocumentPanel).gameObject.activeSelf)
            {
                _uiTextController.UpdateDocumenText("");
                _canvasObject.transform.Find(pathDocumentPanel).gameObject.SetActive(false);

            }
            else
            {
                _canvasObject.transform.Find(pathDocumentPanel).gameObject.SetActive(true);
                _canvasObject.transform.Find(pathDocumentPanel + "/Scroll View/Scrollbar Vertical").TryGetComponent<Scrollbar>(out Scrollbar _scrollbar);
                _uiTextController.UpdateDocumenText(_textString);
                _scrollbar.value = 1;
            }
        }
    }

    // Si el personaje sale del rango del documento, se desactivará.
    private void OnTriggerExit()
    {
        _uiTextController.UpdateDocumenText("");
        _canvasObject.transform.Find(pathDocumentPanel).gameObject.SetActive(false);
    }
}
