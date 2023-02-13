using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerToCollide;
    [SerializeField] private CharacterInputs _inputs;
    [SerializeField] private GameObject shootSound;
    private CharacterAttributes _character;
    private ZombieAttributes _zombie;
    private WeaponAttributes _weapon;
    private string _weaponName;
    private Light _pointer;
    private GameObject _instantiatedObject;
    private AudioSource _shootAudio;

    private void Start()
    {
        _weaponName = "";
        TryGetComponent<Light>(out _pointer);
        _pointer.enabled = false;
        GetMainWeapon();
        _instantiatedObject = null;
        transform.parent.TryGetComponent<AudioSource>(out _shootAudio);
        transform.Find("/Room/Swat").TryGetComponent<CharacterAttributes>(out _character);
    }

    private void Update()
    {
        if (_character.currentHealth > 0)
        {
            GetMainWeapon();
            ControlPointer();
            Shooting();
        }
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

            _shootAudio.Play(0);

            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerToCollide, QueryTriggerInteraction.Ignore))
            {
                _zombie = hit.collider.transform.GetComponent<ZombieAttributes>();
                _zombie.ReceiveDamage(_weapon.damage);
                Debug.Log(($"Hit: '{hit.transform.tag}' - damage: '{_weapon.damage}' - health: '{_zombie.currentHealth}'"));
            }

            if (_instantiatedObject != null)
            {
                Destroy(_instantiatedObject);
            }
            _instantiatedObject = Instantiate(shootSound, transform.position, transform.rotation);
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
