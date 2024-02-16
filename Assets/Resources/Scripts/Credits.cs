using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour
{
    /* Jak dzia³a ten skrypt?
     * zmienia wyœwietlany tekst w zale¿noœci od klawisza który przypiszemy.
     * póŸniej pobiera ten klucz i w³¹cza lub wy³¹cza gre
     * 
     * nastêpnie wypisuje wartoœci zapisane w playerprefs
     * 
     * jak u¿ywaæ ten skrypt?
     * skrypt nale¿y podpi¹c gdzieœ w scenie najlepiej pod canvas
     * i póŸniej podpi¹æ teksty który chcemy zmieniæ
     * 
     */
    [SerializeField] KeyCode key;
    public TMP_Text text;
    public TMP_Text[] texts;
    private void Start()
    {
        //TMP_Text text = GetComponentInChildren<TMP_Text>();
        if (key == KeyCode.None || key == KeyCode.Escape)
        {
            Debug.Log("Keycode either set to None or to Escape. Defaulting key to Space");
            key = KeyCode.Space;
        }
        //text.text = "PRESS " + key + "TO GO BACK TO MENU";

        texts[0].text = "EnemiesKilled: "  + PlayerPrefs.GetInt("EnemiesKilled").ToString();
        texts[1].text = "TimesJumped: "    + PlayerPrefs.GetInt("TimesJumped").ToString();
        texts[2].text = "StarsCollected: " + PlayerPrefs.GetInt("StarsCollected").ToString();
        texts[3].text = "CoinsCollected: " + PlayerPrefs.GetInt("CoinsCollected").ToString();
        texts[4].text = "TimesHealed: "    + PlayerPrefs.GetInt("TimesHealed").ToString();
        PlayerController.currentHealth = 0;
        PlayerPrefs.DeleteAll();
    }
    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}