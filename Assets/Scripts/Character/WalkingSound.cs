using UnityEngine;

/*
Script que permite reproducir un sonido cada vez que los pies del personaje toque
un objeto con la etiqueta de "Floor".
Se simula el sonido de los pasas cada vez que ocurre esta colisión.
*/

public class WalkingSound : MonoBehaviour
{
    // Fuente de audio. Esta es la que reproduce los sonidos de pasos.
    [SerializeField] private new AudioSource audio;

    // Distancia entre los pasos y el suelo antes de que se reproduzca el sonido.
    [SerializeField] private float maxDistance;

    // Layer que se considera para la colisión.
    [SerializeField] private LayerMask layerToCollide;

    void Update()
    {
        // Variable que almacena la información del objeto con el que colisiona el Raycast.
        RaycastHit hit;

        // Solo se consideran aquellos objeto del layer especificado y se ignoran los colliders de tipo trigger.
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, maxDistance, layerToCollide, QueryTriggerInteraction.Ignore))
        {
            // Si el objeto con el que colisiona tiene la etiqueta "Floor" se reproducirá el audio.
            if (hit.collider.gameObject.tag == "Floor")
            {
                audio.Play();
            }
        }
    }
}
