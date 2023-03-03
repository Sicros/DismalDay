using System;
using UnityEngine;
using TMPro;

public class SubscriberEvent : MonoBehaviour
{
    [SerializeField] private TMP_Text m_currentHealthText;
    [SerializeField] private PublisherEvent m_publisherEvent;

    void Start()
    {
        // De esta forma logramos suscribir un método a un evento de Unity.
        // En este caso, el evento definido en PublisherEvent
        m_publisherEvent.OnHealthChange += UpdateHealthUI;
    }

    public void UpdateHealthUI(float p_currentHealth)
    {
        m_currentHealthText.text = $"Health: {p_currentHealth}";
        // De esta forma podemos quitar un suscriptor de este evento.
        // En este caso el método se llama una sola vez y luego se quita de la lista.
        m_publisherEvent.OnHealthChange -= UpdateHealthUI;
    }
}
