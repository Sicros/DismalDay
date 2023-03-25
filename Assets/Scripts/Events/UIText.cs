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

    // Texto relacionado a las interacciones con objetos del escenario.
    [SerializeField] private TMP_Text _textInteraction;

    // Texto relacionado a la lectura de documentos.
    [SerializeField] private TMP_Text _textDocument;

    // Tiempo de espera antes de borrar el mensaje impreso en el panel de interacciones.
    [SerializeField] private float timeBetweenInteractions;

    // Próximo momento en que se eliminará el mensaje impreso.
    private float _timeToDeleteInteraction;

    // Inicia el texto de interacción como nulo.
    private void Awake()
    {
        UpdateInteractionObject("");
    }

    // Revisa que no exista ningún mensaje impreso pasado el tiempo definido.
    private void Update()
    {
        if (_textInteraction.text != "" && _timeToDeleteInteraction <= Time.time)
        {
            _textInteraction.text = "";
        }
    }

    // Método llamado al cambiar la vida del personaje. Se actualiza este valor y la vida máxima
    // que tiene hasta el momento en la UI.
    public void UpdateHealthUI(float currentHealth, float maximumHealth)
    {
        _textCurrentHealth.text = currentHealth.ToString();
        _textMaxHealth.text = "/ " + maximumHealth.ToString();
    }

    // Método llamado al cambiar la munición que tiene cargada el arma. Obtiene la munición actual
    // y máxima del arma actual para imprimirlo en la UI.
    public void UpdateBulletWeaponUI(int currentBullets, int maxBullets)
    {
        _textCurrentAmmo.text = currentBullets.ToString() + " / " + maxBullets.ToString();
    }

    // Método llamado al cambiar la munición que lleva el personaje en su inventario.
    // Cambia el valor de este que se muestra en la UI.
    public void UpdateBulletInventoryUI(int bulletsInventory)
    {
        Debug.Log(bulletsInventory);
        _textInventoryAmmo.text = bulletsInventory.ToString();
    }

    // Método para cambiar el texto mostrado en las interacciones con objetos.
    // De acuerdo al objeto que lo llame, se mostrará un texto en medio de la pantalla
    // asociado a este.
    public void UpdateInteractionObject(string textToPrint)
    {
        _textInteraction.text = textToPrint;
        _timeToDeleteInteraction = Time.time + timeBetweenInteractions;
    }

    public void UpdateDocumenText(string textToPrint)
    {
        _textDocument.text = textToPrint;
    }
}
