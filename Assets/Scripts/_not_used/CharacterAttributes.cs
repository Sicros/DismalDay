using UnityEngine;

/*
Información relacionada a los atributos del personaje como lo son su vida máxima y actual, los
métodos para que reciba daño o se recupere su vida. También contiene animaciones para
los casos en que sea golpeado o muera.
*/

public class CharacterAttributes2 : MonoBehaviour
{
    // Vida actual del personaje
    public float currentHealth;

    // Vida máxima del personaje.
    public float maximumHealth;

    // Referencia a componente de animación del personaje.
    private Animator _animator;

    //Clase que contiene los métodos y listado de objetos
    [SerializeField] private ObjectsList itemList;

    // Arreglo que simular el inventario del personaje:
    // Posición 0: Medkits -> Objeto curativo del juego.
    // Posición 1: Balas -> Munición para la pistola.
    // Posición 2: Llaves -> Número de llaves que sirven para abrir puertas.
    public Item[] inventoryCharacter = new Item [3];

    private void Start()
    {
        // Obtención del componente de animación al inicio del juego.
        transform.TryGetComponent<Animator>(out _animator);
        inventoryCharacter[0] = itemList.GetItem(0, 0);
        inventoryCharacter[1] = itemList.GetItem(1, 0);
        inventoryCharacter[2] = itemList.GetItem(2, 0);
    }

    public void ReceiveDamage(float damage)
    {
        // Se consulta si la vida del personaje es menor al daño recibido.
        if (currentHealth <= damage && currentHealth != 0)
        {
            // Reproducción de animación de muerte del personaje.
            DeathAnimation();

            // Independiente de si el daño es igual o mayor a la vida actual, se asigna a esta última 0.
            currentHealth = 0;

            // Se cambia la etiqueta del personaje para que no siga activando otros triggers.
            gameObject.tag = "PlayerDeath";
        }

        // Caso de que la vida actual del personaje sea mayor al daño recibido.
        if (currentHealth > damage)
        {
            // Se reproduce la animación de recibir daño del personaje.
            GetHitAnimation();

            // Se resta el daño de la vida actual del personaje.
            currentHealth -= damage;
        }
    }

    // Método utilizado para curar al personaje. Se entrega un valor de tipo float llamada heal
    // que corresponde al valor que se agregará a la vida del personaje.
    // Esta suma no puede superar la vida máxima, si este fuera el caso, se asigna esta vida
    // a la vida actual.
    public void ReceiveHeal(float heal)
    {
        if (currentHealth + heal >= maximumHealth && currentHealth > 0)
        {
            currentHealth = maximumHealth;
        }
        if (currentHealth + heal < maximumHealth && currentHealth > 0)
        {
            currentHealth += heal;
        }
    }

    // Método que reproduce la animación de muerte del personaje.
    public void DeathAnimation()
    {
        _animator.SetTrigger("Die");
    }

    // Método que reproduce la animación del personaje cuando es dañado.
    public void GetHitAnimation()
    {
        _animator.SetTrigger("getHit");
    }

    // Método con el que se agregan objetos al inventario. Se revisa el máximo que puede llevar.
    public bool addItem(int id, int getQuantity)
    {
        if (inventoryCharacter[id].quantity == inventoryCharacter[id].maxQuantity)
        {
            return false;
        }
        else if (inventoryCharacter[id].quantity + getQuantity >= inventoryCharacter[id].maxQuantity)
        {
            inventoryCharacter[id].quantity = inventoryCharacter[id].maxQuantity;
            return true;
        }
        else
        {
            inventoryCharacter[id].quantity += getQuantity;
            return true;
        }
    }

    // Método con el que se quitan objetos del inventario. Se revisa el mínimo que puede llevar.
    public int substractItem(int id, int substractQuantity)
    {
        if (inventoryCharacter[id].quantity == 0)
        {
            return 0;
        }
        else if (inventoryCharacter[id].quantity - substractQuantity <= 0)
        {
            int currentQuantity = inventoryCharacter[id].quantity;
            inventoryCharacter[id].quantity = 0;
            return currentQuantity;
        }
        else
        {
            inventoryCharacter[id].quantity -= substractQuantity;
            return substractQuantity;
        }
    }
}
