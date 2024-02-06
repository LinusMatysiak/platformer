using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GUI : MonoBehaviour
{
    TMP_Text coinDisplay;
    Image healthDisplay;
    private void Start()
    {
        coinDisplay = GameObject.Find("CoinDisplay").GetComponent<TMP_Text>();
        healthDisplay = GameObject.Find("HealthDisplay").GetComponent<Image>();
    }
    private void Update()
    {
        ChangeValues();
    }
    void ChangeValues()
    {
        coinDisplay.text = "Coins: " + PlayerController.points.ToString();
        //healthDisplay.fillAmount = PlayerController.health;
    }
}
