using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class LowHealthVignette : MonoBehaviour
{
    private Vignette _vignette;
    // [SerializeField] private PostProcessVolume _volume;
    [SerializeField] private VolumeProfile _profile;
    [SerializeField] private Color colorLowHealth;
    [SerializeField] private Color colorHighHealth;
    [SerializeField] private CharacterEntity _character;

    private void Start()
    {
        // gameObject.TryGetComponent<VolumeProfile>(out _profile);
        _profile.TryGet(out _vignette);
        _character.onHealthChangeC += ColorVignetteHandler;
    }

    public void ColorVignetteHandler(float currentHealth, float maxHealth)
    {
        bool isLowHealth = currentHealth / maxHealth < 0.33;
        _vignette.color.value = isLowHealth ? colorLowHealth : colorHighHealth;
    }
}
