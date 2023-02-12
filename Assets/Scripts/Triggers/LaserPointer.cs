using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private CharacterInputs _inputs;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerToCollide;
    [SerializeField] private ZombieAttributes zombie;
    [SerializeField] private WeaponAttributes weapon;
    [SerializeField] private string weaponName;
    private Light pointer;

    private void Start()
    {
        weaponName = "";
        TryGetComponent<Light>(out pointer);
        pointer.enabled = false;
        GetMainWeapon();
    }

    private void Update()
    {
        GetMainWeapon();
        ControlPointer();
        Shooting();
    }

    private void GetMainWeapon()
    {
        if (transform.root.tag != weaponName)
        {
            transform.parent.TryGetComponent<WeaponAttributes>(out weapon);
            weaponName = transform.parent.tag;
        }
    }

    private void Shooting()
    {
        if (Input.GetMouseButtonDown(_inputs.MouseButton(_inputs.actionButton)))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerToCollide, QueryTriggerInteraction.Ignore))
            {
                zombie = hit.collider.transform.root.GetComponent<ZombieAttributes>();
                zombie.ReceiveDamage(weapon.damage);
                Debug.Log(($"Hit: '{hit.transform.tag}' - damage: '{weapon.damage}' - health: '{zombie.currentHealth}'"));
            }
        }
    }

    private void ControlPointer()
    {
        if (Input.GetMouseButtonDown(_inputs.MouseButton(_inputs.aimButton)))
        {
            pointer.enabled = true;
        }
        if (Input.GetMouseButtonUp(_inputs.MouseButton(_inputs.aimButton)))
        {
            pointer.enabled = false;
        }
    }
}
