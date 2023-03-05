using UnityEngine;
using System;
using UnityEngine.Events;

/*
Información relacionada a los atributos del arma. Actualmente solo se almacenan
el daño que provoca el arma al objetivo y el tiempo de espera entre disparos.
*/

public class WeaponAttributes : MonoBehaviour
{
    // Referencia al scriptable object que contiene los datos del arma.
    [SerializeField] private WeaponObject _weaponObject;

    // Referencia a los atributos del personaje.
    [SerializeField] private CharacterEntity _characterEntity;

    // Referencia al audio que se escucha al disparar un arma cargada.
    [SerializeField] private AudioSource ShootAudio;
    
    // Referencia al audio que se escucha al disparar un arma sin munición.
    [SerializeField] private AudioSource EmptyShootAudio;

    // Cantidad de balas que tiene cargada el arma actualmente.
    private int _currentBullets;
    
    // Próximo momento en que el arma puede volver a ser disparada.
    private float _timeNextShoot;

    // Cantidad de balas que posee el personaje en su inventario.
    private int _currentInventoryBullets;

    // Referencia a la clase de LaserPointer.
    [SerializeField] private LaserPointer _laserPointer;

    // Evento C# que es invocado al cargar el arma.
    public event Action<int, int> onBulletReload;
    
    // Evento C# que es invocado al momento de disparar una bala.
    public event Action<int, float> onBulletShot;

    // Evento Unity que es invocado al momento que la munición actual del arma cambia.
    public UnityEvent<int, int> onBulletChange;

    private void Start()
    {
        // Se asigna la munición máxima a la cantidad actual del arma.
        _currentBullets = _weaponObject.maxBullets;

        // Agrega como suscriptor el método HasBulletsInInventoryHandler al evento onBulletInventoryChange.
        // de la clase CharacterEntity
        _characterEntity.onBulletInventoryChange += HasBulletsInInventoryHandler;

        // Agrega como suscriptor el método ShootWeapon al evento onShoot de la clase LaserPointer.
        _laserPointer.onShoot += ShootWeapon;

        // Invoca los eventos definidos en esta clase para inicializar las variables de otras.
        onBulletShot?.Invoke(_currentBullets, 0);
        onBulletChange?.Invoke(_currentBullets, _weaponObject.maxBullets);
    }

    // Método invocado por la clase de LaserPointer a través del evento onShoot.
    // Simula el disparo como tal y es este método quien controla qué acciones realizar
    // de acuerdo a la munición del arma, la que tiene el personaje en su inventario, cuál audio
    // reproducir, entre otros.
    public void ShootWeapon()
    {
        Debug.Log("Event: onShoot / From: LaserPointer / To: WeaponAttributes");
        onBulletShot?.Invoke(_currentBullets, _timeNextShoot);
        if (_timeNextShoot < Time.time)
        {
            if (_currentBullets > 0)
            {
                _currentBullets--;
                ShootAudio.Play();
                onBulletChange?.Invoke(_currentBullets, _weaponObject.maxBullets);
                _timeNextShoot = Time.time + _weaponObject.timeBetweenShoots;
            }
            else if (_currentInventoryBullets > 0)
            {
                ReloadWeapon();
                onBulletChange?.Invoke(_currentBullets, _weaponObject.maxBullets);
            }
            else
            {
                EmptyShootAudio.Play();
            }
        }
    }

    // Método que simula la recarga del arma. De acuerdo a la munición que tenga el
    // personaje en su inventario, se realizarán diferentes acciones, además de restar
    // la munición cargada del inventario.
    public void ReloadWeapon()
    {
        _timeNextShoot = Time.time + _weaponObject.reloadTime;
        onBulletReload?.Invoke(1, _weaponObject.maxBullets);
        if (_currentInventoryBullets >= _weaponObject.maxBullets)
        {
            _currentBullets = _weaponObject.maxBullets;
        }
        else
        {
            _currentBullets = _currentInventoryBullets;
        }
    }

    // Obtiene la cantidad de balas que tiene el personaje en su inventario para utilizar
    // este valor en otros métodos.
    public void HasBulletsInInventoryHandler(int inventoryBullets)
    {
        Debug.Log("Event: onBulletInventoryChange / From: CharacterEntity / To: WeaponAttributes");
        _currentInventoryBullets = inventoryBullets;
    }

    // Método que permite obtener el valor del daño que provoca el arma por cada disparo.
    public float GetDamageWeapon()
    {
        return _weaponObject.damage;
    }
}
