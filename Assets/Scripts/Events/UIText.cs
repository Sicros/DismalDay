using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/*
Pensado en la UI del juego. Se encargar de mostrar los valores correspondientes
a la cantidad de munición que posee el personaje, así como la que está cargada en
su arma. También se muestra en otra esquina la vida que posee el personaje.
*/

public class UIText : MonoBehaviour
{
    // Texto relacionado a la vida actual del personaje.
    [SerializeField] private TMP_Text _textCurrentHealth;

    // Texto relacionado a la vida máxima del personaje.
    [SerializeField] private TMP_Text _textMaxHealth;

    // Texto relacionado a la munición actual cargada en el arma.
    [SerializeField] private TMP_Text _textCurrentAmmo;

    // Texto relacionado a la munición que se tiene en el inventario.
    [SerializeField] private TMP_Text _textInventoryAmmo;

    // Clase con los atributos del personaje. Utilizado para conocer su
    // inventario y vida.
    [SerializeField] private CharacterAttributes _characterAttributes;

    // Clase con los atributos del arma. Utilizado para conocer su munición
    // actual y la carga máxima que puede llevar.
    [SerializeField] private WeaponAttributes _weaponAttributes;

    // En cada frame se consultan los valores definidos anteriormente, para que puedan ser
    // impresos en la UI del juego.
    private void Update()
    {
        _textCurrentHealth.text = _characterAttributes.currentHealth.ToString();
        _textMaxHealth.text = "/ " + _characterAttributes.maximumHealth.ToString();
        _textCurrentAmmo.text = _weaponAttributes.currentBullets.ToString() + " / " + _weaponAttributes.maxBullets.ToString();
        _textInventoryAmmo.text = _characterAttributes.inventoryCharacter[1].quantity.ToString();
    }
}
