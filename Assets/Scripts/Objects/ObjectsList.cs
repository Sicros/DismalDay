using UnityEngine;

public class ObjectsList : MonoBehaviour
{
    public Item MedKit(int quantityGet)
    {
        return new Item {
            id = 0,
            nameObject = "Medkit",
            description = "Heals 5 HP",
            type = "HealingObject",
            healAmount = 5f,
            quantity = quantityGet,
            maxQuantity = 3,
            consumable = true,
            keyItem = false
        };
    }

    public Item HandgunBullets(int quantityGet)
    {
        return new Item {
            id = 1,
            nameObject = "Handguns's Bullets",
            description = "Ammunition for Handgun weapon",
            healAmount = 0f,
            quantity = quantityGet,
            maxQuantity = 60,
            consumable = true,
            keyItem = false
        };
    }

    public Item KeyDoor(int quantityGet)
    {
        return new Item {
            id = 2,
            nameObject = "Key door",
            description = "Key to open doors",
            healAmount = 0f,
            quantity = quantityGet,
            maxQuantity = 999,
            consumable = false,
            keyItem = true
        };
    }

    public Item GetItem(int id, int quantityGet)
    {
        switch (id)
        {
            case 0:
                return MedKit(quantityGet);
            case 1:
                return HandgunBullets(quantityGet);
            case 2:
                return KeyDoor(quantityGet);
            default:
                return new Item {

                };
        }
    }
}

public struct Item {
    public int id;
    public string nameObject;
    public string description;
    public string type;
    public float healAmount;
    public int quantity;
    public int maxQuantity;
    public bool consumable;
    public bool keyItem;
}