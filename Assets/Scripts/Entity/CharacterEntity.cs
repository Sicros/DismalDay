using UnityEngine;
using UnityEngine.Events;
using System;

public class CharacterEntity : ObjectEntity
{
    // Refrencia a scriptable object que contiene los datos del personaje.
    [SerializeField] private CharacterData characterData;

    // Referencia a componente de animación del personaje.
    private Animator _animator;

    // Evento C# relacionado al cambio de la vida actual del personaje.
    public UnityEvent<float, float> onHealthChange;

    // Evento C# relacionado al cambio de la vida actual del personaje.
    public UnityEvent onCharacterDeathUE;

    // Evento C# relacionado al cambio de la vida actual del personaje, para eventos C#.
    public Action<float, float> onHealthChangeC;

    // Velocidad actual del personaje.
    private float _currentSpeed;

    // Obtención de componente de animación e iniciación de inventario del personaje.
    private void Start()
    {
        _currentSpeed = characterData.speedWalkingUp;
        // Obtención del componente de animación al inicio del juego.
        transform.TryGetComponent<Animator>(out _animator);
        onHealthChangeC?.Invoke(characterData.currentHealth, characterData.maximumHealth);
        onHealthChange?.Invoke(characterData.currentHealth, characterData.maximumHealth);
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

    // Método con el que el personaje recibe daño. Invocado por los enemigos para descontar
    // su daño de ataque a la vida del personaje.
    public void ReceiveDamage(float damage)
    {
        // Se consulta si la vida del personaje es menor al daño recibido.
        if (characterData.currentHealth <= damage && characterData.currentHealth != 0)
        {
            // Reproducción de animación de muerte del personaje.
            DeathAnimation();

            // Independiente de si el daño es igual o mayor a la vida actual, se asigna a esta última 0.
            characterData.currentHealth = 0;

            // Se cambia la etiqueta del personaje para que no siga activando otros triggers.
            gameObject.tag = "PlayerDeath";

            // Carga de pantalla de GameOver
            onCharacterDeathUE?.Invoke();
        }

        // Caso de que la vida actual del personaje sea mayor al daño recibido.
        if (characterData.currentHealth > damage)
        {
            // Se reproduce la animación de recibir daño del personaje.
            GetHitAnimation();

            // Se resta el daño de la vida actual del personaje.
            characterData.currentHealth -= damage;
        }
        onHealthChange?.Invoke(characterData.currentHealth, characterData.maximumHealth);
        onHealthChangeC?.Invoke(characterData.currentHealth, characterData.maximumHealth);
    }

    // Método utilizado para curar al personaje. Se entrega un valor de tipo float llamada heal
    // que corresponde al valor que se agregará a la vida del personaje.
    // Esta suma no puede superar la vida máxima, si este fuera el caso, se asigna esta vida
    // a la vida actual.
    public void ReceiveHeal(float heal)
    {
        if (characterData.currentHealth + heal >= characterData.maximumHealth && characterData.currentHealth > 0)
        {
            characterData.currentHealth = characterData.maximumHealth;
        }
        if (characterData.currentHealth + heal < characterData.maximumHealth && characterData.currentHealth > 0)
        {
            characterData.currentHealth += heal;
        }
        onHealthChange?.Invoke(characterData.currentHealth, characterData.maximumHealth);
        onHealthChangeC?.Invoke(characterData.currentHealth, characterData.maximumHealth);
    }

    // Método para alterar la velocidad de movimiento del personaje. Se espera un valor de tipo float
    // el que es multiplicado por su velocidad actual y el resultado se asigna a esta misma.
    public void ChangeSpeed(bool isWalking)
    {
        _currentSpeed = isWalking ? characterData.speedWalkingUp : characterData.speedRunningUp;
    }

    // Retorna la vida actual del personaje.
    public float GetCurrentHealth()
    {
        return characterData.currentHealth;
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

    // Retorna la velocidad con la que el personaje corre hacia delante.
    public float GetSpeedRunningUp()
    {
        return characterData.speedRunningUp;
    }

    // Retorna la velocidad actual con la que se mueve el personaje.
    public float GetCurrentSpeed()
    {
        return _currentSpeed;
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

    // Animación de recarga.
    public void ReloadTransition(bool _isReloading)
    {
        _animator.SetBool("isReloading", _isReloading);
    }
}
