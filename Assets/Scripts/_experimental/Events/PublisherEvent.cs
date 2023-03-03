using UnityEngine;
using System;

public class PublisherEvent : MonoBehaviour
{
    public static PublisherEvent publisher;
    // De esta forma se define un evento. Se entregan como parámetros los tipos de
    // valores que puede recibir. También, si se antepone "event" impide que otras
    // clases suscritas a este evento lo puedan invocar.
    public event Action<float> OnHealthChange;
    public event Action<bool> OnDeath;
    public float m_currentHealth;

    void Awake()
    {
        publisher = this;
    }

    [ContextMenu("Get Damage")]
    public void ReceiveDamage(float p_currentDamage)
    {
        m_currentHealth -= p_currentDamage;
        // Agregar "?" al final permite preguntar primero si el evento tiene suscriptores.
        // De esta forma se evitan errores innecesarios.
        OnHealthChange?.Invoke(m_currentHealth);
    }

    [ContextMenu("Heal Damage")]
    public void HealDamage(float p_currentHeal)
    {
        m_currentHealth += p_currentHeal;
        OnHealthChange?.Invoke(m_currentHealth);
    }
}
