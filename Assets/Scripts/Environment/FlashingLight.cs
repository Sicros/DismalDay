using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    // Intensidad mínima de la luz
    [SerializeField] private float minIntensity;

    // Intensidad máxima de la luz
    [SerializeField] private float maxIntensity;

    // Tiempo entre que la luz cambia de intensidad
    [SerializeField] private float timeToChange;

    // Indica si la luz está en su máxima intensidad (true) o mínima (false)
    private bool _isTurnOn;

    // Tiempo transcurrido entre una intensidad y otra
    private float _timeElapsed;

    // Objeto que referencia al componente Light del objeto
    private Light _lightObject;

    // En este método se inician cada una de las variables.
    void Start()
    {
        _timeElapsed = 0;
        _isTurnOn = false;
        _lightObject = GetComponent<Light>();
        if (_lightObject != null)
        {
            _lightObject.intensity = minIntensity;
        }
    }

    // En cada frame se recalcula el tiempo transcurrido, de superar el límite establecido
    // desde el inspector, se realiza el cambio de intensidad y se reinicia el contador.
    void Update()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed >= timeToChange)
        {
            TurnFlashingLight();
            _timeElapsed = 0;
        }
    }

    // Método que permite alternar entre la intensidad mínima y máxima de la luz.
    // Se utiliza el booleano definido para identificar cual se debe asignar y
    // se le asigna a este bueno su valor opuesto.
    private void TurnFlashingLight()
    {
        if (_isTurnOn)
        {
            _lightObject.intensity = minIntensity;
        }
        else
        {
            _lightObject.intensity = maxIntensity;
        }
        _isTurnOn = !_isTurnOn;
    }
}
