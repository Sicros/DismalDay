using UnityEngine;
using UnityEngine.Events;
using System;

public class InventoryController : MonoBehaviour
{
    // Referencia a los datos del personaje.
    [SerializeField] private CharacterData _characterData;

    // Referencia a la biblioteca de objetos del juego.
    [SerializeField] private ObjectsList objectsList;

    // Referencia a los atributos del arma.
    [SerializeField] private WeaponAttributes _weaponAttributes;

    // Evento Unity relacionado al cambio de la munición que lleva el personaje en su inventario.
    public UnityEvent<int> onBulletInventoryChangeUE;

    // Evento C# relacionado al cambio de la munición que lleva el personaje en su inventario.
    public event Action<int> onBulletInventoryChange;

    // Suscribe el método de "SubstractItem" al evento de recarga del arma.
    private void Awake()
    {
        _weaponAttributes.onBulletReload += SubstractItem;
    }

    // Obtiene el componente de la biblioteca de objetos y hace las primeras invocaciones de eventos.
    private void Start()
    {
        onBulletInventoryChange?.Invoke(GetQuantityObject(1));
        onBulletInventoryChangeUE?.Invoke(GetQuantityObject(1));
    }

    // Método que permite obtener la cantidad de elementos que posee de un objeto en específico.
    public int GetQuantityObject(int id)
    {
        for (int i = 0; i < _characterData.inventoryCharacter.Count; i++)
        {
            if (_characterData.inventoryCharacter[i].id == id)
            {
                return _characterData.inventoryCharacter[i].quantity;
            }
        }
        return 0;
    }

    // Método que permite añadir un objeto al inventario o incrementar su cantidad,
    // en caso de que ya se tuvieran anteriormente. Se ocupa el primer espacio del
    // inventario que se encuentre vacío en caso de ser un objeto que no se poseía
    // con anterioridad.
    public int AddItem(int id, int quantity)
    {
        int result = 0;
        for (int i = 0; i < _characterData.inventoryCharacter.Count; i++)
        {
            if (_characterData.inventoryCharacter[i].id == id || _characterData.inventoryCharacter[i].id == -1)
            {
                if (quantity + _characterData.inventoryCharacter[i].quantity <= objectsList.itemLibrary[id].maxQuantity)
                {
                    int newQuantity = _characterData.inventoryCharacter[i].quantity + quantity;
                    _characterData.inventoryCharacter[i] = new InventoryCharacter(id, newQuantity);
                    result = quantity;
                }
                else if (_characterData.inventoryCharacter[i].quantity < objectsList.itemLibrary[id].maxQuantity)
                {
                    result = objectsList.itemLibrary[id].maxQuantity - _characterData.inventoryCharacter[i].quantity;
                    _characterData.inventoryCharacter[i] = new InventoryCharacter(id, objectsList.itemLibrary[id].maxQuantity);
                }
                onBulletInventoryChangeUE?.Invoke(GetQuantityObject(1));
                onBulletInventoryChange?.Invoke(GetQuantityObject(1));
                Debug.Log("Bullets after add: " + GetQuantityObject(1));
                return result;
            }
        }
        return result;
    }

    // Método que permite quitar un objeto del inventario. Se resta la cantidad que se posea.
    // En caso de no poseer el objeto este se omite. Si la cantidad a sustraer es mayor
    // a la que se posee, solo devuelve la que se tiene en el inventario y se elimina el
    // objeto del mismo.
    public void SubstractItem(int id, int quantity)
    {
        int result = -1;
        for (int i = 0; i < _characterData.inventoryCharacter.Count; i++)
        {
            if (_characterData.inventoryCharacter[i].id == id)
            {
                if (quantity <= _characterData.inventoryCharacter[i].quantity)
                {
                    int newQuantity = _characterData.inventoryCharacter[i].quantity - quantity;
                    _characterData.inventoryCharacter[i] = new InventoryCharacter(id, newQuantity);
                    result = 0;
                }
                else
                {
                    result = quantity - _characterData.inventoryCharacter[i].quantity;
                    _characterData.inventoryCharacter[i] = new InventoryCharacter(id, 0);
                }
                if (_characterData.inventoryCharacter[i].quantity == 0)
                {
                    for (int j = i; j < _characterData.inventoryCharacter.Count; j++)
                    {
                        if (j == _characterData.inventoryCharacter.Count - 1)
                        {
                            _characterData.inventoryCharacter[j] = new InventoryCharacter(-1, 0);
                        }
                        else
                        {
                            _characterData.inventoryCharacter[j] = _characterData.inventoryCharacter[j + 1];                     
                        }
                    }
                }
                break;
            }
            if (_characterData.inventoryCharacter[i].id == -1)
            {
                break;
            }
        }
        if (id == 1 && result != -1)
        {    
            onBulletInventoryChangeUE?.Invoke(GetQuantityObject(1));
            onBulletInventoryChange?.Invoke(GetQuantityObject(1));
        }
        Debug.Log("Bullets after substract: " + GetQuantityObject(1));
    }
}
