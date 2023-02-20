using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private int quantity;
    [SerializeField] private AudioSource pickupObject;
    [SerializeField] private CharacterAttributes _character;

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
