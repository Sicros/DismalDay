using UnityEngine;
using System;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int quantity;
    [SerializeField] private AudioSource pickupObject;
    [SerializeField] private CharacterEntity _character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pickupObject.Play();
            _character.addItem(id, quantity);
            Destroy(gameObject);
        }
    }
}
