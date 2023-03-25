using UnityEngine;

public class ReadDocument : MonoBehaviour
{
    // Referencia a los inputs del personaje.
    [SerializeField] private CharacterInputs characterInputs;

    [SerializeField] TextAsset textFile;

    [SerializeField] private string pathCanvasObject;

    [SerializeField] private string pathDocumentPanel;

    private string _textString;

    private GameObject _canvasObject;
    
    private UIText _uiTextController;

    private void Awake()
    {
        _textString = textFile.text;
        _canvasObject = GameObject.Find(pathCanvasObject);
        _canvasObject.TryGetComponent<UIText>(out _uiTextController);
        _canvasObject.transform.Find(pathDocumentPanel).gameObject.SetActive(false);
    }

    // Al entrar en contacto con el objeto, este será recogido, siempre y cuando
    // el personaje tenga espacio en su inventario o no lleve la cantidad máxima del
    // objeto.
    private void OnTriggerStay(Collider other)
    {
        if (
            other.tag == "Player"
            && Input.GetMouseButtonDown(characterInputs.MouseButton(characterInputs.actionButton))
            && !Input.GetMouseButton(characterInputs.MouseButton(characterInputs.aimButton))
        )
        {
            if(_canvasObject.transform.Find(pathDocumentPanel).gameObject.activeSelf)
            {
                _uiTextController.UpdateDocumenText("");
                _canvasObject.transform.Find(pathDocumentPanel).gameObject.SetActive(false);
            }
            else
            {
                _canvasObject.transform.Find(pathDocumentPanel).gameObject.SetActive(true);
                _uiTextController.UpdateDocumenText(_textString);
            }
        }
    }

    private void OnTriggerExit()
    {
        _uiTextController.UpdateDocumenText("");
        _canvasObject.transform.Find(pathDocumentPanel).gameObject.SetActive(false);
    }
}
