using UnityEngine;
using TMPro;
public class UI : MonoBehaviour
{
    TMP_Text coinDisplay;
    TMP_Text healthDisplay;
    private void Start()
    {
        coinDisplay = GameObject.Find("CoinDisplay").GetComponent<TMP_Text>();
        healthDisplay = GameObject.Find("HealthDisplay").GetComponent<TMP_Text>();
    }
    private void Update()
    {
        ChangeValues();
    }
    void ChangeValues()
    {
        coinDisplay.text = "Coins: " + PlayerController.points.ToString();
        healthDisplay.text = "Health: " + PlayerController.health.ToString();
    }
}
