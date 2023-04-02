using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class LowHealthVignette : MonoBehaviour
{
    // Referencia a la viñeta del post processing.
    private Vignette _vignette;

    // Referencia al perfil del volumen.
    [SerializeField] private VolumeProfile _profile;

    // Color que debe mostrar cuando la vida está baja.
    [SerializeField] private Color colorLowHealth;

    // Color que debe mostrar cuando la vida está alta.
    [SerializeField] private Color colorHighHealth;

    // Referencia a la entidad del personaje.
    [SerializeField] private CharacterEntity _character;

    // Carga del componente de viñeta así como también la suscripción al evento de cambio de vida.
    private void Start()
    {
        _profile.TryGet(out _vignette);
        _character.onHealthChangeC += ColorVignetteHandler;
    }

    // Método que alterna entre los colores de viñeta definidos. En caso de que la vida
    // sea menor a un 33%, se cambiará.
    public void ColorVignetteHandler(float currentHealth, float maxHealth)
    {
        bool isLowHealth = currentHealth / maxHealth < 0.33;
        _vignette.color.value = isLowHealth ? colorLowHealth : colorHighHealth;
    }
}