using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField] private CharacterInputs _inputs;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerToCollide;
    private Light pointer;

    private void Start()
    {
        TryGetComponent<Light>(out pointer);
        pointer.enabled = false;
    }

    private void Update()
    {
        ControlPointer();
        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetMouseButtonDown(_inputs.MouseButton(_inputs.actionButton)))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerToCollide, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("Hit: " + hit.transform.tag);
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
