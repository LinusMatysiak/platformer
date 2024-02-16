using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GUI : MonoBehaviour
{
    /* Jak dzia³a ten skrypt?
     * wyœwietla ¿ycie i iloœæ monet które ma gracz
     * 
     * 
     * Jak u¿ywac ten skrypt?
     * zale¿y nadaæ tag obiektom które maj¹ wyswietlaæ wartoœci
     * i podpi¹æ gdzieœ skrypt, najlepiej pod canvas
     */
    TMP_Text coinDisplay;
    Image healthDisplay;
    static float maxhealth;
    private void Start()
    {
        if (maxhealth == 0)
        {
            //maxhealth = PlayerController.health;
        }
        coinDisplay = GameObject.Find("CoinDisplay").GetComponent<TMP_Text>();
        healthDisplay = GameObject.Find("HealthDisplay").GetComponent<Image>();
    }
    private void Update()
    {
        ChangeValues();
    }
    void ChangeValues()
    {
        coinDisplay.text = PlayerPrefs.GetInt("CoinsCollected").ToString();
        healthDisplay.fillAmount = PlayerController.currentHealth / PlayerController.maxHealth;
    }
}
