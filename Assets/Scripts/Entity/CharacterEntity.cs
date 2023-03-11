using UnityEngine;
using UnityEngine.Events;
using System;

public class CharacterEntity : ObjectEntity
{
    // Refrencia a scriptable object que contiene los datos del personaje.
    [SerializeField] private CharacterData characterData;

    [SerializeField] private float _currentHealth;
    // Referencia a componente de animación del personaje.
    private Animator _animator;

    //Clase que contiene los métodos y listado de objetos
    [SerializeField] private ObjectsList itemList;

    // Arreglo que simular el inventario del personaje:
    // Posición 0: Medkits -> Objeto curativo del juego.
    // Posición 1: Balas -> Munición para la pistola.
    // Posición 2: Llaves -> Número de llaves que sirven para abrir puertas.
    public Item[] inventoryCharacter = new Item [3];

    // Referencia al arma que lleva el personaje.
    [SerializeField] private WeaponAttributes _weapon;

    // Evento C# relacionado al cambio de la vida actual del personaje.
    public UnityEvent<float, float> onHealthChange;

    // Evento Unity relacionado al cambio de la munición que lleva el personaje en su inventario.
    public UnityEvent<int> onBulletInventoryChangeUE;

    // Evento C# relacionado al cambio de la vida actual del personaje, para eventos C#.
    public Action<float, float> onHealthChangeC;

    // Evento C# relacionado al cambio de la munición que lleva el personaje en su inventario.
    public event Action<int> onBulletInventoryChange;

    // Se asigna la vida máxima al personaje como vida actual.
    private void Awake()
    {
        _currentHealth = characterData.maximumHealth;
    }

    // Obtención de componente de animación e iniciación de inventario del personaje.
    private void Start()
    {
        // Obtención del componente de animación al inicio del juego.
        transform.TryGetComponent<Animator>(out _animator);
        inventoryCharacter[0] = itemList.GetItem(0, 0);
        inventoryCharacter[1] = itemList.GetItem(1, 0);
        inventoryCharacter[2] = itemList.GetItem(2, 0);
        _weapon.onBulletReload += substractItem;
        onBulletInventoryChangeUE?.Invoke(inventoryCharacter[1].quantity);
        onHealthChangeC?.Invoke(_currentHealth, characterData.maximumHealth);
    }

    // Método que reproduce la animación de muerte del personaje.
    public void DeathAnimation()
    {
        _animator.SetTrigger("Die");
    }

    // Método que reproduce la animación del personaje cuando es dañado.
    public void GetHitAnimation()
    {
        _animator.Rebind();
        _animator.SetTrigger("getHit");
    }

    // Método con el que se agregan objetos al inventario. Se revisa el máximo que puede llevar.
    public void addItem(int id, int getQuantity)
    {
        if (inventoryCharacter[id].quantity + getQuantity >= inventoryCharacter[id].maxQuantity)
        {
            inventoryCharacter[id].quantity = inventoryCharacter[id].maxQuantity;
        }
        else
        {
            inventoryCharacter[id].quantity += getQuantity;
        }
        if (id == 1)
        {
            onBulletInventoryChange?.Invoke(inventoryCharacter[id].quantity);
            onBulletInventoryChangeUE?.Invoke(inventoryCharacter[id].quantity);
        }
    }

    // Método con el que se quitan objetos del inventario. Se revisa el mínimo que puede llevar.
    public void substractItem(int id, int substractQuantity)
    {
        Debug.Log("Event: onBulletReload / From: WeaponAttributes / To: CharacterEntity");
        if (inventoryCharacter[id].quantity - substractQuantity <= 0)
        {
            int currentQuantity = inventoryCharacter[id].quantity;
            inventoryCharacter[id].quantity = 0;
        }
        else
        {
            inventoryCharacter[id].quantity -= substractQuantity;
        }
        if (id == 1)
        {
            onBulletInventoryChange?.Invoke(inventoryCharacter[id].quantity);
            onBulletInventoryChangeUE?.Invoke(inventoryCharacter[id].quantity);
        }
    }

    // Método con el que el personaje recibe daño. Invocado por los enemigos para descontar
    // su daño de ataque a la vida del personaje.
    public void ReceiveDamage(float damage)
    {
        // Se consulta si la vida del personaje es menor al daño recibido.
        if (_currentHealth <= damage && _currentHealth != 0)
        {
            // Reproducción de animación de muerte del personaje.
            DeathAnimation();

            // Independiente de si el daño es igual o mayor a la vida actual, se asigna a esta última 0.
            _currentHealth = 0;

            // Se cambia la etiqueta del personaje para que no siga activando otros triggers.
            gameObject.tag = "PlayerDeath";
        }

        // Caso de que la vida actual del personaje sea mayor al daño recibido.
        if (_currentHealth > damage)
        {
            // Se reproduce la animación de recibir daño del personaje.
            GetHitAnimation();

            // Se resta el daño de la vida actual del personaje.
            _currentHealth -= damage;
        }
        onHealthChange?.Invoke(_currentHealth, characterData.maximumHealth);
        onHealthChangeC?.Invoke(_currentHealth, characterData.maximumHealth);
    }

    // Método utilizado para curar al personaje. Se entrega un valor de tipo float llamada heal
    // que corresponde al valor que se agregará a la vida del personaje.
    // Esta suma no puede superar la vida máxima, si este fuera el caso, se asigna esta vida
    // a la vida actual.
    public void ReceiveHeal(float heal)
    {
        if (_currentHealth + heal >= characterData.maximumHealth && _currentHealth > 0)
        {
            _currentHealth = characterData.maximumHealth;
        }
        if (_currentHealth + heal < characterData.maximumHealth && _currentHealth > 0)
        {
            _currentHealth += heal;
        }
        onHealthChange?.Invoke(_currentHealth, characterData.maximumHealth);
        onHealthChangeC?.Invoke(_currentHealth, characterData.maximumHealth);
    }

    // Método para alterar la velocidad de movimiento del personaje. Se espera un valor de tipo float
    // el que es multiplicado por su velocidad actual y el resultado se asigna a esta misma.
    public void ChangeSpeed(float multiplier)
    {
        characterData.speedWalkingUp *= multiplier;
    }

    // Retorna la vida actual del personaje.
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    // Retorna la velocidad con la que el personaje se mueve hacia delante.
    public float GetSpeedWalkingUp()
    {
        return characterData.speedWalkingUp;
    }

    // Retorna la velocidad con la que el personaje se mueve hacia atrás.
    public float GetSpeedWalkingDown()
    {
        return characterData.speedWalkingDown;
    }

    // Retorna la velocidad de rotación del personaje.
    public float GetRotationSpeed()
    {
        return characterData.rotationSpeed;
    }

    // Retorna el tiempo en que demorar el personaje en realizar una rotación de 180°.
    public float GetTimeCompleteRotation()
    {
        return characterData.timeCompleteRotation;
    }

    // Retorna la vida máxima del personaje.
    public float GetMaximumHealth()
    {
        return characterData.maximumHealth;
    }
}
