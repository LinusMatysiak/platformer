using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    // Przypisanie klawisza do p�niejszego odpalenia gry
    [SerializeField] KeyCode key;
    public TMP_Text text;

    private void Start() {
        // Sprawdzenie, czy key ma jak�� warto�� i czy ta warto�� to jest "Escape"
        // je�eli key nie ma warto�ci lub jest przypisana na "Escape"
        // ustalana jest warto�� key na "Space"
        if (key == KeyCode.None || key == KeyCode.Escape) {
            Debug.Log("Keycode jest ustawiony albo na 'None' albo 'Escape', wi�c program domy�lnie ustawi Keycode na 'Space'");
            key = KeyCode.Space;
        }
        // Ustawienie tekstu w komponencie TMP_Text z informacj� kt�ry klawisz nas przeniesie do nast�pnej sceny
        text.text = "PRESS " + key;
        
    }
    private void Update() {
        // Sprawdzenie, czy klawisz zosta� naci�ni�ty, a nast�pnie przej�cie do kolejnej sceny
        if (Input.GetKeyDown(key)) {// Sprawdzenie, czy klawisz zosta� naci�ni�ty, a nast�pnie przej�cie do kolejnej sceny
            SceneManager.LoadScene(sceneBuildIndex: SceneManager.GetActiveScene().buildIndex + 1);
        }
        // Sprawdzenie, czy klawisz Escape zosta� naci�ni�ty, a nast�pnie wyj�cie z aplikacji
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}