using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    /* jak dzia³a ten skrypt?
     * zmienia wyœwietlany tekst w zale¿noœci od klawisza który przypiszemy.
     * póŸniej pobiera ten klucz i w³¹cza lub wy³¹cza gre
     * 
     * jak u¿ywaæ ten skrypt?
     * skrypt nale¿y podpi¹c gdzieœ w scenie najlepiej pod canvas
     * i póŸniej podpi¹æ tekst który chcemy zmieniæ
     */
    [SerializeField] KeyCode key;
    public TMP_Text text;
    public string scene;

    private void Start() {
        if (key == KeyCode.None || key == KeyCode.Escape) {
            Debug.Log("Keycode jest ustawiony albo na 'None' albo 'Escape', wiêc program domyœlnie ustawi Keycode na 'Space'");
            key = KeyCode.Space;
        }
        text.text = "PRESS " + key;
        
    }
    private void Update() {
        if (Input.GetKeyDown(key)) {
            SceneManager.LoadScene(scene);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            // wy³¹cza gre
            Application.Quit();
        }
    }
}