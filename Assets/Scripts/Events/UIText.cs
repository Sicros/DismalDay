using UnityEngine;
using TMPro;

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

    // Método llamado al cambiar la vida del personaje. Se actualiza este valor y la vida máxima
    // que tiene hasta el momento en la UI.
    public void UpdateHealthUI(float currentHealth, float maximumHealth)
    {
        Debug.Log("Event: onHealthChange / From: CharacterEntity / To: UIText");
        _textCurrentHealth.text = currentHealth.ToString();
        _textMaxHealth.text = "/ " + maximumHealth.ToString();
    }

    // Método llamado al cambiar la munición que tiene cargada el arma. Obtiene la munición actual
    // y máxima del arma actual para imprimirlo en la UI.
    public void UpdateBulletWeaponUI(int currentBullets, int maxBullets)
    {
        Debug.Log("Event: onBulletChange / From: WeaponAttributes / To: UIText");
        _textCurrentAmmo.text = currentBullets.ToString() + " / " + maxBullets.ToString();
    }

    // Método llamado al cambair la munición que lleva el personaje en su inventario.
    // Cambia el valor de este que se muestra en la UI.
    public void UpdateBulletInventoryUI(int bulletsInventory)
    {
        Debug.Log("Event: onBulletInventoryChangeUE / From: CharacterEntity / To: UIText");
        _textInventoryAmmo.text = bulletsInventory.ToString();
    }
}
