using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GUI : MonoBehaviour
{
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
