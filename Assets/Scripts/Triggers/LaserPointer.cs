using UnityEngine;

/*
Simula ser el puntero laser del arma utilizada por el personaje. Se apoya en este mismo puntero
para realizar el disparo en el punto rojo. Este provoca daño si impacta en un enemigo,
reduciendo su vida.
Esta acción está acompañada de su sonido de disparo, así como la habilitación del puntero
si el personaje esta con su camara en primera persona activa.
*/

public class LaserPointer : MonoBehaviour
{
    // Distancia máxima a la que llega el puntero.
    [SerializeField] private float maxDistance;

    // Layers con los que colisiona este puntero.
    [SerializeField] private LayerMask layerToCollide;
    
    // Objeto que simular el rango de sonido del disparo.
    [SerializeField] private GameObject shootSound;
    
    // Variable quue almacena los inputs del jugador.
    [SerializeField] private CharacterInputs _inputs;

    // Variable que almacena los atributos del personaje.
    private CharacterEntity _character;

    // Variable que almacena los atributos del zombie.
    private ZombieEntity _zombie;

    // Variable que almacena los atributos del arma del personaje.
    private WeaponAttributes _weapon;

    // Nombre del arma utilizada por el personaje.
    private string _weaponName;

    // Luz que simula el puntero rojo.
    private Light _pointer;

    // Variable que guarda la información de un objeto instanciado.
    private GameObject _instantiatedObject;

    // Fuente de audio del disparo.
    private AudioSource _shootAudio;

    // Fuente de audio del disparo de un arma vacía.
    private AudioSource _emptyShootAudio;

    // Momento en el que se puede realizar el próximo disparo.
    private float _nextTimeShoot;

    // Momento en el que terminar de cagar el arma.
    private float _timeToReload;

    private void Start()
    {
        // Inicialización del nombre del arma como vacío.
        _weaponName = "";

        // Inicialización del momento del próximo disparo.
        _nextTimeShoot = 0f;

        // Componente de luz asociado al objeto.
        TryGetComponent<Light>(out _pointer);

        // Inicialización del puntero.
        _pointer.enabled = false;

        // Inicialización del nombre del arma y atributos de la misma que utiliza el personaje.
        GetMainWeapon();

        // Inicialización de la variable referida al objeto instanciado.
        _instantiatedObject = null;

        // Componente del audio de disparo.
        transform.parent.Find("Audios/Shoot").TryGetComponent<AudioSource>(out _shootAudio);

        // Componente del audio de disparo vacío.
        transform.parent.Find("Audios/EmptyShoot").TryGetComponent<AudioSource>(out _emptyShootAudio);

        // Componen de los atributos del personaje.
        transform.Find("/Swat").TryGetComponent<CharacterEntity>(out _character);
    }

    // En cada frame se obtiene la información que está utilizando el personaje, el estado del puntero
    // (si está activado o desactivado), y si se ha generado un disparo. Todas estas acciones solo se
    // realizan si la vida el personaje es superior a 0.
    private void Update()
    {
        if (_character.GetCurrentHealth() > 0)
        {
            GetMainWeapon();
            ControlPointer();
            Shooting();
        }
    }

    // Si se encuentra que el objeto invocado tiene una etiqueta distinta a la anterior, se reasignan
    // los valores para los atributos y nombre del arma.
    private void GetMainWeapon()
    {
        if (transform.parent.tag != _weaponName)
        {
            transform.parent.TryGetComponent<WeaponAttributes>(out _weapon);
            _weaponName = transform.parent.tag;
        }
    }

    // Este método solo realiza acciones si se presionan los dos botones necesarios para disparar. Es
    // el que se encarga de hacer el disparo como tal e instanciar un objeto que atrae a los zombies
    // hasta su posición.
    private void Shooting()
    {
        if (Input.GetMouseButtonDown(_inputs.MouseButton(_inputs.actionButton)) && Input.GetMouseButton(_inputs.MouseButton(_inputs.aimButton)))
        {
            if (_weapon.currentBullets > 0)
            {
                if (_nextTimeShoot <= Time.time && _timeToReload <= Time.time && _weapon.currentBullets > 0)
                {
                    // Variable que almacena la información del objeto con el que colisiona el Raycast.
                    RaycastHit hit;

                    // Descuenta una bala de la carga actual de la pistola.
                    _weapon.currentBullets--;

                    // Se reproduce el audio de disparo.
                    _shootAudio.Play();

                    // Solo se consideran aquellos objeto del layer especificado y se ignoran los colliders de tipo trigger.
                    if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerToCollide, QueryTriggerInteraction.Ignore))
                    {
                        // Variable que almacena los atributos del zombie con el que colisiona el raycast.
                        _zombie = hit.collider.transform.GetComponent<ZombieEntity>();

                        // Invocación del método que provoca daño al zombie.
                        _zombie.ReceiveDamage(_weapon.damage);
                    }

                    // En caso de ya existir un objeto instanciado, este se destruye.
                    if (_instantiatedObject != null)
                    {
                        Destroy(_instantiatedObject);
                    }

                    // Instanciación del objeto con el collider que atraer a los zombis que estén en su radio hasta la
                    // posición en la que se produjo el disparo.
                    _instantiatedObject = Instantiate(shootSound, transform.position, transform.rotation);
                    _nextTimeShoot = Time.time + _weapon.timeBetweenShoots;
                }
            }
            else if (_character.inventoryCharacter[1].quantity > 0)
            {
                _weapon.currentBullets = _character.substractItem(1, 15);
                _timeToReload = Time.time + _weapon.reloadTime;
            }
            else
            {
                _emptyShootAudio.Play();
            }
        }
    }

    // Método que controla el puntero. Si está en modo primera persona, se activa, en caso contrario
    // es desactivado.
    private void ControlPointer()
    {
        if (Input.GetMouseButtonDown(_inputs.MouseButton(_inputs.aimButton)))
        {
            _pointer.enabled = true;
        }
        if (Input.GetMouseButtonUp(_inputs.MouseButton(_inputs.aimButton)))
        {
            _pointer.enabled = false;
        }
    }
}
