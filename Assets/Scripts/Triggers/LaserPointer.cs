using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerToCollide;
    [SerializeField] private CharacterInputs _inputs;
    private ZombieAttributes _zombie;
    private WeaponAttributes _weapon;
    private string _weaponName;
    private Light _pointer;

    private void Start()
    {
        _weaponName = "";
        TryGetComponent<Light>(out _pointer);
        _pointer.enabled = false;
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
        if (transform.root.tag != _weaponName)
        {
            transform.parent.TryGetComponent<WeaponAttributes>(out _weapon);
            _weaponName = transform.parent.tag;
        }
    }

    private void Shooting()
    {
        if (Input.GetMouseButtonDown(_inputs.MouseButton(_inputs.actionButton)) && Input.GetMouseButton(_inputs.MouseButton(_inputs.aimButton)))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerToCollide, QueryTriggerInteraction.Ignore))
            {
                _zombie = hit.collider.transform.root.GetComponent<ZombieAttributes>();
                _zombie.ReceiveDamage(_weapon.damage);
                Debug.Log(($"Hit: '{hit.transform.tag}' - damage: '{_weapon.damage}' - health: '{_zombie.currentHealth}'"));
            }
        }
    }

    private void ControlPointer()
    {
        if (Input.GetMouseButtonDown(_inputs.MouseButton(_inputs.aimButton)))
        {
            _pointer.enabled = true;
        }
        if (Input.GetMouseButtonUp(_inputs.MouseButton(_inputs.aimButton)))
        {
            _pointer.enabled = false;
        }
    }
}
