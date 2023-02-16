using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerToCollide;

    void Update()
    {
        // Variable que almacena la información del objeto con el que colisiona el Raycast.
        RaycastHit hit;

        // Solo se consideran aquellos objeto del layer especificado y se ignoran los colliders de tipo trigger.
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, maxDistance, layerToCollide, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.tag == "Floor")
            {
                audio.Play();
            }
        }
    }
}
