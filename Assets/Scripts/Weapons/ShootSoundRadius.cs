using System.Collections.Generic;
using UnityEngine;

/*
Simula el efecto que ocasiona el sonido de una bala, que atrae a los enemigos hasta su punto central,
que corresponde al punto en el que se generó el disparo.
El efecto solo se produce al inicio del disparo, posterior a este no activará otros zombies que
hayan entrado en la zona. También este objeto se detruye en uno de los siguientes casos o la
combinación de más de uno de ellos:
    a) Los zombies llegaron al punto central de la bala (con un margen de distancia).
    b) Los zombies fueron eliminados.
    c) Los zombies están observando, persiguiendo y/o atacando al jugador.
    d) El jugador ha generado un nuevo disparo.
*/

public class ShootSoundRadius : MonoBehaviour
{
    // Tiempo de duración del efecto que genera la bala. Esto evita que zombies que hayan
    // ingresado al rango después de haber disparado, no se vean afectados.
    [SerializeField] private float timeShootRange;

    // Momento del juego en que el rango del disparo deja de tener efecto.
    private float _timeAtShootDisappear;

    // Posición en la que se generó el disparo.
    private Vector3 ShootPosition;

    // Lista que almacena los ID de los zombies como objetos.
    private List <int> _zombiesId = new List<int> ();

    // Diccionario que almacena los Transforms de los zombies. La llave corresponde al ID de estos.
    private Dictionary <int, Transform> _zombiesTransform = new Dictionary <int, Transform> ();

    // Diccionario que almacena los atributos de los zombies. La llave corresponde al ID de estos.
    private Dictionary <int, ZombieAttributes> _zombiesAttributes = new Dictionary <int, ZombieAttributes> ();

    // Diccionario que almacena las animaciones de los zombies. La llave corresponde al ID de estos.
    private Dictionary <int, Animator> _zombiesAnimator = new Dictionary <int, Animator> ();

    // Diccionario que almacena el estado de los zombies (si están yendo al punto o no).
    // La llave corresponde al ID de estos.
    private Dictionary <int, bool> _zombiesFollowing = new Dictionary <int, bool> ();

    // Cálculo del próximo momento en que el rango de la bala dejará de tener efecto.
    private void Awake()
    {
        _timeAtShootDisappear = Time.time + timeShootRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo se consideran aquellos objetos que tengan la etiqueta de "Zombie" y
        // si se encuentran dentro del tiempo establecido.
        if (other.gameObject.tag == "Zombie" && _timeAtShootDisappear <= Time.time)
        {
            // Se almacena en una lista los IDs de cada zombie y en un diccionario lo que son los
            // Transforms, atributos y animaciones en un diccionario con su respectivo ID como llave.
            int id = other.gameObject.GetInstanceID();
            Transform _zombieTransform = other.transform;
            ZombieAttributes _zombieAttributes = other.transform.GetComponent<ZombieAttributes>();
            Animator _zombieAnimator = other.transform.GetComponent<Animator>();
            if (!_zombiesId.Contains(id))
            {
                _zombiesId.Add(id);
            }
            _zombiesTransform.Add(id, _zombieTransform);
            _zombiesAttributes.Add(id, _zombieAttributes);
            _zombiesAnimator.Add(id, _zombieAnimator);
            _zombiesFollowing.Add(id, true);
        }
    }

    private void Update()
    {
        // Recorrer lista de ID. Se utiliza este valor para consultar cada componente del objeto.
        foreach (int _zombieId in _zombiesId)
        {
            // Solo se consideran aquellos casos que están siguiendo el sonido de la bala.
            if (_zombiesFollowing[_zombieId])
            {
                // Mientras el zombie no esté persiguiendo ni atacando al jugador, ira hasta el punto de disparo.
                if (
                    !_zombiesAnimator[_zombieId].GetBool("isAttacking")
                    && !_zombiesAnimator[_zombieId].GetBool("isChasing")
                )
                {
                    // Posición del punto de disparo.
                    ShootPosition = transform.position;

                    // Activación de animación de caminar del zombie.
                    _zombiesAnimator[_zombieId].SetBool("justWalking", true);

                    // Centra la mirada en el punto que se generó el disparo
                    WatchingShoot(_zombiesTransform[_zombieId], _zombiesAttributes[_zombieId]);

                    // Camina hacia el punto de la bala. Se retorna un booleanos que corresponde al estado del zombie.
                    _zombiesFollowing[_zombieId] = ChasingShoot(_zombiesTransform[_zombieId], _zombiesAttributes[_zombieId], _zombiesAnimator[_zombieId]);
                }
                else
                {
                    // El zombie dejará de ir hasta el punto de disparo.
                    _zombiesFollowing[_zombieId] = false;

                    // Se eliminan los componentes de cada diccionario para ahorrar recursos.
                    _zombiesTransform.Remove(_zombieId);
                    _zombiesAttributes.Remove(_zombieId);
                    _zombiesAnimator.Remove(_zombieId);
                }
            }
        }
        // Una vez que no exista ningún zombie siguiendo el punto, se destruirá este mismo objeto.
        if(!_zombiesFollowing.ContainsValue(true))
        {
            Destroy(gameObject);
        }
    }

    // Método que permite al zombie avanzar hasta el punto de disparo. Requiere los atributos, transform y animaciones del zombie.
    // Tener en cuenta que se cambia el valor del eje Y al zombie para evitar que el objeto pueda avanzar o rotar hacia esta dirección.
    private bool ChasingShoot(Transform _zombieTransform, ZombieAttributes _zombieAttributes, Animator _zombieAnimator)
    {
        Vector3 shootNewPosition = new Vector3 (
            ShootPosition.x,
            _zombieTransform.position.y,
            ShootPosition.z
        );
        var vectorToShoot = shootNewPosition - _zombieTransform.position;
        if (vectorToShoot.magnitude > _zombieAttributes.keepDistanceShoot)
        {
            _zombieTransform.position += new Vector3 (vectorToShoot.normalized.x, 0, vectorToShoot.normalized.z) * _zombieAttributes.chasingSpeed * Time.deltaTime;
        }
        else
        {
            _zombieAnimator.SetBool("justWalking", false);
            return false;
        }
        return true;
    }

    // Método que permite al zombie rotar hacia el punto de disparo. Requiere los atributos y transform del zombie.
    // Tener en cuenta que se cambia el valor del eje Y al zombie para evitar que el objeto pueda avanzar o rotar hacia esta dirección.
    private void WatchingShoot(Transform _zombieTransform, ZombieAttributes _zombieAttributes)
    {
        Vector3 shootNewPosition = new Vector3 (
            ShootPosition.x,
            _zombieTransform.position.y,
            ShootPosition.z
        );
        var vectorToShoot = shootNewPosition - _zombieTransform.position;
        Quaternion newRotation = Quaternion.LookRotation(vectorToShoot);
        _zombieTransform.rotation = Quaternion.Lerp(_zombieTransform.rotation, newRotation, Time.deltaTime * _zombieAttributes.rotationSpeed);
    }
}
